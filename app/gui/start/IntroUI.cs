using System;
using System.Windows.Forms;

namespace QueryTalk.Mapper
{
    internal partial class IntroUI : UserControl, ILateInitializer
    {
        private MainForm _mainForm;

        #region ILateInitializer

        void ILateInitializer.Initialize()
        {
            // Log.LogUse(30);
        }

        #endregion

        internal IntroUI(MainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            Program.License.ShowFreeTrial(_ctrFreeTrial);
        }

        private void _btnGetStarted_Click(object sender, EventArgs e)
        {
            _mainForm.ShowPage(new Step1_RepositoryUI(_mainForm));
        }
    }
}
