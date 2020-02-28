using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using QueryTalk;
using QueryTalk.Security;

namespace QueryTalk.Mapper
{
    internal class Start
    {
        private object _locker = new object();

        public event EventHandler LocalServersCompleted;
        public event EventHandler<ConnectionCheckEventArgs> ConnectionCheckCompleted;
        public event EventHandler<ConnectionCheckEventArgs> LoadDatabasesCompleted;
        public event EventHandler QueryTalkBaseCreationCompleted;
        public event EventHandler MappingCompleted;

        #region Visual Studio

        private static readonly string _codeExamplesProject = @"QueryTalkDemo\QueryTalkDemo.csproj";

        internal void OpenVisualStudio(MainForm mainForm)
        {
            try
            {
                var path = Path.Combine(RepositoryPath, _codeExamplesProject);

                if (!File.Exists(path))
                {
                    Common.ShowNotification("The QueryTalkDemo project cannot be found.\r\n\r\nPlease pull the files again.");
                    mainForm.ShowStep2();
                }
                else
                {
                    System.Diagnostics.Process.Start(path);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex);
            }
        }

        #endregion

        private StartType _currentMode;
        internal StartType CurrentMode
        {
            get
            {
                return _currentMode;
            }
            set
            {
                _currentMode = value;
            }
        }

        private int _startStep;
        internal int StartStep
        {
            get
            {
                return _startStep;
            }
        }

        private string[] _localServers;
        internal string[] LocalServers
        {
            get
            {
                return _localServers;
            }
        }

        internal RegistryHandler Registry { get; private set; }

        internal bool HasLocalServers
        {
            get
            {
                lock (_locker)
                {
                    return _localServers != null && _localServers.Length > 0;
                }
            }
        }

        private string _repositoryPath;
        internal string RepositoryPath
        {
            get
            {
                return _repositoryPath;
            }
            set
            {
                if (value != null)
                {
                    _repositoryPath = value;
                    Registry.WriteRepository(_repositoryPath);
                }
            }
        }

        //private SqlConnectionStringBuilder _prevConnBuilder;

        private SqlConnectionStringBuilder _connBuilder;
        internal SqlConnectionStringBuilder ConnBuilder
        {
            get
            {
                return _connBuilder;
            }
            //private set
            //{
            //    if (value != null)
            //    {
            //        //_prevConnBuilder = _connBuilder;
            //        _connBuilder = value;
            //        Registry.WriteDefaultConnection(value.ToString());
            //    }
            //}
        }

        internal string InitialCatalog
        {
            get
            {
                if (_connBuilder != null)
                {
                    return _connBuilder.InitialCatalog;
                }

                return null;
            }
        }

        internal void ResetTest()
        {
            _connBuilder = null;
            Databases = null;
        }

        private SqlConnectionStringBuilder _masterConnBuilder
        {
            get
            {
                if (_connBuilder == null)
                {
                    return null;
                }

                var connBuilder = new SqlConnectionStringBuilder(_connBuilder.ToString());
                connBuilder.InitialCatalog = "master";
                return connBuilder;
            }
        }

        internal void SetConnBuilder(SqlConnectionStringBuilder connBuilder)
        {
            if (connBuilder == null)
            {
                return;
            }

            _connBuilder = new SqlConnectionStringBuilder(connBuilder.ToString());
            Registry.WriteDefaultConnection(_connBuilder.ToString());
        }

        internal void SetConnBuilder(string database)
        {
            if (!string.IsNullOrEmpty(database))
            {
                _connBuilder.InitialCatalog = database;
                Registry.WriteDefaultConnection(_connBuilder.ToString());
            }
        }

        internal bool IsServerChanged(SqlConnectionStringBuilder connBuilder)
        {
            if (_connBuilder == null)
            {
                return true;
            }

            return string.Compare(connBuilder.DataSource, _connBuilder.DataSource, true) != 0;
        }

        internal bool HasConnection
        {
            get
            {
                lock (_locker)
                {
                    return ConnBuilder != null;
                }
            }
        }

        internal bool HasDatabase
        {
            get
            {
                lock (_locker)
                {
                    return ConnBuilder != null && ConnBuilder.InitialCatalog != null;
                }
            }
        }

        internal List<string> Databases { get; private set; }

        internal bool AnyDatabase
        {
            get
            {
                lock (_locker)
                {
                    return Databases != null && Databases.Count > 0;
                }
            }
        }

        // first table of the mapped database (used only in MapperDemo - on mapping finished)
        internal string SampleTable { get; set; }

        internal Guid _LoadDatabasesToken;

        internal Exception CurrentProcessException { get; private set; }

        internal Exception LocalServersException { get; private set; }

        internal Exception PullException { get; private set; }

        internal byte[] PullBytes { get; set; }

        internal Start()
        {
            ReadRegistry();
        }

        internal void Initialize()
        {
            SetCurrentMode();
            StartSearchingForLocalServers();
            StartPull();
            Databases = new List<string>();
        }

        #region Registry

        private void ReadRegistry()
        {
            Registry = new RegistryHandler();
            _repositoryPath = Registry.GetRepository();
            _connBuilder = Registry.GetDefaultConnection();
        } 

        private void SetCurrentMode()
        {
            // GetStarted
            if (!Registry.ReadGetStartedDone())
            {
                _currentMode = StartType.GetStarted;
                return;
            }

            // Flexible
            if (RepositoryPath == null)
            {
                _currentMode = StartType.Flexible;
                _startStep = 1;
            }
            else if (!Pull.LibraryExists)
            {
                _currentMode = StartType.Flexible;
                _startStep = 2;
            }
            else if (ConnBuilder == null)
            {
                _currentMode = StartType.Flexible;
                _startStep = 3;
            }
            // Mapping
            else
            {
                _currentMode = StartType.Mapping;
            }
        }

        //internal bool LoadDatabases()
        //{
        //    try
        //    {
        //        if (ConnBuilder == null)
        //        {
        //            throw new Exception("Connection is not defined. Connect to the SQL Server first.");
        //        }

        //        d.SetConnection(connKey => new ConnectionData(_masterConnBuilder.ToString(), 0));
        //        Databases = d
        //            .From("sys.sysdatabases")
        //            .OrderBy("name")
        //            .Select("name")
        //            .Go<Row<string>>()
        //            .Select(a => a.Column1)
        //            .ToList();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Common.ShowOtherError(ex);    // DO NOT SHOW THIS ERROR !!!
        //        return false;
        //    }
        //}

        #endregion

        #region Start methods (async)

        internal void StartSearchingForLocalServers()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var localServers = new List<string>();
                    var dataSourceEnumerator = SqlDataSourceEnumerator.Instance;
                    DataTable table = dataSourceEnumerator.GetDataSources();

                    if (table == null || table.Rows.Count == 0)
                    {
                        return;
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        var col1 = row["ServerName"];
                        var col2 = row["InstanceName"];

                        string server = "";
                        if (col1 != null && col1 != DBNull.Value)
                        {
                            server = col1.ToString();
                        }
                        if (col2 != null && col2 != DBNull.Value)
                        {
                            server += @"\" + col2.ToString();
                        }

                        localServers.Add(server);
                    }

                    _localServers = localServers.ToArray();
                }
                catch (Exception ex)
                {
                    LocalServersException = ex;
                }
                finally
                {
                    LocalServersCompleted?.Invoke(this, new EventArgs());
                }
            });
        }

        internal void StartConnectionCheck(Guid newToken,
            string server, bool integratedSecurity, string login, string password, string database)
        {
            _LoadDatabasesToken = newToken;

            try
            {
                var connBuilder = new SqlConnectionStringBuilder()
                {
                    DataSource = server,
                    InitialCatalog = "master",
                    IntegratedSecurity = integratedSecurity
                };
                
                if (!integratedSecurity)
                {
                    connBuilder.IntegratedSecurity = false;
                    connBuilder.UserID = login;
                    connBuilder.Password = password;
                }

                // set connection
                d.SetConnection(connKey => new ConnectionData(connBuilder.ToString(), 0));

                var load = new Action<Guid, SqlConnectionStringBuilder, string>((token, builder, db) =>
                {
                    try
                    {
                        Databases = d
                            .From("sys.sysdatabases")
                            //.Where("HAS_DBACCESS(name) = 1".E())
                            .OrderBy("name")
                            .Select("name")
                            .Go<Row<string>>()
                            .Select(a => a.Column1)
                            .ToList();

                        // check db
                        if (Databases.Count == 0)
                        {
                            throw new System.Exception("There is no database. Provide connection with the access to at least one database.");
                        }

                        // store conn builder
                        if (!string.IsNullOrEmpty(db))
                        {
                            builder.InitialCatalog = db;
                        }
                        SetConnBuilder(builder);

                        CurrentProcessException = null;

                        //Thread.Sleep(2000);

                        ConnectionCheckCompleted?.Invoke(this, new ConnectionCheckEventArgs(db));
                    }
                    catch (Exception ex)
                    {
                        //ConnBuilder = null;
                        if (_LoadDatabasesToken == token)
                        {
                            CurrentProcessException = ex;
                            ConnectionCheckCompleted?.Invoke(this, new ConnectionCheckEventArgs(db, ex));
                        }
                    }
                });

                Task.Factory.StartNew(() =>
                {
                    load(newToken, connBuilder, database);
                });
            }
            catch (Exception ex)
            {
                Common.ShowUnknownException(ex);
            }
        }

        // Designed for the comboDatabase click arrow down event only!
        internal void StartLoadDatabases()
        {
            try
            {
                if (_connBuilder == null)
                {
                    return;
                }

                d.SetConnection(connKey => new ConnectionData(_masterConnBuilder.ToString(), 0));

                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        Databases = d
                            .From("sys.sysdatabases")
                            .OrderBy("name")
                            .Select("name")
                            .Go<Row<string>>()
                            .Select(a => a.Column1)
                            .ToList();

                        LoadDatabasesCompleted?.Invoke(this, new ConnectionCheckEventArgs());
                    }
                    catch (Exception ex)
                    {
                        LoadDatabasesCompleted?.Invoke(this, new ConnectionCheckEventArgs(ex));
                    }
                });

            }
            catch (Exception ex)
            {
                LoadDatabasesCompleted?.Invoke(this, new ConnectionCheckEventArgs(ex));
            }
        }

        internal bool HasQueryTalkBase()
        {
            try
            {
                d.SetConnection(connKey => new ConnectionData(ConnBuilder.ToString(), 0));

                return d
                    .From("sys.sysdatabases")
                    .Where("name", "QueryTalkBase")
                    .Select("name")
                    .Go<Row<string>>()
                    .Any();
            }
            catch
            {
                return false;
            }
        }

        internal void ShowQueryTalkBase()
        {
            try
            {
                ConnBuilder.InitialCatalog = "QueryTalkBase";
                var connString = ConnBuilder.ToString();
                d.SetConnection(connString);
                ConnBuilder.InitialCatalog = "master";   // get back the default value
                d.AsNon("Database Table Loader")
                    .Declare("@ccTable", d.Concatenator)
                    .BeginCursor(d.From("INFORMATION_SCHEMA.TABLES")
                            .OrderBy("TABLE_NAME")
                            .Select((d.Quotename("TABLE_SCHEMA")).Plus(".".L()).Plus(d.Quotename("TABLE_NAME"))).EndView())
                        .IntoVars("@ccTable")
                        .FromSelect("@ccTable")
                        .FetchNext()
                    .EndCursor()
                    .Test();
            }
            catch { }
        }

        private void StartPull()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var clientDownload = new ClientDownload(new Guid().ToString("D"), Program.License.User, Program.License.Password);
                    PullBytes = clientDownload.PullZip();
                }
                catch (Exception ex)
                {
                    PullException = ex;
                }
            });
        }

        internal void StartCreateQueryTalkBase()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var connBuilder = new SqlConnectionStringBuilder(ConnBuilder.ToString());
                    connBuilder.InitialCatalog = "master";
                    CreateQueryTalkBase(connBuilder);

                    // store QueryTalkBase connection string
                    connBuilder.InitialCatalog = "QueryTalkBase";
                    Registry.WriteQueryTalkBaseConnection(connBuilder.ToString());
                }
                catch (Exception ex)
                {
                    if (!HasQueryTalkBase())
                    {
                        CurrentProcessException = ex;
                    }
                    else
                    {
                        CurrentProcessException = null;
                    }
                }
                finally
                {
                    QueryTalkBaseCreationCompleted?.Invoke(this, new EventArgs());
                }
            });
        }

        internal static void CreateQueryTalkBase(SqlConnectionStringBuilder connBuilder)
        { 
            d.SetConnection(connKey => new ConnectionData(connBuilder.ToString(), 0));
            d.Go(Properties.Resources.QueryTalkBaseSQL);
        }

        internal void StartMapping()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Thread.Sleep(500);
                    //throw new Exception("Test mapping exception.");
                }
                catch (Exception ex)
                {
                    CurrentProcessException = ex;
                }
                finally
                {
                    MappingCompleted?.Invoke(this, new EventArgs());
                }
            });
        }

        #endregion

        #region Helper methods

        internal static void EnableButton(Button button, string text = null)
        {
            button.ThreadSafeInvoke(() =>
            {
                button.Enabled = true;

                if (text != null)
                {
                    button.Text = text;
                }
            });
        }

        internal static void DisableButton(Button button, string text = null)
        {
            button.ThreadSafeInvoke(() =>
            {
                button.Enabled = false;

                if (text != null)
                {
                    button.Text = text;
                }
            });
        }

        internal static void EnableLink(LinkLabel link, string text = null)
        {
            link.ThreadSafeInvoke(() =>
            {
                link.Visible = true;

                if (text != null)
                {
                    link.Text = text;
                }
            });
        }

        internal static void DisableLink(LinkLabel link)
        {
            link.ThreadSafeInvoke(() =>
            {
                link.Visible = false;
                //link.LinkColor = Color.LightGray;
            });
        }

        internal static void EnableNextButton(Button button)
        {
            button.ThreadSafeInvoke(() =>
            {
                button.Enabled = true;
                button.BackColor = Color.Green;
                button.Visible = true;
            });
        }

        internal static void DisableNextButton(Button button)
        {
            button.ThreadSafeInvoke(() =>
            {
                button.Enabled = false;
                button.BackColor = Color.DarkGray;
                button.Visible = false;
            });
        }

        #endregion

    }
}
