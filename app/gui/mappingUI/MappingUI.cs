using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using QueryTalk.Wall;

namespace QueryTalk.Mapper
{
    internal partial class MappingUI : UserControl, ILateInitializer
    {
        private bool _netPathReadFromRegistry;
        private const string _queryTalkDefaultBase = "QueryTalkBase";
        private string _netPath = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319";
        private Color _linkColor = Color.FromArgb(220, 234, 239);

        internal string NET_PATH
        {
            get
            {
                if (!_netPathReadFromRegistry)
                {
                    Registry.ReadNetDirectory();
                    if (Registry.NetPath != null)
                    {
                        _netPath = Registry.NetPath;
                    }
                    _netPathReadFromRegistry = true;
                }
                return _netPath;
            }
            set
            {
                if (value != _netPath && value != null)
                {
                    Registry.WriteNetDirectory(value);
                    _netPath = value;
                }
            }
        }

        private Start _start
        {
            get
            {
                return Program.Start;
            }
        }

        // process state
        private ProcessState _processState = ProcessState.Idle;
        internal ProcessState ProcessState 
        {
            get
            {
                return _processState;
            }
        }
        internal void SetProcessState(ProcessState processState)
        {
            _processState = processState;
            if (processState != ProcessState.Processing && processState != ProcessState.Compiling)
            {
                CurrentGuid = Guid.NewGuid();   // every process state "anulate" the working thread 
                                                // (except ProcessingTryConnection -> Processing)
            }

            // deactive the Go button (to prevent not intented clicks)
            if (processState == ProcessState.Failed
                || processState == ProcessState.Finished
                || processState == ProcessState.Stopped)
            {
                _btnGo.ThreadSafeInvoke(() =>
                    {
                        _btnGo.Enabled = false;
                    });
            }
        }

        // MainForm
        internal MainForm MainUI { get; set; }

        // Guid of the current process
        internal Guid CurrentGuid { get; private set; }

        // registry
        internal RegistryHandler Registry { get; private set; }

        // any exception that is thrown and caught
        internal System.Exception Exception { get; set; }

        // has error ocurred while testing the connection
        internal bool HasConnectionPassed { get; set; }

        #region Assembly Settings

        // true if assembly settings are set by user (not default)
        internal bool HasSettings
        {
            get
            {
                return ((AssemblyVersion != null && (AssemblyVersion.Item1 != 0 || AssemblyVersion.Item2 != 0 || AssemblyVersion.Item3 != 0 || AssemblyVersion.Item4 != 0))
                    || (FileVersion != null && (FileVersion.Item1 != 0 || FileVersion.Item2 != 0 || FileVersion.Item3 != 0 || FileVersion.Item4 != 0))
                    || (KeyFile != null));
            }
        }
        internal Tuple<int, int, int, int> AssemblyVersion { get; set; }
        internal Tuple<int, int, int, int> FileVersion { get; set; }
        private void ResetSettings()
        {
            AssemblyVersion = null;
            FileVersion = null;
            KeyFile = null;
            RefreshLinks();
        }
        internal KeyFile KeyFile { get; set; }
        internal void RefreshLinks()
        {
            _linkMapperSettings.Enabled = true;
            _linkQueryTalkBase.Enabled = true;
            _linkRepository.Enabled = true;

            if (HasSettings)
            {
                _linkMapperSettings.LinkColor = Color.Green;
            }
            else
            {
                _linkMapperSettings.LinkColor = Color.FromName("HotTrack");
            }
        }

        #endregion

        // message type
        internal MessageType MessageType;

        private List<string> _reserved;
        private MappingHandler _mapping;
        private System.Diagnostics.Stopwatch _watch;
        private List<string> _nonCompliantObjects;
        private Dictionary<string, string[]> _serverDatabases;    // <server, database>
        //private int _selectedDatabaseIndex;

        #region ILateInitializer

        private bool _initialized;
        void ILateInitializer.Initialize()
        {
            // Log.LogUse(5);
            Program.Start.Registry.WriteGetStartedDone();

            _start.CurrentMode = StartType.Mapping;

            if (_initialized)
            {
                _ctrLoadingDatabases.ThreadSafeInvoke(() =>
                {
                    _ctrLoadingDatabases.Visible = false;
                });

                return;
            }

            // event handler: ConnectionCheckCompleted
            _start.ConnectionCheckCompleted += (o, e) =>
            {
                Unlock();

                if (e.Exception != null)
                {
                    SetServer(true);
                    return;
                }

                SetServer();
                _comboDatabases.ThreadSafeInvoke(() =>
                {
                    if (_start.HasDatabase && _start.Databases.Count > 0)
                    {
                        if (!_start.Databases.Where(a => a.ToUpperInvariant() == _start.InitialCatalog.ToUpperInvariant()).Any())
                        {
                            Common.ShowNotification(string.Format("Database {0} cannot be found.", _start.InitialCatalog));
                        }

                        SetDatabase(_start.InitialCatalog);
                    }
                });

                //Unlock();
            };

            SetServer();
            SetDatabase(_start.InitialCatalog);

            _initialized = true;
        }

        #endregion

        // ctor (try-catch)
        internal MappingUI(MainForm mainForm)
        {
            try
            {
                MainUI = mainForm;
                InitializeComponent();
                Program.License.ShowFreeTrial(_ctrFreeTrial);
                Init();
                AddEventHandlers();

                // reserved names (for QueryTalk.Db.<db> namespace)
                _reserved = new List<string>(new[] { "QueryTalk", "Db", "Map" });

                AssemblyVersion = Tuple.Create(0, 0, 0, 0);
                RefreshLinks();
                //CheckVersions();
            }
            catch (Exception ex)
            {
                Common.ShowUnknownException(ex);
            }
        }

        private void Init()
        {
            // registry
            Registry = new RegistryHandler();
            Registry.ReadConnections();
            RefreshConnections();

            // tooltips
            _linkMapperSettings.SetToolTip("Set mapper", "Set the mapper assembly settings.");
            _iconDeleteConnection.SetToolTip("Remove connection", "Remove connection from the connection list.");
            _linkRepository.SetToolTip("Set repository", "Set the QueryTalk repository.", 10);
            _linkOpenRepository.SetToolTip("Open repository", "Open the QueryTalk repository folder.");
            _panelMapperOpenRepository.SetToolTip("Open repository", "Open the QueryTalk repository folder.");
            _linkQueryTalkBase.SetToolTip("Create QueryTalkBase",
                "Create the QueryTalkBase database.\r\nIt is strongly recommended to create it as it is used in all code examples.",
                20);
            //_linkHelp.SetToolTip("Help", "Read how to use this application");
            //_linkAbout.SetToolTip("About", "About this application");
            //_ctrConnStringNotification.SetToolTip("ConnectionString field", "You can also enter the connection string in the server field", 10);
            //_linkPull.SetToolTip("Pull", "Pull the library and other files from the server\r\n" +
            //   "and store them in the repository.", 10);
            _comboDatabases.SetToolTip("Show databases", "Get the list of all databases from the SQL server.");
            _linkCodeExamples.SetToolTip("Code examples", "Open the demo project with code examples in the Visual Studio.");

            //_imageProblemsVideo.Size = new Size(151, 108);
            //_imageVideo.SetToolTip("Have problems?", "Learn how to use this page in 1 minute.");

            // watch
            _watch = new System.Diagnostics.Stopwatch();
            _serverDatabases = new Dictionary<string, string[]>();

            _comboConnection.DropDownHeight = 300;
            _comboDatabases.DropDownHeight = 255;

            _SetGoDefault();
        }

        private void SetServer(bool failed = false)
        {
            _linkServer.ThreadSafeInvoke(() =>
            {
                if (failed)
                {
                    _linkServer.Text = String.Format("{0} (connection failed)", _start.ConnBuilder.DataSource);
                    _linkServer.LinkColor = Color.Red;
                    _comboDatabases.DataSource = null;
                    _ctrChooseDatabase.ForeColor = Color.Red;
                    return;
                }

                // double connection check (first is done in Start.SetCurrentMode method)
                if (_start.ConnBuilder == null)
                {
                    MainUI.ShowStep3();
                    return;
                }

                // connection is given:

                // set server
                _linkServer.Text = _start.ConnBuilder.DataSource;
                _linkServer.LinkColor = Color.FromName("HotTrack");

                // set combo databases
                if (_start.Databases.Count > 0)
                {
                    _ctrChooseDatabase.ForeColor = Color.Black;
                    _comboDatabases.DataSource = _start.Databases.ToArray();
                }
                else
                {
                    // the main page is not initialized yet => do the database load
                    if (!_initialized)
                    {
                        ProvideDatabases();
                    }
                    else
                    {
                        _ctrChooseDatabase.ForeColor = Color.Red;
                    }
                }
            });
        }

        // provide database for combo control
        // anticipated:
        //    - ConnBuild is given (from Registry).
        //    - Databases has not been loaded yet.
        internal void ProvideDatabases()
        {
            _ctrLoadingDatabases.ThreadSafeInvoke(() =>
            {
                _ctrLoadingDatabases.Visible = true;
                LockWhileConnecting();
                MainUI.ServerUI.SetConnection(_start.ConnBuilder);
                MainUI.ServerUI.Execute(_start.InitialCatalog);
            });
        }

        private void SetDatabase(string database)
        {
            // check if combo is loaded; if not => load it
            if (_comboDatabases.Items.Count == 0 && _start.AnyDatabase)
            {
                _comboDatabases.DataSource = _start.Databases;
            }

            if (!string.IsNullOrEmpty(database))
            {
                _comboDatabases.Text = database;
            }
        }

        internal void AddAndSetQueryTalkBase()
        {

            //SetServer();
            //SetDatabase(Common.QueryTalkBase);

            _comboDatabases.ThreadSafeInvoke(() =>
            {
                if (!_start.Databases.Contains(Common.QueryTalkBase))
                {
                    _start.Databases.Add(Common.QueryTalkBase);
                }

                _comboDatabases.DataSource = _start.Databases.ToArray();
                _comboDatabases.Text = Common.QueryTalkBase;
            });
        }

        // NOT IN USE
        private void CheckVersions()
        {
            var token = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                Exception exception;

                try
                {
                    var pull = new Pull(token);
                    var serverVersion = pull.GetServerVersion(out exception);
                    if (exception == null)
                    {
                        // lib
                        if (_start.RepositoryPath != null)
                        {
                            if (Pull.GetClientLibVersion() != Common.NoFile)
                            {
                                if (serverVersion.CompareToLibVersion(Pull.GetClientLibVersion()) == 1)
                                {
                                    ShowLibraryNotification(true, String.Format("New library version {0} is available", serverVersion.LibVersion));
                                }
                            }
                        }
                    }
                }
                catch { }
            }, token.Token);
        }

        // refresh connection combo
        internal void RefreshConnections(SqlConnectionStringBuilder connBuilder = null)
        {
            _comboConnection.ThreadSafeInvoke(() =>
            {
                // clear
                _comboConnection.Items.Clear();

                var items = new List<string>();
                var ix = 1;
                var selectedIndex = 0;
                Registry.ConnBuilders
                    .OrderBy(a => a.DataSource)
                    .ThenBy(a => a.InitialCatalog)
                    .ThenBy(a => a.UserID)
                    .ToList()
                    .ForEach(builder =>
                    {
                        items.Add(String.Format(@"[{0}].[{1}]{2}", builder.DataSource, builder.InitialCatalog, (builder.IntegratedSecurity) ? "" : "@" + builder.UserID));

                        // store conn builder index
                        if (selectedIndex == 0 && connBuilder != null)
                        {
                            if (builder.ToString().ToUpperInvariant() == connBuilder.ToString().ToUpperInvariant())
                            {
                                selectedIndex = ix;
                            }
                        }

                        ++ix;
                    });

                if (items.Count == 0)
                {
                    _comboConnection.Items.Add("(no stored connections available)");
                    _comboConnection.SelectedIndex = 0;
                }
                else
                {
                    _comboConnection.Items.Add("(choose stored connection)");
                    _comboConnection.Items.AddRange(items.OrderBy(a => a).ToArray());

                    if (selectedIndex != 0)
                    {
                        _comboConnectionChangedIgnore = true;
                    }

                    _comboConnection.SelectedIndex = selectedIndex;
                }

            });
        }

        internal bool CheckNetDirectory()
        {
            // check if path is valid
            if (!Common.IsPathValid(NET_PATH))
            {
                MainUI.ShowNetDirectoryUI(NET_PATH);
                return false;
            }

            //if (File.Exists(Path.Combine(NET_PATH, "System.dll")))
            if (Common.CheckNetDirectory(NET_PATH))
            {
                return true;
            }

            // check default 64
            if (Common.CheckNetDirectory(Common.NetPath64))
            {
                NET_PATH = Common.NetPath64;
                return true;
            }

            // check default 32
            if (Common.CheckNetDirectory(Common.NetPath32))
            {
                NET_PATH = Common.NetPath32;
                return true;
            }

            MainUI.ShowNetDirectoryUI(NET_PATH);
            return false;
        }

        #region Validate

        // validate the connection data
        private bool ValidateGo()
        {
            // server
            if (String.IsNullOrWhiteSpace(_linkServer.Text))
            {
                ShowValidationError("The server name is missing.");
                _linkServer.Focus();
                return false;
            }

            // database
            if (String.IsNullOrWhiteSpace(_comboDatabases.Text))
            {
                ShowValidationError("Database is missing.\r\n\r\nSelect the database from the list.");
                _comboDatabases.Focus();
                return false;
            }

            // database=tempdb
            if (_comboDatabases.Text.EqualIgnoreCase("tempdb"))
            {
                ShowValidationError("It is not allowed to map the [tempdb] database.");
                return false;
            }

            // mapper: empty
            if (String.IsNullOrWhiteSpace(_ctrMapper.Text))
            {
                ShowValidationError("Mapper name is required.");
                return false;
            }

            // mapper
            if (!CheckDbName())
            {
                ShowValidationError("Mapper name is invalid..\r\nUse alphanumeric characters and underscore. No spaces. First character should not be a digit.\r\n\r\nThe name can also be reserved (e.g. QueryTalk, Db, Map).");
                return false;
            }
       
            return true;
        }

        private void ShowValidationError(string message)
        {
            Common.ShowNotification(message);
        }

        private bool CheckDbName()
        {
            var name = _ctrMapper.Text;

            if (String.IsNullOrWhiteSpace(name))
            {
                return true;  // no name is valid
            }

            if (!Api.IsValidClrName(name))
            {
                return false;
            }
            // QueryTalk reserved names for database { QueryTalk, Db, Map }
            else if (_reserved.Contains(name))
            {
                _ctrMapper.Text += ClrName.RENAMED;
                return true;
            }

            return true;
        }

        #endregion

        #region Show

        // set undergo text
        internal void ShowUnderGo(bool visible, string text = null, bool reinitialize = false)
        {
            _ctrMessage.ThreadSafeInvoke(() =>
            {
                // first line
                _ctrUnderGo.Text = text;
                _ctrUnderGo.Visible = visible;
                _ctrUnderGo.LinkColor = Color.DimGray;
                _ctrUnderGo.Enabled = false;

                // reinitialize
                if (reinitialize)
                {
                    //_ctrUnderGo.LinkColor = Color.FromName("HotTrack");
                    _ctrUnderGo.LinkColor = Color.Green;
                    _ctrUnderGo.Enabled = true;
                }

            });
        }

        // set undergo as reinitialize
        private void ShowUnderGoReinitialize()
        {
            ShowUnderGo(true, "Click to reinitialize", true);

            // show Assembly Settings
            RefreshLinks();
        }

        // set message
        internal void ShowMessage(bool visible, string text, MessageType messageType)
        {
            _ctrMessage.ThreadSafeInvoke(() =>
            {
                MessageType = messageType;  // store message type

                _ctrMessage.Text = text + " Click for more info.";
                _ctrMessage.Visible = visible;

                // is warning
                switch (messageType)
                {
                    case MessageType.MissingObjectsInfo:
                        _ctrMessage.LinkColor = Color.Red;
                        break;
                    case MessageType.NoMappingDataInfo:
                        _ctrMessage.LinkColor = Color.Red;
                        break;
                    case MessageType.MappingConnectionError:
                    case MessageType.SqlException:
                    case MessageType.CompilerError:
                    case MessageType.UnknownError:
                        _ctrMessage.LinkColor = Color.Red;
                        break;
                    default:
                        _ctrMessage.LinkColor = Color.Black;
                        break;
                }
            });
        }        

        // NOT IN USE
        internal void ShowLibraryNotification(bool show, string message = null)
        { }

        #endregion

        private void _linkServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ctrLoadingDatabases.Visible = false;
            MainUI.ShowStep3();
        }

        private void _panelMapperOpenRepository_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
        }

        private void _linkCodeExamples_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Log.LogUse(75);
            _start.OpenVisualStudio(MainUI);
        }

        private void _panelMapperCodeExamplesVS_Click(object sender, EventArgs e)
        {
            // Log.LogUse(75);
            _start.OpenVisualStudio(MainUI);
        }

        private void _imageVideo_Click(object sender, EventArgs e)
        {
            // Log.LogUse(55);
            Common.OpenBrowser(Globals.GlobalResources.AppMapDatabaseVideo);
        }
    }
}
