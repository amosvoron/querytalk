using System;
using System.Windows.Forms;
using QueryTalk.Security;

namespace QueryTalk.Mapper
{
    internal partial class FreeTrialExpiredUI : UserControl, ILateInitializer
    {
        private MainForm _mainForm;

        #region ILateInitializer

        void ILateInitializer.Initialize()
        {
            // Log.LogUse(35);
        }

        #endregion

        internal FreeTrialExpiredUI(MainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
        }

        private void _linkAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mainForm.ShowAccountUI();
        }

        private void _btnRegister_Click(object sender, EventArgs e)
        {
            // Log.LogUse(47);

            try
            {
                System.Diagnostics.Process.Start(Globals.GlobalResources.AppRegisterUrl);
            }
            catch { }
        }
    }
}
