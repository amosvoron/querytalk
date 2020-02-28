using System.Windows.Forms;
using System.Reflection;

namespace QueryTalk.Mapper
{
    internal partial class AboutUI : UserControl, ILateInitializer
    {
        private MappingUI _mappingUI;

        #region ILateInitializer

        void ILateInitializer.Initialize()
        {
            // Log.LogUse(10);
        }

        #endregion

        internal AboutUI(MappingUI mappingUI)
        {
            _mappingUI = mappingUI;
            InitializeComponent();

            Program.License.ShowFreeTrial(_ctrFreeTrial);

            // show data
            _ctrVersion.Text = string.Format("version {0}", GetVersion());
            _ctrLicenseEmail.Text = Program.License.User;
            _ctrExpirationDate.Text = Program.License.ExpirationDate;
            _ctrMachineKey.Text = Security.MachineManager.MachineKey;

            AddEventHandlers();
        }

        // get assembly informational version
        private string GetVersion()
        {
            return (Assembly.GetEntryAssembly()
                .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false)
                    as AssemblyInformationalVersionAttribute[])[0].InformationalVersion;
        }

        private void AddEventHandlers()
        {
            _linkQueryTalk.Click += (o, e) =>
                {
                    // Log.LogUse(20);

                    try
                    {
                        System.Diagnostics.Process.Start(Globals.GlobalResources.QueryTalkUrlDomain);
                    }
                    catch { }
                };

            _linkMyLicense.Click += (o, e) =>
            {
                // Log.LogUse(21);

                if (Program.License.HasAccount)
                {
                    Program.License.GoToLicensePage();
                }
                else
                {
                    Common.ShowNotification("TRIAL VERSION does not have a license.");
                }
            };

            _linkEula.Click += (o, e) =>
            {
                try
                {
                    // Log.LogUse(22);

                    System.Diagnostics.Process.Start(Globals.GlobalResources.AppEulaUrl);
                }
                catch { }
            };
        }

        private void _linkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mappingUI.MainUI.ShowMainPage();
        }
    }
}
