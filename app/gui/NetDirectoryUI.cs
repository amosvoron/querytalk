using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace QueryTalk.Mapper
{
    internal partial class NetDirectoryUI : UserControl, ILateInitializer
    {
        private MappingUI _mappingUI;
        private string _netPath;

        #region ILateInitializer

        void ILateInitializer.Initialize()
        {
            // Log.LogUse(7);
        }

        #endregion

        internal NetDirectoryUI(MappingUI mappingUI, string netPath)
        {
            _mappingUI = mappingUI;
            _netPath = netPath;
            InitializeComponent();

            _ctrDirectory.Text = netPath;

            // description
            _messageBox.Text = @"A valid .NET Framework directory path is needed to compile the mapping file. Normally, it is located in the C:\Windows\Microsoft.NET\Framework64\v4.0.30319 or C:\Windows\Microsoft.NET\Framework\v4.0.30319 directory.";

            // check
            PerformCheck();

            AddEventHandlers();
        }

        // check validity of the .NET directory
        private void PerformCheck()
        {
            var path = _ctrDirectory.Text;

            if (!Common.IsPathValid(path) || !File.Exists(Path.Combine(path, "System.dll")))
            {
                _ctrValidationMessage.Text = ".NET directory is not valid.";
                _ctrValidationMessage.ForeColor = Color.Red;
                _linkBack.Visible = false;
                _iconOK.Visible = false;
            }
            else
            {
                _ctrValidationMessage.Text = ".NET directory is valid.";
                _ctrValidationMessage.ForeColor = Color.Green;
                _mappingUI.NET_PATH = path;
                _linkBack.Visible = true;
                _iconOK.Visible = true;
            }
        }

        private void AddEventHandlers()
        {
            _ctrDirectory.TextChanged += (o, e) =>
            {
                PerformCheck();
            };
        }

        private void _linkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mappingUI.MainUI.ShowMainPage();
        }

        private void _btnChoose_Click(object sender, System.EventArgs e)
        {
            try
            {
                // Log.LogUse(51);
                var search = new FolderBrowserDialog();
                search.Description = "Choose a .NET Directory";
                DialogResult result = search.ShowDialog();

                if (result == DialogResult.OK)
                {
                    _ctrDirectory.Text = search.SelectedPath;
                }

            }
            catch { }
        }

        private void _imageVideo_Click(object sender, System.EventArgs e)
        {
            // Log.LogUse(71);
            Common.OpenBrowser(Globals.GlobalResources.AppDotNetDirectoryVideo);
        }
    }
}
