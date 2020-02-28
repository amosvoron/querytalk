using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QueryTalk.Mapper
{
    internal partial class MainForm : Form
    {
        private UserControl _currentUI;
        private MappingUI _mappingUI;
        private AccountUI _accountUI;
        private Step1_RepositoryUI _step1UI;
        private Step2_PullUI _step2UI;
        private Step3_ServerUI _step3UI;

        private GuiType _guiType = GuiType.None;
        internal GuiType GuiType
        {
            get
            {
                return _guiType;
            }
            set
            {
                _guiType = value;
            }
        }

        internal Step3_ServerUI ServerUI
        {
            get
            {
                return _step3UI;
            }
        }

        internal AccountUI AccountUI
        {
            get
            {
                return _accountUI;
            }
        }

        // is mapping
        private bool _isCurrentMapping
        {
            get
            {
                return _currentUI != null && _currentUI is MappingUI;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            // Log.LogUse(2);
        }

        internal void Initialize()
        {
            _step1UI = new Step1_RepositoryUI(this);
            _step2UI = new Step2_PullUI(this);
            _step3UI = new Step3_ServerUI(this);

            //if (Program.Connect.AutoConnect)
            //{
            //    ShowAutoConnectUI();
            //}
            //else
            //{
            //    ShowAccountUI();
            //}

            //ShowIntroUI();
            OnAuthenticationSuccess();
        }

        internal void SetTitle()
        {
            this.Text = Program.License.AppTitle;
        }

        internal void RefreshDatabaseList(string currentDatabase)
        {
            _step3UI.RefreshDatabases(currentDatabase);
        }

        internal void OnAuthenticationSuccess()
        {
            try
            {
                Program.License.IsLimited = false;  // !important

                if (Program.Start.CurrentMode == StartType.GetStarted)
                {
                    ShowIntroUI();
                }
                else if (Program.Start.CurrentMode == StartType.Mapping)
                {
                    ShowMainPage();
                }
                // flexible: 
                else
                {
                    if (Program.Start.StartStep == 1)
                    {
                        ShowStep1();
                    }
                    if (Program.Start.StartStep == 2)
                    {
                        ShowStep2();
                    }
                    else if (Program.Start.StartStep == 3)
                    {
                        ShowStep3();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowUnknownException(ex);
            }
        }

        #region Generic Show Page

        internal void ShowPage(UserControl ui)
        {
            this.ThreadSafeInvoke(() =>
            {
                try
                {
                    if (!object.ReferenceEquals(_currentUI, ui))
                    {
                        ui.Dock = DockStyle.Fill;
                        _contentPanel.Controls.Remove(_currentUI);
                        _currentUI = ui;
                        _contentPanel.Controls.Add(_currentUI);
                    }

                    (ui as ILateInitializer).Initialize();
                }
                catch (Exception ex)
                {
                    Common.ShowUnknownException(ex);
                }
            });
        }

        #endregion

        #region Show Cached Pages

        internal void ShowAccountUI()
        {
            _guiType = GuiType.Account;
            if (_accountUI == null)
            {
                _accountUI = new AccountUI(this);
            }

            ShowPage(_accountUI);
        }

        internal void ShowMainPage()
        {
            _guiType = GuiType.MainPage;

            try
            {
                this.ThreadSafeInvoke(() =>
                {
                    if (_mappingUI == null)
                    {
                        _mappingUI = new MappingUI(this);
                    }
                   _mappingUI.SetAppEnvironment();   // !important

                    ShowPage(_mappingUI);
                });
            }
            catch (System.Exception ex)
            {
                Common.ShowUnknownException(ex);
            }
        }

        internal void ShowStep1()
        {
            ShowPage(_step1UI);
        }

        internal void ShowStep2()
        {
            ShowPage(_step2UI);
        }

        internal void ShowStep3(bool forceConnect = false)
        {
            ShowPage(_step3UI);
            if (forceConnect)
            {
                _step3UI.Execute();
            }
        }

        #endregion

        #region Show New Pages

        internal void ShowAutoConnectUI()
        {
            _guiType = GuiType.AutoConnect;

            this.ThreadSafeInvoke(() =>
            {
                try
                {
                    var ui = new AutoConnectUI(this);
                    ShowPage(ui);
                }
                catch (Exception ex)
                {
                    Common.ShowUnknownException(ex);
                }
            });
        }

        internal void ShowIntroUI()
        {
            _guiType = GuiType.Other;
            ShowPage(new IntroUI(this));
        }

        internal void ShowFreeTrialExpiredUI()
        {
            _guiType = GuiType.FreeTrialExpired;
            ShowPage(new FreeTrialExpiredUI(this));
        }

        internal void ShowResumeUI(List<string> excluded)
        {
            _guiType = GuiType.Other;
            ShowPage(new ResumeUI(_mappingUI, excluded));
        }

        internal void ShowNetDirectoryUI(string netPath)
        {
            _guiType = GuiType.Other;
            ShowPage(new NetDirectoryUI(_mappingUI, netPath));
        }

        internal void ShowSettingsUI()
        {
            _guiType = GuiType.Other;
            ShowPage(new SettingsUI(_mappingUI));
        }

        internal void ShowAboutUI()
        {
            _guiType = GuiType.Other;
            ShowPage(new AboutUI(_mappingUI));
        }

        #endregion

    }
}
