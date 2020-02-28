using System;
using System.Windows.Forms;
using QueryTalk.Security;
using QueryTalk.Globals;

namespace QueryTalk.Mapper
{
    internal partial class AccountUI : UserControl, ILateInitializer
    {

        #region Fields & Properties

        private MainForm _mainForm;    
        private bool _firstConnect = true;   // true only on first connect

        private Connect _connect
        {
            get
            {
                return Program.Connect;
            }
        }

        private Start _start
        {
            get
            {
                return Program.Start;
            }
        }

        private void SetFirstConnectToFalse()
        {
            _firstConnect = false;
        }

        private bool AutoConnecting
        {
            get
            {
                // autoconnect on
                if (!_firstConnect && _checkAutoconnect.Checked)
                {
                    return true;
                }

                // anonymous
                if (_firstConnect && !Program.License.HasAccount)
                {
                    return true;
                }

                return false;
            }
        }

        internal string Email
        {
            get
            {
                return _ctrEmail.Text;
            }
        }

        internal string Password
        {
            get
            {
                return _ctrPassword.Text;
            }
        }

        internal bool AutoConnect
        {
            get
            {
                return _checkAutoconnect.Checked;
            }
        }

        #endregion

        internal AccountUI(MainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            _checkAutoconnect.SetToolTip("Autoconnect (Remember me)", "Save my credentials & connect automatically");
            AddEventHandlers();

            //StartTasks();

            // event handlers
            _connect.ServerDenied += (o, e) => Deny(e.ConnectionToken, e.ServerResponse);
            _connect.ServerAllowed += (o, e) => OnAllowed(e.ServerResponse);
            _connect.Canceled += (o, e) => Cancel();
        }

        #region Authenticate

        private Guid _connectionToken;
        internal void Authenticate()
        {
            _connectionToken = Guid.NewGuid();
            ShowProcessing();
            _connect.Authenticate(_connectionToken, _ctrEmail.Text, _ctrPassword.Text, _checkAutoconnect.Checked);
        }

        private void OnAllowed(string serverResponse)
        {
            HideProcessing();
            Program.License.SetUser(_ctrEmail.Text, _ctrPassword.Text);
            Program.License.SetExpirationDate(Connect.GetDateFromServerResponse(serverResponse));

            // free trial email
            if (_connect.ConnectAsAnonymous && _connect.FreeTrialEmail == null)
            {
                _mainForm.ShowPage(new FreeTrialEmailUI(_mainForm));
                return;
            }

            _mainForm.OnAuthenticationSuccess();
        }

        #endregion

        #region ILateInitializer

        void ILateInitializer.Initialize()
        {
            // Log.LogUse(23);

            if (Program.Connect.ConnectAsRegisteredUser)
            {
                _linkConnectAsAnonymous.Visible = false;
            }

            // reset:
            _ctrEmailVal.Visible = false;
            _ctrPasswordVal.Visible = false;
            _ctrDenied.Visible = false;
            _linkDeniedDescription.Visible = false;
            _ctrCancel.Visible = false;
            _iconAjax.Visible = false;
        }

        #endregion

        #region Event handlers

        private void AddEventHandlers()
        {
            _btnConnect.Click += (o, e) =>
            {
                // Log.LogUse(42);

                // if a Connect button is pressed => connect as Registered user
                Program.Connect.SetConnectAsRegisteredUser();

                if (ValidateCredentials())
                {
                    Authenticate();
                }
            };

            _ctrCancel.Click += (o, e) =>
            {
                Cancel();
            };

            _ctrPassword.KeyDown += (o, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (ValidateCredentials())
                    {
                        Authenticate();
                    }
                }
            };

            _iconEye.MouseDown += (o, e) =>
            {
                _ctrPassword.PasswordChar = '\0';
            };

            _iconEye.MouseUp += (o, e) =>
            {
                _ctrPassword.PasswordChar = '*';
            };

            _ctrEmail.TextChanged += (o, e) =>
            {
                if (string.IsNullOrEmpty(_ctrEmail.Text))
                {
                    _ctrEmailVal.Visible = true;
                }
                else
                {
                    _ctrEmailVal.Visible = false;
                }
            };

            _ctrPassword.TextChanged += (o, e) =>
            {
                if (string.IsNullOrEmpty(_ctrPassword.Text))
                {
                    _ctrPasswordVal.Visible = true;
                }
                else
                {
                    _ctrPasswordVal.Visible = false;
                }
            };

            _linkDeniedDescription.Click += (o, e) =>
            {
                try
                {
                    System.Diagnostics.Process.Start(GlobalResources.AppLicenseVerificationFailedUrl);
                }
                catch { }  // do nothing
            };

        }

        private void _linkConnectAsAnonymous_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // if a Connect button is pressed => connect as Registered user
            Program.Connect.SetConnectAsAnonymous();
            Authenticate();
        }

        #endregion

        #region Credentials, validation & registry

        internal void SetCredentials()
        {
            _ctrEmail.Text = Program.Connect.RegistryEmail;
            _ctrPassword.Text = Program.Connect.RegistryPassword;
            _checkAutoconnect.Checked = Program.Connect.AutoConnect;
        }

        private bool ValidateCredentials()
        {
            if (Program.Connect.ConnectAsAnonymous)
            {
                return true;
            }

            var isValid = true;

            // validate
            if (String.IsNullOrEmpty(_ctrEmail.Text))
            {
                _ctrEmailVal.Visible = true;
                isValid = false;
            }

            if (String.IsNullOrEmpty(_ctrPassword.Text))
            {
                _ctrPasswordVal.Visible = true;
                isValid = false;
            }

            return isValid;
        }

        private void SaveRegister()
        {
            try
            {
                RegistryManager registry = new RegistryManager();
                if (_checkAutoconnect.Checked)
                {
                    registry.SaveCredencials(_ctrEmail.Text, _ctrPassword.Text, _checkAutoconnect.Checked);
                }
                else
                {
                    registry.ClearCredentials();
                }
            }
            catch { }   // do not throw exception if something went wrong with the Registry
        }

        #endregion

        #region Cancel, Deny

        private void Cancel()
        {
            try
            {
                // Log.LogUse(43);

                SetFirstConnectToFalse();
                _connect.Cancel();
                HideProcessing();
            }
            catch (Exception ex)
            {
                Common.ShowUnknownException(ex);
            }
        }

        internal void Deny(Guid connectionToken, string message)
        {
            try
            {
                if (_connectionToken != connectionToken && _connectionToken != Guid.Empty)
                {
                    return;
                }

                // Log.LogUse(25, Connect.PrepareDeniedMessage(message));

                HideProcessing();

                // exit if the task was canceled
                if (_connect.IsCryptoCanceled)
                {
                    return;
                }

                ShowDenied(message);

                // check if server connection failed => set flag
                _connect.HandleServerFailed(message);
            }
            catch (Exception ex)
            {
                Common.ShowUnknownException(ex);
            }
        }

        #endregion

        #region Show, Hide

        private void ShowDenied(string message)
        {
            _ctrDenied.ThreadSafeInvoke(() =>
            {
                _ctrDenied.Visible = true;
                var output = Connect.PrepareDeniedMessage(message);

                if (output == Connect.PrepareDeniedMessage(GlobalResources.ServerFailedMessage))
                {
                    _linkDeniedDescription.Text = "Check you internet connection and try again. If it doesn't help, try later.";
                    _linkDeniedDescription.Visible = true;
                    _linkDeniedDescription.Enabled = false;
                }

                else if (output == Connect.PrepareDeniedMessage(GlobalResources.LicenseFailedMessage))
                {
                    _linkDeniedDescription.Text = "What can be wrong with my license?";
                    _linkDeniedDescription.Visible = true;
                    _linkDeniedDescription.Enabled = true;
                    _linkConnectAsAnonymous.Visible = false;
                }

                // output:
                _ctrDenied.Text = output;
            });
        }

        private void ShowProcessing()
        {
            _btnConnect.ThreadSafeInvoke(() =>
            {
                _ctrCancel.Visible = true;
                _iconAjax.Visible = true;
                _btnConnect.Enabled = false;
                _ctrDenied.Visible = false;
                _linkDeniedDescription.Visible = false;
            });
        }

        private void HideProcessing()
        {
            _btnConnect.ThreadSafeInvoke(() =>
            {
                _btnConnect.Enabled = true;
                _iconAjax.Visible = false;
                _ctrCancel.Visible = false;
                SetFirstConnectToFalse();
            });
        }

        #endregion

    }
}
