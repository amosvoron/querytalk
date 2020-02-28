using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Win32;

namespace QueryTalk.Mapper
{
    internal class RegistryHandler
    {
        private const string REGISTRY = @"SOFTWARE\QueryTalk";
        private const string REGISTRY_CONNSTRINGS = @"SOFTWARE\QueryTalk\ConnectionStrings";
        private const string REGISTRY_DotNetDirectory = "DotNetDirectory";
        private const string REGISTRY_Repository = "RepositoryPath";
        private const string REGISTRY_DefaultConnection = "DefaultConnection";
        private const string REGISTRY_QueryTalkBaseConnection = "QueryTalkBaseConnection";
        private const string REGISTRY_GetStartedDone = "GetStartedDone";
        private const int MAX_CONNSTRINGS = 20;

        private List<SqlConnectionStringBuilder> _connBuilders = new List<SqlConnectionStringBuilder>();
        internal List<SqlConnectionStringBuilder> ConnBuilders
        {
            get
            {
                return _connBuilders;
            }
        }

        private string _netPath;
        internal string NetPath
        {
            get
            {
                return String.IsNullOrWhiteSpace(_netPath) ? null : _netPath;
            }
        }

        internal RegistryHandler()
        { }

        // Registry: provide querytalk registry root
        internal RegistryKey ProvideRoot()
        {
            try
            {
                // create root if does not exists
                var key = Registry.CurrentUser.OpenSubKey(REGISTRY, true);
                if (key != null)
                {
                    return key;
                }
                else
                {
                    return Registry.CurrentUser.CreateSubKey(REGISTRY);
                }
            }
            catch 
            {
                return null;
            }
        }

        #region GetStartedDone

        // read GetStartedDone
        internal bool ReadGetStartedDone()
        {
            try
            {
                var root = ProvideRoot();
                if (root == null)
                {
                    return false;   
                }

                return root.GetValue(REGISTRY_GetStartedDone).ToString().ToLowerInvariant() == "true";
            }
            catch
            {
                return false;
            }
        }

        // write GetStartedDone
        internal void WriteGetStartedDone()
        {
            try
            {
                var root = ProvideRoot();
                root.SetValue(REGISTRY_GetStartedDone, "true");
            }
            catch { }
        }

        #endregion

        #region Default Connection

        // read default connection
        internal SqlConnectionStringBuilder GetDefaultConnection()
        {
            try
            {
                var root = ProvideRoot();

                // check 
                if (root == null)
                {
                    return null;   // not likely to happen
                }

                var defaultConnection = (string)root.GetValue(REGISTRY_DefaultConnection);
                var connString = ConnectionEncryption.DecryptAes(defaultConnection);

                if (!string.IsNullOrEmpty(connString))
                {
                    //_defaultConnection = new SqlConnectionStringBuilder(connString);
                    return new SqlConnectionStringBuilder(connString);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        // write default connection
        internal void WriteDefaultConnection(string defaultConnection)
        {
            try
            {
                var root = ProvideRoot();
                root.SetValue(REGISTRY_DefaultConnection, 
                    ConnectionEncryption.EncryptAes(defaultConnection), RegistryValueKind.String);
            }
            catch { }
        }


        #endregion

        #region QueryTalkBase Connection

        // read QueryTalkBase connection
        internal SqlConnectionStringBuilder GetQueryTalkBaseConnection()
        {
            try
            {
                var root = ProvideRoot();

                // check 
                if (root == null)
                {
                    return null;   // not likely to happen
                }

                var queryTalkBaseConnection = (string)root.GetValue(REGISTRY_QueryTalkBaseConnection);
                var connString = ConnectionEncryption.DecryptAes(queryTalkBaseConnection);

                if (!string.IsNullOrEmpty(connString))
                {
                    return new SqlConnectionStringBuilder(connString);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        // write QueryTalkBase connection
        internal void WriteQueryTalkBaseConnection(string queryTalkBaseConnection)
        {
            try
            {
                var root = ProvideRoot();
                root.SetValue(REGISTRY_QueryTalkBaseConnection,
                    ConnectionEncryption.EncryptAes(queryTalkBaseConnection), RegistryValueKind.String);
            }
            catch { }
        }


        #endregion

        #region Connection Strings

        // Registry: read all
        internal void ReadConnections()
        {
            try
            {
                _connBuilders.Clear();

                // open registry subkey
                var key = Registry.CurrentUser.OpenSubKey(REGISTRY_CONNSTRINGS);

                // create Registry subkey
                if (key != null && key.ValueCount > 0)
                {
                    // read registry values
                    foreach (var name in key.GetValueNames())
                    {
                        var value = key.GetValue(name);
                        if (value != null && value is System.String && !String.IsNullOrWhiteSpace((string)value))
                        {
                            try
                            {
                                var connString = ConnectionEncryption.DecryptAes((string)value);
                                _connBuilders.Add(new SqlConnectionStringBuilder(connString));
                            }
                            // catch exception if value is not a conn string
                            catch { }
                        }
                    }
                }
                else
                {
                    Registry.CurrentUser.CreateSubKey(REGISTRY_CONNSTRINGS);
                }
            }
            catch { }
        }

        // Registry: save all
        internal void SyncConnections()
        {
            try
            {
                // delete all registry subkey
                Registry.CurrentUser.DeleteSubKeyTree(REGISTRY_CONNSTRINGS, false);

                // re-create subkey
                var key = Registry.CurrentUser.CreateSubKey(REGISTRY_CONNSTRINGS);

                // add values
                var i = 1;

                foreach (var connBuilder in _connBuilders)
                {
                    key.SetValue(
                        String.Format("Item{0}", i++),
                        ConnectionEncryption.EncryptAes(connBuilder.ToString()),
                        RegistryValueKind.String);
                }
            }
            catch { }
        }

        // add new conn builder only if it is unique
        internal bool TryAddConnBuilder(SqlConnectionStringBuilder connBuilder)
        {
            if (!_connBuilders.Where(b => b.ToString().ToUpperInvariant() == connBuilder.ToString().ToUpperInvariant()).Any())
            {
                // remove the first item if the collection has reached the maximum
                if (_connBuilders.Count >= MAX_CONNSTRINGS)
                {
                    _connBuilders.RemoveAt(0);
                }

                _connBuilders.Add(connBuilder);
                return true;
            }

            return false;
        }

        // remove conn builder 
        internal void RemoveConnBuilder(SqlConnectionStringBuilder connBuilder)
        {
            _connBuilders.Remove(connBuilder);
            SyncConnections();
        }

        #endregion

        #region NET Directory

        // Registry: read NET Directory
        internal void ReadNetDirectory()
        {
            try
            {
                var root = ProvideRoot();
                
                // check 
                if (root == null)
                {
                    return;   // not likely to happen
                }

                _netPath = (string)root.GetValue(REGISTRY_DotNetDirectory);
            }
            catch { }
        }

        // Registry: read NET Directory
        internal void WriteNetDirectory(string path)
        {
            try
            {
                var root = ProvideRoot();
                root.SetValue(REGISTRY_DotNetDirectory, path, RegistryValueKind.String);
            }
            catch { }
        }

        #endregion

        #region Repository

        // check repository 
        //   - should exists in Registry
        //   - should exists in a file system
        // (return true if directory exists and is valid)
        internal bool CheckRepository()
        {
            var repository = GetRepository();

            if (repository == null)
            {
                return false;   // not valid
            }

            if (!Directory.Exists(repository))
            {
                return false;   // not valid
            }

            return true;   // valid
        }

        // Registry: read Repository
        internal string GetRepository()
        {
            try
            {
                var root = ProvideRoot();

                // check 
                if (root == null)
                {
                    return null;   // not likely to happen
                }

                return (string)root.GetValue(REGISTRY_Repository);
                //Program.License.RepositoryPath = _repositoryPath;
                //Program.Start.RepositoryPath = _repositoryPath;
            }
            catch
            {
                return null;
            }
        }

        // Registry: write Repository
        internal void WriteRepository(string path)
        {
            try
            {
                var root = ProvideRoot();
                root.SetValue(REGISTRY_Repository, path, RegistryValueKind.String);
            }
            catch { }
        }

        #endregion

    }
}
