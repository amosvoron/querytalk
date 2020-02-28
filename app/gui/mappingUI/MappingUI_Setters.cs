using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;


namespace QueryTalk.Mapper
{
    internal partial class MappingUI : UserControl
    {
        // Sets the mapping environment showing the appropriate title and footer 
        // depending on the FULL/LIMITED version
        internal void SetAppEnvironment()
        {
            //if (Program.License.IsLimited)
            //{
            //    _linkRevalidateLicense.Visible = true;
            //}
            //else if (!Program.License.HasAccount)
            //{
            //    _linkRevalidateLicense.Visible = true;
            //}
            //else
            //{
            //    _linkRevalidateLicense.Visible = false;
            //}

            _linkRevalidateLicense.Visible = false;
            MainUI.SetTitle();
        }

        internal void SetProgress(Guid guid, int increment)
        {
            _progressBar.ThreadSafeInvoke(() =>
            {
                // check Guid
                if (guid != CurrentGuid) { return; }

                _progressBar.Increment(increment);
            });
        }

        private void LockWhileConnecting()
        {
            _btnGo.ThreadSafeInvoke(() =>
            {
                _iconAjaxConnect.Visible = true;
                _linkServer.Visible = false;
                _btnGo.Enabled = false;
                _comboConnection.Enabled = false;
                _comboDatabases.Enabled = false;
                _ctrMapper.Enabled = false;
                _linkMapperSettings.Enabled = false;
                _linkQueryTalkBase.Enabled = false;
                _linkRepository.Enabled = false;
            });
        }

        private void Unlock()
        {
            _btnGo.ThreadSafeInvoke(() =>
            {
                _iconAjaxConnect.Visible = false;
                _linkServer.Visible = true;
                _btnGo.Enabled = true;
                _linkServer.Enabled = true;
                _comboDatabases.Enabled = true;
                _ctrMapper.Enabled = true;
                _linkMapperSettings.Enabled = true;
                _linkQueryTalkBase.Enabled = true;
                _linkRepository.Enabled = true;
                _ctrLoadingDatabases.Visible = false;
                _comboConnection.Enabled = true;
            });
        }

        // ProcessingTryConnection
        private void SetProcessingTryConnection()
        {
            SetProcessState(ProcessState.ProcessingTryConnection);

            _btnGo.ThreadSafeInvoke(() =>
            {
                _btnGo.Text = "Stop";
                _progressBar.Visible = true;
                _progressBar.Value = 0;
                _comboConnection.Enabled = false;
                _linkServer.Enabled = false;
                _comboDatabases.Enabled = false;
                _ctrMapper.Enabled = false;
                _watch.Start();

                // disable Mapper Settings
                _linkMapperSettings.Enabled = false;
                _linkQueryTalkBase.Enabled = false;
                _linkRepository.Enabled = false;
            });
        }

        // Processing
        internal void SetProcessing()
        {
            SetProcessState(ProcessState.Processing);
            HasConnectionPassed = true;
        }

        // Stopped
        private void SetStopped()
        {
            _mapping.Thread.Abort();    // !!! - Abort should stop the thread immediately (but not necesarrily)
                                        //       => we also use Guid to determine if calls from a certain thread 
                                        //          are made from the CURRENT process' thread.
            _btnGo.ThreadSafeInvoke(() =>
            {
                SetProcessState(ProcessState.Stopped);
                _btnGo.Text = "Stopped";
                _progressBar.Visible = false;
                ShowUnderGoReinitialize();
                _watch.Stop();
            });
        }

        // Idle
        internal void SetIdle()
        {
            _btnGo.ThreadSafeInvoke(() =>
            {
                SetProcessState(ProcessState.Idle);
                _btnGo.Text = "Map it!";
                _SetGoDefault();
                ShowUnderGo(false);
                _progressBar.Visible = false;
                _progressBar.Value = 0;
                _watch.Reset();
                _ctrElapsed.Text = "(0 sec)";
                _ctrElapsed.Visible = false;
                _comboConnection.Enabled = true;
                _linkServer.Enabled = true;
                _comboDatabases.Enabled = true;
                _ctrMapper.Enabled = true;
                _panelMapper.Visible = false;

                // activate Go button
                _btnGo.Enabled = true;
            });
        }

        // Finished (last method in the thread)
        internal Decimal SetFinished(Guid guid)
        {
            // check Guid
            if (guid != CurrentGuid) { return 0; }
            var elapsed = (Decimal)_watch.ElapsedMilliseconds / 1000;

            _btnGo.ThreadSafeInvoke(() =>
            {
                SetProcessState(ProcessState.Finished);
                _watch.Stop();
                _btnGo.Text = "Finished";
                _btnGo.ForeColor = Color.White;
                _btnGo.BackColor = Color.FromArgb(0, 175, 0);
                ShowUnderGoReinitialize();
                _progressBar.Visible = false;
                _ctrElapsed.Text = String.Format("({0} sec)", elapsed);
                _ctrElapsed.Visible = true;

                // handle non-compliant objects
                _nonCompliantObjects = _mapping.GetNonCompliantObjects();
                if (_nonCompliantObjects.Count > 0)
                {
                    ShowMessage(true, "Some objects have been excluded.", MessageType.MissingObjectsInfo);
                }

                SetDemo();
                _panelMapper.Visible = true;
            });

            return elapsed;
        }

        // Finished (but no database info data)
        internal void SetFinishedWithNoData(Guid guid)
        {
            // check Guid
            if (guid != CurrentGuid) { return; }

            _btnGo.ThreadSafeInvoke(() =>
            {
                SetProcessState(ProcessState.Finished);
                _watch.Stop();
                _btnGo.Text = "Finished";
                _btnGo.ForeColor = Color.White;
                _btnGo.BackColor = Color.FromArgb(0, 175, 0);
                ShowUnderGoReinitialize();
                ShowMessage(true, "No mapping data.", MessageType.NoMappingDataInfo);
                _progressBar.Visible = false;
                _ctrElapsed.Text = String.Format("({0} sec)", (Decimal)_watch.ElapsedMilliseconds / 1000);
                _ctrElapsed.Visible = true;
            });
        }

        // Finished (but no database info data)
        internal void SetNoConnection(Guid guid)
        {
            // check Guid
            if (guid != CurrentGuid) { return; }

            _btnGo.ThreadSafeInvoke(() =>
            {
                SetProcessState(ProcessState.Failed);
                _btnGo.Text = "Failed";
                _SetGoDefault();
                _progressBar.Visible = false;
                ShowUnderGoReinitialize();
                ShowMessage(true, "A connection to the database could not be established.", MessageType.MappingConnectionError);
                _watch.Stop();
            });
        }

        // failed (unknown exception)
        internal void SetException(Exception ex)
        {
            _btnGo.ThreadSafeInvoke(() =>
            {
                Exception = ex;
                _btnGo.Text = "Failed";
                _SetGoDefault();
                _progressBar.Visible = false;
                ShowUnderGoReinitialize();

                // is SQL exception
                if (ex is System.Data.SqlClient.SqlException
                    || (ex is QueryTalkException
                        && ((QueryTalkException)ex).ClrException != null && ((QueryTalkException)ex).ClrException is System.Data.SqlClient.SqlException))
                {
                    ShowMessage(true, "The mapping failed. SQL exception occured.", MessageType.SqlException);
                }
                // compiler exception
                else if (ProcessState == Mapper.ProcessState.Compiling)
                {
                    ShowMessage(true, "The mapping failed. The compiler threw an exception.", MessageType.CompilerError);
                }
                // unknown exception
                else
                {
                    ShowMessage(true, "The mapping failed.", MessageType.UnknownError);
                }
                _watch.Stop();

                // processing state -> Failed
                SetProcessState(ProcessState.Failed);
            });
        }

        // default button color
        private void _SetGoDefault()
        {
            _btnGo.ForeColor = Color.White;
            _btnGo.BackColor = Color.FromArgb(15, 156, 202);
        }

        private void SetComboConnection()
        {
            //_comboConnection.ThreadSafeInvoke(() =>
            //{
            //    if (_comboConnection.Items.Count > 1 && _comboConnection.Size.Width != 490)
            //    {
            //        _comboConnection.Size = new Size(490, 33);
            //    }
            //});
        }

        private void SetConnectionDeleteIcon()
        {
            _iconDeleteConnection.ThreadSafeInvoke(() =>
            {
                if (_comboConnection.SelectedIndex != 0)
                {
                    _iconDeleteConnection.Visible = true;
                }
                else
                {
                    _iconDeleteConnection.Visible = false;
                }
            });
        }

        private void SetDemo()
        {
            // init
            _panelMapperDemoCode.Clear();
            var classColor = Color.FromArgb(79, 146, 180);
            var identifierColor = Color.Blue;
            var stringColor = Color.FromArgb(163, 21, 21);
            var mapper = _ctrMapper.Text;
            _panelMapperFile.Text = _ctrMapperDll.Text;

            int len; string text;

            // using...
            len = _panelMapperDemoCode.TextLength;
            text = "using";
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = identifierColor;

            // ... QueryTalk
            len = _panelMapperDemoCode.TextLength;
            text = " QueryTalk;   ";
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = Color.Black;

            // comment #1
            len = _panelMapperDemoCode.TextLength;
            text = "// reference QueryTalk.dll\r\n";
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = Color.Green;

            // using...
            len = _panelMapperDemoCode.TextLength;
            text = "using ";
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = identifierColor;

            // s
            len = _panelMapperDemoCode.TextLength;
            text = "s";
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = classColor;

            // = QueryTalk.Db.
            len = _panelMapperDemoCode.TextLength;
            text = " = QueryTalk.Db.";
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = Color.Black;

            // <mapper>
            len = _panelMapperDemoCode.TextLength;
            text = mapper;
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = classColor;

            // ;
            len = _panelMapperDemoCode.TextLength;
            text = ";   ";
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = Color.Black;

            // comment #2
            len = _panelMapperDemoCode.TextLength;
            text = string.Format("// reference {0}\r\n", _ctrMapperDll.Text);
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = Color.Green;

            _panelMapperDemoCode.AppendText("...\r\n");

            // d
            len = _panelMapperDemoCode.TextLength;
            text = "d";
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = classColor;

            // .SetConnection
            len = _panelMapperDemoCode.TextLength;
            text = ".SetConnection(";
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = Color.Black;

            // "<connString>"
            len = _panelMapperDemoCode.TextLength;
            text = string.Format("@\"{0}\"", _start.ConnBuilder.ToString());
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = stringColor;

            // );
            len = _panelMapperDemoCode.TextLength;
            text = ");\r\n";
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = Color.Black;

            // s
            len = _panelMapperDemoCode.TextLength;
            text = "s";
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = classColor;

            // <SampleTable>
            len = _panelMapperDemoCode.TextLength;
            text = string.Format(".{0}.Test();   ", _start.SampleTable ?? "<MyTable>");
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = Color.Black;

            // comment #3
            len = _panelMapperDemoCode.TextLength;
            text = "// load sample table in testing environment";
            _panelMapperDemoCode.AppendText(text);
            _panelMapperDemoCode.SelectionStart = len;
            _panelMapperDemoCode.SelectionLength = text.Length;
            _panelMapperDemoCode.SelectionColor = Color.Green;
        }

    }

}