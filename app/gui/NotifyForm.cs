using System;
using System.Drawing;
using System.Windows.Forms;

namespace QueryTalk.Mapper
{
    // Common methods guarantee that exception argument (ex) is ALWAYS given!!!
    internal partial class NotifyForm : Form
    {
        private Exception _exception;
        private bool _isSendToServer = false;
        private string _stackTrace;

        internal NotifyForm(string message)
        {
            InitializeComponent();
            // Log.LogUse(26, message);

            _title.Text = "Notification";
            _title.ForeColor = Color.Black;
            _message.Text = message;
            _message.ForeColor = Color.Black;
            AddEventHandlers();
            TopMost = true;
        }

        internal NotifyForm(Exception ex)
        {
            InitializeComponent();
            _exception = ex;
            _stackTrace = ex.StackTrace;

            try
            {
                // SQL exception:
                if (ex != null && ex is QueryTalkException
                    && ((QueryTalkException)ex).ClrException != null
                    && ((QueryTalkException)ex).ClrException is System.Data.SqlClient.SqlException)
                {
                    _title.Text = "SQL exception";
                    _message.Text = ex.Message;
                }
                // other exceptions:
                else
                {
                    _title.Text = "Error";
                    _message.Text = ex.Message;
                }

                AddEventHandlers();

                Log.LogNotify(_title.Text, ex.Message);
            }
            catch
            {
                Close();
            }
        }

        internal NotifyForm(MessageType messageType)
        {
            InitializeComponent();

            try
            {
                AddEventHandlers();

                switch (messageType)
                {

                    case MessageType.NoMappingDataInfo:
                        _title.Text = "No mapping data.";
                        _message.Text = "Either the database is empty or the user does not have permission to access the database objects.";
                        _title.ForeColor = Color.Black;
                        _message.ForeColor = Color.Black;
                        Log.LogNotify(_title.Text);
                        return;

                    case MessageType.MissingObjectsInfo:
                        _title.Text = "Excluded objects";
                        _message.Text = "Some objects have been excluded.";
                        _title.ForeColor = Color.Black;
                        _message.ForeColor = Color.Black;
                        Log.LogNotify(_message.Text);
                        return;
                }
            }
            catch
            {
                Close();
            }
        }

        internal NotifyForm(MessageType messageType, Exception ex)
        {
            InitializeComponent();

            try
            {
                _exception = ex;
                AddEventHandlers();

                switch (messageType)
                {
                    case MessageType.ApplicationCrashed:
                        _exception = ex;
                        _title.Text = "Application has crashed.";
                        var text = Common.RTBegin
                            + @"We are sorry for the inconvenience." + Common.NL1
                            + @"In order to fix the error, the application will send the exception report to our server. No personal data will be included in the report."
                            + Common.RTEnd;
                        _message.Rtf = text;
                        _isSendToServer = true;
                        Log.LogNotify(_title.Text, ex.Message);
                        return;

                    case MessageType.UnknownError:
                        UnknownException(ex);
                        return;

                    case MessageType.MappingConnectionError:
                        _exception = ex;
                        _title.Text = "Connection failure";
                        _message.Text = ex.Message;
                        Log.LogNotify(_title.Text, ex.Message);
                        return;

                    case MessageType.SqlException:
                        _exception = ex;
                        _title.Text = "SQL exception";
                        _message.Text = ex.Message;
                        Log.LogNotify(_title.Text, ex.Message);
                        return;

                    case MessageType.CompilerError:
                        _title.Text = "Compiler exception";
                        _message.Text = ex.Message;
                        Log.LogNotify(_title.Text, ex.Message);
                        return;

                    default:
                        UnknownException(ex);
                        return;

                }
            }
            catch
            {
                Close();
            }
        }

        // unknown exception
        private void UnknownException(Exception ex)
        {
            _exception = ex;
            _title.Text = "Unknown error";
            var text = Common.RTBegin
                + @"We are sorry for the inconvenience." + Common.NL1
                + @"In order to fix the error, the application will send the exception report to our server. No personal data will be included in the report."
                + Common.RTEnd;
            _message.Rtf = text;
            _isSendToServer = true;
            // Log.LogUse(41, string.Format("{0}: {1}", _title.Text, ex.Message));
        }

        // add event handlers
        private void AddEventHandlers()
        {
            FormClosed += (o, e) =>
            {
                try
                {
                    if (_isSendToServer && _exception != null)
                    {
                        Log.LogError(_exception.Message, _exception.StackTrace);
                    }

                    Close();
                }
                catch { }  
            };
        }

        private void _btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
