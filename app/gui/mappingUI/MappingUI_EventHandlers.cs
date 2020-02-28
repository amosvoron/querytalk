using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using QueryTalk.Wall;

namespace QueryTalk.Mapper
{
    internal partial class MappingUI : UserControl
    {

        private void AddEventHandlers()
        {
            OnGo();
            OnComboConnection();
            OnComboDatabases();
            OnMapperTextChanged();
            OnUnderGoClick();
            OnMessageClick();
            OnSettingsClick();
            OnRepository();
            OnDeleteConnection();
            OnQueryTalkBaseClick();
            OnAboutClick();
            //OnRevalidateLicense();
            OnPull();
            OnVideoImage();
        }

        private void OnGo()
        {
            // Go:Click
            _btnGo.Click += (o, e) =>
            {
                try
                {
                    // reset message type
                    MessageType = MessageType.None;

                    // log every Go button click - !
                    // Log.LogUse(_btnGo.Text == "Map it!" ? 1 : 27, _comboDatabases.Text);

                    // check .NET path
                    if (!CheckNetDirectory())
                    {
                        return;
                    }

                    // check repository
                    if (!CheckRepository())
                    {
                        return;
                    }

                    // Finished, Stopped -> Idle
                    if (ProcessState == ProcessState.Finished || ProcessState == ProcessState.Stopped)
                    {
                        SetIdle();
                        return;
                    }

                    // Processing -> Stopped
                    if (ProcessState.IsProcessing())
                    {
                        if (_mapping != null && _mapping.Thread != null)
                        {
                            SetStopped();
                            return;
                        }
                    }

                    // validate
                    if (!ValidateGo())
                    {
                        return;
                    }

                    // create connection builder
                    _start.SetConnBuilder(_comboDatabases.Text);
                    var connBuilder = new SqlConnectionStringBuilder(_start.ConnBuilder.ToString());

                    // set processing and start mapping (on other thread)
                    SetProcessingTryConnection();
                    _mapping = new MappingHandler(this, CurrentGuid, connBuilder, _ctrMapper.Text);
                }
                catch (System.Exception ex)
                {
                    SetException(ex);
                }
            };

            // Go:MouseEnter
            _btnGo.MouseEnter += (o, e) =>
            {
                try
                {
                    if (_processState == ProcessState.Idle || _processState.IsProcessing())
                    {
                        _btnGo.BackColor = Color.Green;
                    }
                }
                catch { }  // do nothing
            };

            // Go:MouseLeave
            _btnGo.MouseLeave += (o, e) =>
            {
                try
                {
                    if (_processState == ProcessState.Idle || _processState.IsProcessing())
                    {
                        _SetGoDefault();
                    }
                }
                catch { }  // do nothing
            };
        }

        private void OnUnderGoClick()
        {
            // reinitialize
            _ctrUnderGo.Click += (o, e) =>
            {
                try
                {
                    SetIdle();
                    _ctrMessage.Visible = false;
                }
                catch (System.Exception ex)
                {
                    Common.ShowUnknownException(ex);
                }
            };
        }

        private bool _comboConnectionChangedIgnore = false;
        private void OnComboConnection()
        {
            _comboConnection.DropDown += (o, e) =>
            {
                SetComboConnection();
                _comboConnection.ForeColor = Color.Black;
            };

            _comboConnection.DropDownClosed += (o, e) =>
            {
                _comboConnection.ForeColor = Color.Gray;
            };

            // combo:SelectedIndexChanged          
            _comboConnection.SelectedIndexChanged += (o, e) =>
            {
                SetComboConnection();
                SetConnectionDeleteIcon();

                if (_comboConnectionChangedIgnore)
                {
                    _comboConnectionChangedIgnore = false;
                    return;
                }

                try
                {
                    _ctrLoadingDatabases.Visible = false;
                    var ix = _comboConnection.SelectedIndex;
                    if (ix != 0)
                    {
                        _iconDeleteConnection.Visible = true;
                        --ix;   // exclude first no-selection item
                        var connBuilder = Registry.ConnBuilders
                            // apply the same ordering as is in the connection combo (!)
                            .OrderBy(c => String.Format(@"[{0}].[{1}]{2}", c.DataSource, c.InitialCatalog, (c.IntegratedSecurity) ? "" : "@" + c.UserID))
                            .ToArray()[ix];

                        LockWhileConnecting();
                        MainUI.ServerUI.SetConnection(connBuilder);
                        MainUI.ServerUI.Execute(connBuilder.InitialCatalog);
                    }
                    else
                    {
                        _iconDeleteConnection.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    Common.ShowError(ex);
                }
            };
        }

        private void OnComboDatabases()
        {
            _comboDatabases.Click += (o, e) =>
            {
                if (_start.ConnBuilder != null)
                {
                    if (_comboDatabases.Items == null || _comboDatabases.Items.Count == 0)
                    {
                        ProvideDatabases();
                    }
                    else if (_start.Databases.Count > 0)
                    {
                        MainUI.RefreshDatabaseList(_comboDatabases.Text);
                        //_comboDatabases.DataSource = _start.Databases;
                    }
                }
                else
                {
                    MainUI.ShowStep3();
                }
            };

            // database:TextChanged
            _comboDatabases.TextChanged += (o, e) =>
            {
                try
                {
                    if (_start.AnyDatabase)
                    {
                        var dbName = Api.GetClrName(_comboDatabases.Text);
                        if (dbName == "_" || string.IsNullOrEmpty(_comboDatabases.Text))
                        {
                            dbName = null;  // reset empty name
                        }
                        _ctrMapper.Text = dbName;
                        _ctrMapperDll.ForeColor = Color.Green;
                        _ctrMapperDll.Text = String.Format("QueryTalk.Db.{0}.dll", dbName);
                    }
                }
                catch { }  // do nothing
            };

            _ctrMapper.TextChanged += (o, e) =>
            {
                if (string.IsNullOrEmpty(_ctrMapper.Text))
                {
                    _ctrMapperDll.ForeColor = Color.Red;
                    _ctrMapperDll.Text = "Name is required.";
                }
                else
                {
                    _ctrMapperDll.ForeColor = Color.Green;
                    _ctrMapperDll.Text = String.Format("QueryTalk.Db.{0}.dll", _ctrMapper.Text);
                }
            };

        }

        private void OnAboutClick()
        {
            //_linkAbout.Click += (o, e) =>
            //{
            //    try
            //    {
            //        MainUI.ShowAboutUI();
            //    }
            //    catch { }  // do nothing
            //};
        }

        private void OnQueryTalkBaseClick()
        {
            _linkQueryTalkBase.Click += (o, e) =>
            {
                MainUI.ShowPage(new Step4_QueryTalkBaseUI(MainUI));
            };
        }

        private void OnDeleteConnection()
        {
            _iconDeleteConnection.Click += (o, e) =>
            {
                try
                {
                    if (_comboConnection.SelectedIndex != 0)
                    {
                        RemoveSelectedConnection();
                    }
                }
                catch { }  // do nothing
            };

            _iconDeleteConnection.MouseEnter += (o, e) =>
            {
                try
                {
                    Cursor = Cursors.Hand;
                }
                catch { }
            };

            _iconDeleteConnection.MouseLeave += (o, e) =>
            {
                try
                {
                    Cursor = Cursors.Default;
                }
                catch { }
            };
        }

        private void OnRepository()
        {
            _linkOpenRepository.Click += (o, e) =>
            {
                try
                {
                    if (CheckRepository())
                    {
                        System.Diagnostics.Process.Start("explorer", _start.RepositoryPath);
                        // Log.LogUse(13);
                    }
                }
                catch { }  // do nothing
            };

            //_linkOpenRepository.MouseEnter += (o, e) =>
            //{
            //    try
            //    {
            //        Cursor = Cursors.Hand;
            //    }
            //    catch { }
            //};

            //_linkOpenRepository.MouseLeave += (o, e) =>
            //{
            //    try
            //    {
            //        Cursor = Cursors.Default;
            //    }
            //    catch { }
            //};

            _linkRepository.Click += (o, e) =>
            {
                try
                {
                    MainUI.ShowStep1();
                }
                catch { }  // do nothing
            };

        }

        private void OnSettingsClick()
        {
            _linkMapperSettings.Click += (o, e) =>
            {
                MainUI.ShowSettingsUI();
            };
        }

        private void OnMessageClick()
        {
            _ctrMessage.Click += (o, e) =>
            {
                try
                {
                    if (MessageType == MessageType.MissingObjectsInfo)
                    {
                        MainUI.ShowResumeUI(_nonCompliantObjects);
                    }
                    else
                    {
                        Common.ShowError(Exception, MessageType);
                    }
                }
                catch (Exception ex)
                {
                    Common.ShowUnknownException(ex);
                }
            };
        }

        private void OnMapperTextChanged()
        {
            // Namespace:TextChanged
            _ctrMapper.TextChanged += (o, e) =>
            {
                try
                {
                    if (CheckDbName())
                    {
                        //_ctrMapperDll.Visible = false;
                    }
                    else
                    {
                        _ctrMapperDll.ForeColor = Color.Red;
                        _ctrMapperDll.Text = "Wait, there must be some invalid character.";
                    }
                }
                catch { }  // do nothing
            };
        }

        //private void OnRevalidateLicense()
        //{
        //    _linkRevalidateLicense.Click += (o, e) =>
        //    {
        //        try
        //        {
        //            // Log.LogUse(56);
        //            MainUI.ShowAccountUI();
        //        }
        //        catch { }  // do nothing
        //    };
        //}

        private void OnPull()
        {
            //_linkPull.Click += (o, e) =>
            //{
            //    try
            //    {
            //        if (CheckRepository())
            //        {
            //            MainUI.ShowStep2();
            //        }            
            //    }
            //    catch { }  // do nothing
            //};
        }

        private void OnVideoImage()
        {
            //_imageVideo.MouseEnter += (o, e) =>
            //{
            //    this.Cursor = Cursors.Hand;
            //};

            //_imageVideo.MouseLeave += (o, e) =>
            //{
            //    this.Cursor = Cursors.Default;
            //};
        }

        #region Supported methods

        // provide repository
        internal bool CheckRepository()
        {
            if (Registry.CheckRepository())
            {
                return true;
            }
            else
            {
                MainUI.ShowStep1();
                return false;
            }
        }

        // remove connection when user presses (x)
        private void RemoveSelectedConnection()
        {
            try
            {
                var ix = _comboConnection.SelectedIndex;
                if (ix != 0)
                {
                    --ix;   // exclude first no-selection item
                    var connBuilder = Registry.ConnBuilders
                        // apply the same ordering as is in the connection combo (!)
                        .OrderBy(c => String.Format(@"[{0}].[{1}]{2}", c.DataSource, c.InitialCatalog, (c.IntegratedSecurity) ? "" : "@" + c.UserID))
                        .ToArray()[ix];

                    var connName = _comboConnection.Text;
                    Registry.RemoveConnBuilder(connBuilder);
                    Common.ShowNotification(String.Format("Connection {0} has been removed.", connName));
                    RefreshConnections();
                }
            }
            catch { }
        }

        #endregion

    }

}