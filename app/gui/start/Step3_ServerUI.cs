using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace QueryTalk.Mapper
{
    internal partial class Step3_ServerUI : UserControl, ILateInitializer
    {
        private MainForm _mainForm;

        private Start _start
        {
            get
            {
                return Program.Start;
            }
        }

        private void _AddLocalServers()
        {
            if (!_start.HasLocalServers)
            {
                return;
            }

            foreach (var server in _start.LocalServers)
            {
                _comboServer.Items.Add(server);
            }
        }

        internal Step3_ServerUI(MainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
        }

        #region LateInitializer

        private bool _initialized;
        void ILateInitializer.Initialize()
        {
            // Log.LogUse(38);

            // init
            Program.License.ShowFreeTrial(_ctrFreeTrial);
            Reset();
            
            // always show error
            if (_start.CurrentProcessException != null)
            {
                SetFailed();
            }

            if (_initialized)
            {
                return;
            }

            // connection check evenyt handler
            _start.ConnectionCheckCompleted += (o, e) =>
            {
                if (_start.CurrentProcessException != null)
                {
                    SetFailed();
                }
                else
                {
                    SetDone();
                }
            };

            // local servers
            if (!_start.HasLocalServers)
            {
                _iconAjaxLocalServers.ThreadSafeInvoke(() =>
                {
                    _iconAjaxLocalServers.Visible = true;
                });

                _start.LocalServersCompleted += (o, e) =>
                {
                    _comboServer.ThreadSafeInvoke(() =>
                    {
                        _AddLocalServers();
                        _iconAjaxLocalServers.Visible = false;

                        // leave it commented: if active the dropdown appears in other user controls as well
                        //_comboServer.DroppedDown = true;
                    });
                };
            }
            else
            {
                _AddLocalServers();
            }

            // connection data
            _comboServer.ThreadSafeInvoke(() =>
            {
                _comboServer.DropDownHeight = 255;

                // connection data
                if (_start.HasConnection)
                {
                    _comboServer.Text = _start.ConnBuilder.DataSource;
                    _checkAuthentication.Checked = !_start.ConnBuilder.IntegratedSecurity;
                    _ctrLogin.Text = _start.ConnBuilder.UserID;
                    _ctrPassword.Text = _start.ConnBuilder.Password;

                    Reset();
                    //SetIdle();
                }
            });

            _initialized = true;
        }

        #endregion

        private void SetDone()
        {
            // Log.LogUse(60);

            _iconOK.ThreadSafeInvoke(() =>
            {
                _ctrDone.Visible = true;
                _iconOK.Visible = true; 
                Start.DisableLink(_linkFailed);
                _iconAjaxConnect.Visible = false;

                HandleNavigation();
            });
        }

        private void SetIdle()
        {
            if (_start.ConnBuilder == null)
            {
                return;
            }

            Reset();
            HandleNavigation();
        }

        private void Reset()
        {
            _ctrDone.ThreadSafeInvoke(() =>
            {
                HandleNavigation();
                _ctrDone.Visible = false;
                _iconOK.Visible = false;
                _iconAjaxConnect.Visible = false;
            });
        }

        private void SetFailed()
        {
            // Log.LogUse(61);

            _ctrDone.ThreadSafeInvoke(() =>
            {
                HandleNavigation();
                Start.EnableLink(_linkFailed);
                Start.DisableLink(_linkNext);
                Start.DisableLink(_linkMainPage);
                _ctrDone.Visible = false;
                _iconOK.Visible = false;
                _iconAjaxConnect.Visible = false;
            });
        }

        private void HandleNavigation()
        {
            Start.DisableLink(_linkNext);
            Start.DisableLink(_linkMainPage);
            Start.DisableLink(_linkBack);

            // check connection
            if (!_start.HasConnection)
            {
                return;
            }

            // default handling
            if (_start.CurrentMode == StartType.GetStarted)
            {
                //Start.EnableLink(_linkNext);
                Start.EnableLink(_linkMainPage);
                Start.EnableLink(_linkBack);
                return;
            }

            if (_start.CurrentMode == StartType.Mapping)
            {
                Start.EnableLink(_linkBack);
                return;
            }

            // Flexible:
            Start.EnableLink(_linkMainPage);
        }

        internal void SetConnection(SqlConnectionStringBuilder connBuilder)
        {
            _start.SetConnBuilder(connBuilder);
            _comboServer.ThreadSafeInvoke(() =>
            {
                _comboServer.Text = connBuilder.DataSource;
                _checkAuthentication.Checked = !connBuilder.IntegratedSecurity;
                _ctrLogin.Text = connBuilder.UserID;
                _ctrPassword.Text = connBuilder.Password;
            });
        }

        // @param: database
        //   If not null then the client has passed the selected database.
        //   (When combo connections is selected.)
        internal bool Execute(string database = null)
        {
            Reset();

            // validate
            var valid = true;

            if (string.IsNullOrWhiteSpace(_comboServer.Text))
            {
                _ctrServerLabel.ForeColor = Color.Red;
                valid = false;
            }

            if (_checkAuthentication.Checked)
            {
                if (string.IsNullOrEmpty(_ctrLogin.Text))
                {
                    _ctrLoginMessage.Visible = true;
                    valid = false;
                }
            }

            if (!valid)
            {
                return valid;
            }

            // start processing
            _iconAjaxConnect.Visible = true;
            _start.StartConnectionCheck(Guid.NewGuid(), _comboServer.Text, !_checkAuthentication.Checked, _ctrLogin.Text, _ctrPassword.Text,
                database);

            return valid;
        }

        internal void RefreshDatabases(string database)
        {
            if (_start.ConnBuilder == null)
            {
                return;
            }

            _start.StartConnectionCheck(Guid.NewGuid(),
                _start.ConnBuilder.DataSource,
                _start.ConnBuilder.IntegratedSecurity,
                _start.ConnBuilder.UserID,
                _start.ConnBuilder.Password,
                database);
        }

        private void _btnConnect_Click(object sender, EventArgs e)
        {
            // Log.LogUse(48);
            Execute();
        }

        private void _checkAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            _ctrLoginMessage.Visible = false;

            if (_checkAuthentication.Checked)
            {
                _labelLogin.Visible = true;
                _labelPassword.Visible = true;
                _ctrLogin.Visible = true;
                _ctrPassword.Visible = true;
            }
            else
            {
                _labelLogin.Visible = false;
                _labelPassword.Visible = false;
                _ctrLogin.Visible = false;
                _ctrPassword.Visible = false;
            }
        }

        private void _ctrLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(_ctrLogin.Text))
            {
                _ctrLoginMessage.Visible = false;
            }
        }

        private void _comboServer_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_comboServer.Text))
            {
                _ctrServerLabel.ForeColor = Color.Black;
            }
        }

        private void _linkFailed_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_start.CurrentProcessException != null)
            {
                Common.ShowError(_start.CurrentProcessException);
            }
        }

        #region Navigation

        private void _linkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_start.CurrentMode == StartType.GetStarted)
            {
                //_mainForm.ShowPage(new Step2_PullUI(_mainForm));
                _mainForm.ShowPage(new Step1_RepositoryUI(_mainForm));
            }
            else
            {
                _mainForm.ShowMainPage();
            }
        }

        private void _linkNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mainForm.ShowPage(new Step4_QueryTalkBaseUI(_mainForm));
        }

        private void _linkMainPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mainForm.ShowMainPage();
        }

        #endregion

        private void _imageVideo_Click(object sender, EventArgs e)
        {
            // Log.LogUse(69);
            Common.OpenBrowser(Globals.GlobalResources.AppSetServerVideo);
        }
    }
}
