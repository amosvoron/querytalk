using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using QueryTalk.Security;
using QueryTalk.Globals;

namespace QueryTalk.Mapper
{
    internal class Connect
    {

        internal event EventHandler<AccountEventArgs> ServerDenied;
        internal event EventHandler<AccountEventArgs> ServerAllowed;
        internal event EventHandler<AccountEventArgs> Canceled;

        private CancellationTokenSource _connectTaskToken;

        #region ConnectAsAnonymous

        private bool _connectAsAnonymous;
        internal bool ConnectAsAnonymous
        {
            get
            {
                return !_isTrialVersionExpired && _connectAsAnonymous;
            }
        }

        private bool _isTrialVersionExpired;
        internal bool IsTrialVersionExpired
        {
            get
            {
                return _isTrialVersionExpired;
            }
        }

        internal void SetTrialVersionExpired()
        {
            _isTrialVersionExpired = true;
        }

        internal void SetConnectAsRegisteredUser()
        {
            _connectAsAnonymous = false;
        }

        internal void SetConnectAsAnonymous()
        {
            _connectAsAnonymous = true;
        }

        internal bool ConnectAsRegisteredUser
        {
            get
            {
                return !ConnectAsAnonymous;
            }
        }

        #endregion

        internal bool AutoConnect
        {
            get
            {
                return !Program.License.HasAccount || RegistryAutoconnect;
            }
        }

        internal string RegistryEmail { get; private set; }

        internal string RegistryPassword { get; private set; }

        internal bool RegistryAutoconnect { get; private set; }

        // indicates that the last call to server was denied due to the ServerFailed denial
        private bool _wasServerFailed;

        internal string FreeTrialEmail { get; private set; }

        internal Connect()
        {
            try
            {
                StartTasks();

                var registry = new RegistryManager();
                if (registry.HasAccount)
                {
                    Program.License.SetHasAccount();
                    _connectAsAnonymous = false;
                }
                else
                {
                    _connectAsAnonymous = true;
                }

                var objCredential = registry.ReadCredencial();
                if (objCredential != null && objCredential.Count == 3)
                {
                    if (!ConnectAsAnonymous)
                    {
                        RegistryEmail = objCredential[0];
                        RegistryPassword = objCredential[1];
                    }

                    RegistryAutoconnect = (objCredential[2].ToLower() == "true");
                }
            }
            catch
            { }
        }

        private void StartTasks()
        {
            // crypto
            _cryptoTask = Task.Factory.StartNew(() => PrepareCryptoContext());

            // machine
            Task.Factory.StartNew(() =>
            {
                var computerName = MachineManager.ComputerName;
                var manufacturer = MachineManager.ComputerManufacturer;
                var model = MachineManager.Model;
                var processor = MachineManager.Procesor;
                var machineKey = MachineManager.MachineKey;
            });
        }

        internal void Authenticate(Guid connectionToken, string email, string password, bool autoconnect)
        {
            // Log.LogUse(3);

            if (ConnectAsRegisteredUser && (email == null || password == null))
            {
                ServerDenied?.Invoke(this, new AccountEventArgs(connectionToken, GlobalResources.LoginFailedMessage));
                return;
            }

            _connectTaskToken = new CancellationTokenSource();

            Task.Factory.StartNew(() =>
            {
                try
                {
                    HandleCrypto(connectionToken);

                    if (_connectTaskToken.IsCancellationRequested)
                    {
                        Canceled?.Invoke(this, new AccountEventArgs());
                        return;
                    }

                    string response;
                    if (Program.Connect.ConnectAsAnonymous)
                    {
                        response = CommunicationManager.ExecuteCommand(Program.License.CommunicationID, Commands.AUTHENTICATE_ANONYMOUS, 
                            string.Empty, string.Empty, string.Empty);
                        TryParseFreeTrialEmail(response);
                    }
                    else
                    {
                        response = CommunicationManager.ExecuteCommand(Program.License.CommunicationID, Commands.AUTHENTICATE, 
                            email, password, string.Empty);
                    }

                    // check if canceled
                    if (_connectTaskToken.IsCancellationRequested)
                    {
                        Canceled?.Invoke(this, new AccountEventArgs());
                        return;
                    }

                    //_mainForm.ShowAccountUI();

                    if (CommunicationManager.IsDenied(response))
                    {
                        ServerDenied?.Invoke(this, new AccountEventArgs(connectionToken, response));
                        return;
                    }

                    // success:
                    else
                    {
                        TrySetRegisteredUser();
                        SaveRegister(email, password, autoconnect);
                        ServerAllowed?.Invoke(this, new AccountEventArgs(connectionToken, response));
                    }
                }
                catch (Exception ex)
                {
                    ServerDenied?.Invoke(this, new AccountEventArgs(connectionToken, GlobalResources.ServerFailedMessage));
                }
            }, _connectTaskToken.Token);
        }

        private void TryParseFreeTrialEmail(string value)
        {
            if (!Regex.IsMatch(value, ";"))
            {
                return;
            }

            var email = Regex.Replace(value, @"^([^;]*;)", "");
            if (string.IsNullOrWhiteSpace(email))
            {
                return;
            }

            FreeTrialEmail = email.Trim();
        }

        #region Crypto Context

        private Task _cryptoTask;
        private TaskStatus _cryptoTaskStatus = TaskStatus.None;

        internal bool IsCryptoCanceled
        {
            get
            {
                return _connectTaskToken != null && _connectTaskToken.IsCancellationRequested;
            }
        }

        internal void Cancel()
        {
            if (_connectTaskToken != null)
            {
                _connectTaskToken.Cancel();
            }
        }

        private void HandleCrypto(Guid connectionToken)
        {
            // finalize crypto task
            if (_cryptoTaskStatus != TaskStatus.None)
            {
                _cryptoTask.Wait();
                FinalizeCryptoTask();   // !important
            }

            // check if last denial was ServerFailed
            else if (_wasServerFailed)
            {
                var responseStr = CommunicationManager.StartCommunication(Program.License.CommunicationID);

                if (CommunicationManager.IsDenied(responseStr))
                {
                    ServerDenied?.Invoke(this, new AccountEventArgs(connectionToken, responseStr));
                    return;
                }

                _wasServerFailed = false;
            }

            // check: assure that simetric key is given
            if (string.IsNullOrEmpty(CryptoContext.CryptedSimKey))
            {
                ServerDenied?.Invoke(this, new AccountEventArgs(connectionToken, GlobalResources.ServerFailedMessage));
                return;
            }
        }

        internal void PrepareCryptoContext()
        {
            try
            {
                _cryptoTaskStatus = TaskStatus.Running;
                var responseStr = CommunicationManager.StartCommunication(Program.License.CommunicationID);

                if (CommunicationManager.IsDenied(responseStr))
                {
                    return;
                }
            }
            catch
            { }
        }

        internal void FinalizeCryptoTask()
        {
            _cryptoTaskStatus = TaskStatus.None;
        }

        #endregion

        #region Helper methods

        private void SaveRegister(string email, string password, bool autoconnect)
        {
            try
            {
                RegistryManager registry = new RegistryManager();
                if (autoconnect)
                {
                    registry.SaveCredencials(email, password, autoconnect);
                }
                else
                {
                    registry.ClearCredentials();
                }
            }
            catch { }   // do not throw exception if something went wrong with the Registry
        }

        private void TrySetRegisteredUser()
        {
            if (ConnectAsAnonymous)
            {
                // Log.LogUse(31);
                return;
            }

            // Log.LogUse(32);
            Program.License.SetHasAccount();
            RegistryManager registry = new RegistryManager();
            registry.RegisterAccount();
        }

        internal bool HandleServerFailed(string message)
        {
            var clean = PrepareDeniedMessage(message);
            var failed = (clean == PrepareDeniedMessage(GlobalResources.ServerFailedMessage));
            if (failed)
            {
                _wasServerFailed = true;
            }

            return failed;
        }

        internal static string PrepareDeniedMessage(string message)
        {
            return Regex.Replace(message, "^Denied:", "").Trim();
        }

        internal static string GetDateFromServerResponse(string message)
        {
            // remove "Allowed"
            var date = Regex.Replace(message, @"Allowed", "").Trim();

            // clear free trial email, if any
            date = Regex.Replace(date, @";.*$", "");

            return date;
        }

        internal static bool IsLicenseFailed(string message)
        {
            return PrepareDeniedMessage(message) == PrepareDeniedMessage(GlobalResources.LicenseFailedMessage);
        }

        #endregion

    }
}
