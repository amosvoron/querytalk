using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QueryTalk.Mapper
{
    internal partial class ResumeUI : UserControl, ILateInitializer
    {
        private MappingUI _mappingUI;

        #region ILateInitializer

        void ILateInitializer.Initialize()
        {
            // Log.LogUse(6);
        }

        #endregion

        internal ResumeUI(MappingUI mappingUI, List<string> excluded)
        {
            _mappingUI = mappingUI;
            InitializeComponent();

            var text = Common.RTBegin
                + @"The reason why certain objects are excluded is because they contain columns or parameters of \b non-supported\b0  data types (e.g. geography). "
                + @"If a database user under which the mapping is executed does not have permission to access a certain user-defined data type then some objects with the parameter(s) of a \b non-accessable\b0  data type may also be excluded."
                + Common.RTEnd;

            _messageBox.Rtf = text;
            _ctrExcluded.Lines = excluded.OrderBy(a => a).ToArray();
        }

        private void _linkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mappingUI.MainUI.ShowMainPage();
        }

        private void _imageVideo_Click(object sender, System.EventArgs e)
        {
            // Log.LogUse(74);
            Common.OpenBrowser(Globals.GlobalResources.AppExcludedObjectsVideo);
        }
    }
}
