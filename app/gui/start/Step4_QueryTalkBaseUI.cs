using System;
using System.Windows.Forms;

namespace QueryTalk.Mapper
{
    internal partial class Step4_QueryTalkBaseUI : UserControl, ILateInitializer
    {
        private MainForm _mainForm;

        private Start _start
        {
            get
            {
                return Program.Start;
            }
        }

        #region ILateInitializer

        void ILateInitializer.Initialize()
        {
            // Log.LogUse(39);
        }

        #endregion

        internal Step4_QueryTalkBaseUI(MainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            Program.License.ShowFreeTrial(_ctrFreeTrial);

            // check if QueryTalkBase already exists:
            if (_start.HasQueryTalkBase())
            {
                SetDone("Database exists.", "Recreate");
            }

            // QueryTaklkBase creation event handler
            _start.QueryTalkBaseCreationCompleted += (o, e) =>
            {
                if (Program.Start.CurrentProcessException == null)
                {
                    _mainForm.RefreshDatabaseList(Common.QueryTalkBase);
                    SetDone("Done.", "Recreate");
                }
                else
                {
                    SetFailure();
                }
            };
        }

        private void SetDone(string message, string button)
        {
            // Log.LogUse(63);

            _iconOK.ThreadSafeInvoke(() =>
            {
                _ctrDone.Text = message;
                _ctrDone.Visible = true;
                _iconOK.Visible = true;
                _iconAjax.Visible = false;
                Start.DisableLink(_linkFailed);
                Start.DisableLink(_linkDont);
                Start.EnableButton(_btnCreate, button);
                Start.EnableLink(_linkShowDatabase);

                if (_start.CurrentMode == StartType.GetStarted)
                {
                    Start.EnableLink(_linkNext);
                }
            });
        }

        private void SetProcessing()
        {
            _btnCreate.ThreadSafeInvoke(() =>
            {
                _iconAjax.Visible = true;
                Start.DisableButton(_btnCreate);
                Start.DisableLink(_linkNext);
                Start.DisableLink(_linkFailed);
                _ctrDone.Visible = false;
                _ctrCreateLater.Visible = false;
                Start.DisableLink(_linkShowDatabase);
            });
        }

        private void SetFailure()
        {
            // Log.LogUse(64);

            _btnCreate.ThreadSafeInvoke(() =>
            {
                _iconAjax.Visible = false;
                Start.EnableButton(_btnCreate);
                Start.EnableLink(_linkNext);
                Start.EnableLink(_linkFailed);
                Start.DisableLink(_linkDont);
                Start.DisableLink(_linkShowDatabase);
                _ctrDone.Visible = false;
            });
        }

        private void _btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // Log.LogUse(49);
                SetProcessing();
                _start.StartCreateQueryTalkBase();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex);
            }
        }

        private void _linkDont_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Log.LogUse(65);
            _linkDont.Visible = false;
            _ctrCreateLater.Visible = true;
            Start.EnableLink(_linkNext);
        }

        private void _linkFailed_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_start.CurrentProcessException != null)
            {
                Common.ShowError(_start.CurrentProcessException);
            }
        }

        private void _linkSeeDatabase_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Log.LogUse(66);
            _start.ShowQueryTalkBase();
        }

        #region Navigation

        private void _linkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_start.CurrentMode == StartType.GetStarted)
            {
                _mainForm.ShowPage(new Step3_ServerUI(_mainForm));
            }
            else
            {
                _mainForm.ShowMainPage();
            }
        }

        private void _linkNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mainForm.ShowPage(new Step5_VisualStudioUI(_mainForm));
        }

        #endregion

        private void _imageVideo_Click(object sender, EventArgs e)
        {
            // Log.LogUse(70);
            Common.OpenBrowser(Globals.GlobalResources.AppQueryTalkBaseVideo);
        }
    }
}
