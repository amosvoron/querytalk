using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace QueryTalk.Mapper
{
    internal partial class Step1_RepositoryUI : UserControl, ILateInitializer
    {
        private MainForm _mainForm;
        private string _repositoryPathOnEnter;
        private RegistryHandler _registry;

        private Start _start
        {
            get
            {
                return Program.Start;
            }
        }

        internal Step1_RepositoryUI(MainForm mainForm)
        {
            _mainForm = mainForm;
            _registry = new RegistryHandler();
            InitializeComponent();
            AddEventHandlers();
        }

        private void AddEventHandlers()
        {
            _ctrRepository.TextChanged += (o, e) =>
            {
                Execute();
            };
        }

        #region ILateInitializer

        void ILateInitializer.Initialize()
        {
            // Log.LogUse(36);

            // init
            Program.License.ShowFreeTrial(_ctrFreeTrial);
            _repositoryPathOnEnter = _start.RepositoryPath;
            _ctrRepository.Text = _repositoryPathOnEnter;
  
            Execute();
        }

        #endregion

        private void ToggleIconOK(bool showOK)
        {
            if (showOK)
            {
                _iconOK.Visible = true;
            }
            else
            {
                _iconOK.Visible = false;
            }
        }

        private void Reset()
        {
            _iconOK.ThreadSafeInvoke(() =>
            {
                Start.DisableLink(_linkBack);
                Start.DisableLink(_linkNext);
                Start.DisableLink(_linkMainPage);
                _ctrValidationMessage.Text = "";
                ToggleIconOK(false);
                _ctrDone.Visible = false;
            });
        }

        private void SetFailed(string message)
        {
            _ctrDone.ThreadSafeInvoke(() =>
            {
                _ctrValidationMessage.Text = message;
                _ctrValidationMessage.ForeColor = Color.Red;
                _ctrDone.Visible = false;
                ToggleIconOK(false);
            });
        }

        private void SetDone()
        {
            _ctrDone.ThreadSafeInvoke(() =>
            {
                _ctrDone.Visible = true;
                ToggleIconOK(true);
                _HandleNavigation();
            });
        }

        private void _HandleNavigation()
        {
            // default handling
            if (_start.CurrentMode == StartType.GetStarted)
            {
                Start.EnableLink(_linkNext);
                return;
            }

            // library does not exist => continue with the next page
            if (!Pull.LibraryExists)
            {
                Start.EnableLink(_linkNext);
                return;
            }

            // default connection does not exists
            if (_start.CurrentMode == StartType.Flexible && _start.ConnBuilder == null)
            {
                Start.EnableLink(_linkNext);
                return;
            }

            // enable Back/MainPage link
            if (_start.CurrentMode == StartType.Mapping)
            {
                Start.EnableLink(_linkBack);
            }
            else
            {
                Start.EnableLink(_linkMainPage);
            }
        }

        // check validity of the .NET directory
        private void Execute()
        {
            Reset();
            var folder = _ctrRepository.Text;

            // repository is not given
            if (String.IsNullOrWhiteSpace(folder))
            {
                SetFailed("Choose the repository folder.");
                return;
            }

            // folder does not exist
            if (!Directory.Exists(folder))
            {
                SetFailed("The folder does not exist.");
                return;
            }

            // done
            _start.RepositoryPath = folder;
            SetDone();
        }

        private void _btnChoose_Click(object sender, EventArgs e)
        {
            try
            {
                var search = new FolderBrowserDialog();
                search.Description = "Choose repository folder";
                DialogResult result = search.ShowDialog();

                if (result == DialogResult.OK)
                {
                    _ctrRepository.Text = search.SelectedPath;
                }

            }
            catch { }
        }

        #region Navigation

        private void _linkNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //_mainForm.ShowPage(new Step2_PullUI(_mainForm));
            _mainForm.ShowPage(new Step3_ServerUI(_mainForm));
        }

        private void _linkMainPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mainForm.ShowMainPage();
        }

        private void _linkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mainForm.ShowMainPage();
        }

        #endregion

        private void _imageVideo_Click(object sender, EventArgs e)
        {
            // Log.LogUse(67);
            Common.OpenBrowser(Globals.GlobalResources.AppRepositoryVideo);
        }
    }
}
