using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.Sql;

namespace QueryTalk.Mapper
{
    internal partial class Step5_VisualStudioUI : UserControl, ILateInitializer
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
            // Log.LogUse(40);
        }

        #endregion

        internal Step5_VisualStudioUI(MainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            Program.License.ShowFreeTrial(_ctrFreeTrial);
        }

        #region Navigation

        private void _linkNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mainForm.ShowMainPage();
        }

        private void _linkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mainForm.ShowPage(new Step4_QueryTalkBaseUI(_mainForm));
        }

        #endregion

        private void _btnOpenVS_Click(object sender, EventArgs e)
        {
            // Log.LogUse(50);
            _start.OpenVisualStudio(_mainForm);
        }
    }
}
