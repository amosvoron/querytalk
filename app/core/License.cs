using System;
using System.Threading.Tasks;
using QueryTalk.Security;

namespace QueryTalk.Mapper
{
    internal class License
    {
        private string _communicationID = Guid.NewGuid().ToString("D");
        internal string CommunicationID
        {
            get
            {
                return _communicationID;
            }
        }

        internal string User { get; private set; }

        internal string Password { get; private set; }

        internal string ExpirationDate { get; private set; }

        internal void SetExpirationDate(string expirationDate)
        {
            ExpirationDate = expirationDate;
        }

        internal bool IsLimited { get; set; }

        private bool _hasAccount;
        internal bool HasAccount
        {
            get
            {
                return _hasAccount;
            }
        }

        internal void SetHasAccount()
        {
            _hasAccount = true;
        }

        internal bool IsFreeTrial
        {
            get
            {
                return !_hasAccount;
            }
        }

        internal string AppTitle
        {
            get
            {
                return String.Format("QueryTalker {0}", Common.AssemblyVersion);

                if (IsLimited)
                {
                    return String.Format("QueryTalker {0} | {1} | LIMITED VERSION",
                        Common.AssemblyVersion, User);
                }
                else
                {
                    // anonymous
                    if (!HasAccount)
                    {
                        return String.Format("QueryTalker {0} | TRIAL VERSION | Valid until {1}",
                            Common.AssemblyVersion, ExpirationDate);
                    }
                    // registered user
                    else
                    {
                        return String.Format("QueryTalker {0} | {1} | Licence valid until {2}",
                            Common.AssemblyVersion, User, ExpirationDate);
                    }
                }
            }
        }

        internal void GoToLicensePage()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    string responseStr = CommunicationManager.ExecuteCommand(new Guid().ToString("D"), Commands.VIEWLICENSE, 
                        User, Password, string.Empty);
                    if (CommunicationManager.IsDenied(responseStr))
                    {
                        System.Diagnostics.Process.Start(Globals.GlobalResources.AppLicenseUrl);
                    }
                }
                catch { }
            });
        }

        internal void ShowFreeTrial(System.Windows.Forms.Label label)
        {
            // hide Free Trial 
            label.Visible = false;
            return;

            if (IsFreeTrial)
            {
                label.Text = "Free Trial!";
                label.Visible = true;
            }
            else
            {
                label.Visible = false;
            }
        }

        internal void SetUser(string email, string password)
        {
            if (!string.IsNullOrEmpty(email))
            {
                Program.License.User = email;
                Program.License.Password = password;
            }
        }

    }
}
