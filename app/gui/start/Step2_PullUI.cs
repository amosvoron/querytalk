using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;

namespace QueryTalk.Mapper
{
    internal partial class Step2_PullUI : UserControl, ILateInitializer
    {
        private MainForm _mainForm;
        private RegistryHandler _registry;
        private Pull _pull;
        private Exception _exception;

        private Start _start
        {
            get
            {
                return Program.Start;
            }
        }

        internal Step2_PullUI(MainForm mainForm)
        {
            _mainForm = mainForm;
            _registry = new RegistryHandler();
            InitializeComponent();
        }

        #region ILateInitializer

        void ILateInitializer.Initialize()
        {
            // Log.LogUse(37);

            // init
            Program.License.ShowFreeTrial(_ctrFreeTrial);

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

        private void Execute()
        {
            try
            {
                Reset();

                _pull = new Pull(null);
                if (Pull.LibraryExists)
                {
                    SetDone("Library exists.");
                }
                else
                {
                    if (Program.Start.PullBytes != null)
                    {
                        _pull.Extract(Program.Start.PullBytes);
                        SetDone("Done.");
                    }
                    else
                    {
                        TryPull();
                    }
                }
            }
            catch (Exception ex)
            {
                _exception = ex;
                SetFailed();
            }
        }

        //private int _test = 0;
        private void TryPull()
        {
            SetProcessing();

            Task.Factory.StartNew(() =>
            {
                try
                {
                    var success = _pull.ForcePull();

                    if (success)
                    {
                        SetDone("Done.");
                    }
                    else
                    {
                        throw new Exception("Pull was canceled.");
                    }
                }
                catch (Exception ex)
                {
                    _exception = ex;
                    SetFailed();
                }
            });
        }

        private void Reset()
        {
            _ctrDone.ThreadSafeInvoke(() =>
            {
                _ctrDone.Visible = false;
                _linkFailed.Visible = false;
                _ctrErrorMessage.Visible = false;
                Start.DisableLink(_linkBack);
                Start.DisableLink(_linkNext);
                Start.DisableLink(_linkMainPage);
                _iconAjax.Visible = false;
                Start.EnableButton(_btnPull);
                ToggleIconOK(false);
                _ctrQueryTalkVersion.Visible = false;
            });
        }

        private void SetDone(string message)
        {
            // Log.LogUse(58);

            _iconOK.ThreadSafeInvoke(() =>
            {
                _ctrDone.Text = message;
                _ctrDone.Visible = true;
                ToggleIconOK(true);
                Start.EnableLink(_linkNext);
                _linkFailed.Visible = false;
                _iconAjax.Visible = false;
                _btnPull.Text = "Pull again";
                Start.EnableButton(_btnPull);
                Start.EnableLink(_linkRepository);
                _ctrQueryTalkVersion.Text = String.Format("QueryTalk {0}", Pull.GetClientLibVersion());
                _ctrQueryTalkVersion.Visible = true;

                _HandleNavigation();
            });
        }

        private void SetProcessing()
        {
            _ctrDone.ThreadSafeInvoke(() =>
            {
                _iconAjax.Visible = true;
                Start.DisableButton(_btnPull);
            });
        }

        private void SetFailed()
        {
            if (_exception != null)
            {
                // Log.LogUse(59, _exception.Message);
            }
            else
            {
                // Log.LogUse(59);
            }

            _ctrDone.ThreadSafeInvoke(() =>
            {
                _ctrDone.Visible = false;
                _linkFailed.Visible = true;
                _iconAjax.Visible = false;
                Start.EnableLink(_linkNext);
                _btnPull.Text = "Pull again";
                Start.EnableButton(_btnPull);
                ToggleIconOK(false);

                if (!Pull.LibraryExists)
                {
                    _ctrErrorMessage.Visible = true;
                    _ctrQueryTalkVersion.Visible = false;
                }
                else
                {
                    _ctrQueryTalkVersion.Text = String.Format("QueryTalk {0}", Pull.GetClientLibVersion());
                    _ctrQueryTalkVersion.Visible = true;
                }

                _HandleNavigation();
            });
        }

        private void _HandleNavigation()
        {
            Start.DisableLink(_linkNext);
            Start.DisableLink(_linkMainPage);
            Start.DisableLink(_linkBack);

            // default handling
            if (_start.CurrentMode == StartType.GetStarted)
            {
                Start.EnableLink(_linkNext);
                Start.EnableLink(_linkBack);
                return;
            }

            // no server connection  => continue with the next page
            if (_start.ConnBuilder == null)
            {
                Start.EnableLink(_linkNext);
                return;
            }

            if (_start.CurrentMode == StartType.Mapping)
            {
                Start.EnableLink(_linkBack);
            }
            else
            {
                Start.EnableLink(_linkMainPage);
            }
        }

        private void _btnPull_Click(object sender, EventArgs e)
        {
            // Log.LogUse(15);
            Reset();
            TryPull();
        }

        private void _linkFailed_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Common.ShowError(_exception);
        }

        private void _linkLibrary_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("explorer", _start.RepositoryPath);
            }
            catch { }
        }

        #region Navigation

        private void _linkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_start.CurrentMode == StartType.GetStarted)
            {
                _mainForm.ShowPage(new Step1_RepositoryUI(_mainForm));
            }
            else
            {
                _mainForm.ShowMainPage();
            }
        }

        private void _linkNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mainForm.ShowPage(new Step3_ServerUI(_mainForm));
        }

        #endregion

        private void _linkMainPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mainForm.ShowMainPage();
        }

        private void _imageVideo_Click(object sender, EventArgs e)
        {
            // Log.LogUse(68);
            Common.OpenBrowser(Globals.GlobalResources.AppPullVideo);
        }
    }
}
