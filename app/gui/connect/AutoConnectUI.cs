using System;
using System.Windows.Forms;

namespace QueryTalk.Mapper
{
    internal partial class AutoConnectUI : UserControl, ILateInitializer
    {
        private MainForm _mainForm;

        private Connect _connect
        {
            get
            {
                return Program.Connect;
            }
        }

        #region ILateInitializer

        void ILateInitializer.Initialize()
        {
            // Log.LogUse(33);
        }

        #endregion

        internal AutoConnectUI(MainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            Program.License.ShowFreeTrial(_ctrFreeTrial);
            BackColor = Common.BackgroundColor;

            // is registered
            if (Program.Connect.ConnectAsRegisteredUser)
            {
                _ctrFreeTrial.Visible = false;
                _linkCancel.Visible = true;
            }

            _iconAjax.Visible = true;

            AddEventHandlers();

            _connect.Authenticate(Guid.NewGuid(), _connect.RegistryEmail, _connect.RegistryPassword, true);
        }

        private void AddEventHandlers()
        {
            _connect.ServerDenied += OnServerDenied;
            _connect.ServerAllowed += OnServerAllowed;
            _connect.Canceled += OnCancel;
        }

        private void RemoveEventHandlers()
        {
            _connect.ServerDenied -= OnServerDenied;
            _connect.ServerAllowed -= OnServerAllowed;
            _connect.Canceled -= OnCancel;
        }

        private void OnServerDenied(object sender, AccountEventArgs e)
        {
            RemoveEventHandlers();

            if (_connect.ConnectAsAnonymous && Connect.IsLicenseFailed(e.ServerResponse))
            {
                Program.Connect.SetTrialVersionExpired();
                _mainForm.ShowFreeTrialExpiredUI();
            }
            else
            {
                _mainForm.ShowAccountUI();
                _mainForm.AccountUI.Deny(Guid.Empty, e.ServerResponse);
                _mainForm.AccountUI.SetCredentials();
            }            
        }

        private void OnServerAllowed(object sender, AccountEventArgs e)
        {
            RemoveEventHandlers();
            Program.License.SetUser(Program.Connect.RegistryEmail, Program.Connect.RegistryPassword);
            Program.License.SetExpirationDate(Connect.GetDateFromServerResponse(e.ServerResponse));

            // free trial email
            if (_connect.ConnectAsAnonymous && _connect.FreeTrialEmail == null)
            {
                _mainForm.ShowPage(new FreeTrialEmailUI(_mainForm));
                return;
            }

            _mainForm.OnAuthenticationSuccess();
        }

        private void OnCancel(object sender, AccountEventArgs e)
        {
            Cancel();
        }

        private void _linkCancel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Cancel();
        }

        private void Cancel()
        {
            // Log.LogUse(44);

            _connect.Cancel();
            RemoveEventHandlers();
            _mainForm.ShowAccountUI();
            _mainForm.AccountUI.SetCredentials();
        }

    }
}
