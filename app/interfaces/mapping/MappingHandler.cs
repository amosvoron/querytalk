using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using QueryTalk.Wall;

namespace QueryTalk.Mapper
{
    internal class MappingHandler
    {
        // thread of the mapping process 
        internal Thread Thread { get; set; }

        private string _dbName;
        internal string DbName
        {
            get
            {
                return _dbName;
            }
        }

        private MappingUI _mappingUI;
        internal MappingUI MappingUI
        {
            get
            {
                return _mappingUI;
            }
        }

        private Guid _guid;             // Guid of this thread
        private SqlConnectionStringBuilder _connBuilder;

        // .NET path
        internal string NET_PATH
        {
            get
            {
                return _mappingUI.NET_PATH;
            }
        }

        // assembly version
        internal Tuple<int, int, int, int> AssemblyVersion
        {
            get
            {
                return _mappingUI.AssemblyVersion ?? Tuple.Create(0, 0, 0, 0);
            }
        }

        // file version
        internal Tuple<int, int, int, int> FileVersion
        {
            get
            {
                return _mappingUI.FileVersion ?? Tuple.Create(0, 0, 0, 0);
            }
        }

        // key file (assembly sigining)
        internal KeyFile KeyFile
        {
            get
            {
                return _mappingUI.KeyFile;
            }
        }

        // db info
        internal Result<TableOrView, FuncOrProc, Column, Parameter, Relation, RelationColumn> DbInfo { get; set; }

        // dictionary of table objects
        internal Dictionary<int, TableOrView> TableObjects { get; set; }

        // all SQL objects (initialized in ProcessNodes method)
        private List<IName> _nodes;

        // returns true if this thread is active
        // it is active if the parent holds the same thread Guid
        // Note: it is not likely that this flag returns false value,
        // because the Abort normally stops the thread immediatelly. 
        private bool _isActive
        {
            get
            {
                if (_mappingUI == null)
                {
                    return false;
                }

                return _mappingUI.CurrentGuid == _guid;
            }
        }

        // set progress method
        internal void SetProgress(int increment)
        {
            _mappingUI.SetProgress(_guid, increment);
        }

        // ctor
        internal MappingHandler(MappingUI mappingUI, Guid guid, SqlConnectionStringBuilder connBuilder, string dbName)
        {
            TableObjects = new Dictionary<int, TableOrView>();
            _mappingUI = mappingUI;
            _guid = guid;
            _connBuilder = connBuilder;
            _dbName = dbName;

            // show starting progress
            SetProgress(5);

            // create thread M
            Thread = new Thread(new ThreadStart(Process));
            Thread.IsBackground = true;
            Thread.Start();
        }

        // try connection (try-catch)
        private bool TryConnection(string connString, out SqlException exception)
        {
            exception = null;
            using (var conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (SqlException ex)
                {
                    exception = ex;
                    return false;
                }
            }
        }

        // fetch mapping data
        private Result<TableOrView, FuncOrProc, Column, Parameter, Relation, RelationColumn> FetchDbInfo(string connString)
        {
            d.SetConnection(connKey => new ConnectionData(connString, 0));

            // LIMITED VERSION:
            if (Program.License.IsLimited)
            {
                return SQL.ProcLimited
                    .Go<TableOrView, FuncOrProc, Column, Parameter, Relation, RelationColumn>();
            }
            // FULL VERSION:
            else
            {
                return SQL.Proc
                    .Go<TableOrView, FuncOrProc, Column, Parameter, Relation, RelationColumn>();
            }
        }

        // try add connection data to the Registry
        private void TryAddToRegistry()
        {
            // try add connection builder
            if (_mappingUI.Registry.TryAddConnBuilder(_connBuilder))
            {
                _mappingUI.Registry.SyncConnections();

                // refresh connections
                _mappingUI.RefreshConnections(_connBuilder);
            }
        }

        #region Process (on other thread)

        // process on other thread (try-catch)
        internal void Process()
        {
            try
            {
                // reset the connection flag
                _mappingUI.HasConnectionPassed = false;

                // fetch mapping data
                var connString = _connBuilder.ToString();

                _mappingUI.ShowUnderGo(true, "Connecting...");
                SqlException ex;
                if (!TryConnection(connString, out ex))
                {
                    _mappingUI.SetNoConnection(_guid);
                    _mappingUI.Exception = ex;
                    return;
                }

                // here connection has been tested as successful
                // and the processing has been started
                _mappingUI.SetProcessing();

                _mappingUI.ShowUnderGo(true, "Loading...");

                DbInfo = FetchDbInfo(connString);

                SetProgress(30);

                // try add connection builder
                TryAddToRegistry();

                // check
                if (!CheckIfAnyData())
                {
                    _mappingUI.SetFinishedWithNoData(_guid);
                    return;
                }

                // prepare data
                _mappingUI.ShowUnderGo(true, "Processing...");
                ProcessRelationMirroring();     // mirror relation data
                ProcessNodes();                 // provide object names
                ProcessDataTypes();             // provide data types for columns and params (and set IsCompliant flag for columns/params)
                ProcessColumnNames();           // provide column names, add columns to objects 
                ProcessParamNames();            // provide param names, add params to objects 
                ProcessRelations();             // add columns to relations and relations to tables
                Finalizer();                    // object finalizer: set IsCompliant flag of objects (and theirs relations too)
                SetProgress(5);

                // build & compile C# code
                Builder builder = new Builder(this, Program.Start.RepositoryPath);
                _mappingUI.ShowUnderGo(true, "Compiling...");
                _mappingUI.SetProcessState(ProcessState.Compiling);
                builder.Compile();

                SetProgress(25);

                // Finished (should be the last method in the thread)
                var elapsed = _mappingUI.SetFinished(_guid);

                // Log.LogUse(28, string.Format("{0} ({1})", _connBuilder.InitialCatalog, elapsed));
            }
            // Abort: do nothing
            catch (System.Threading.ThreadAbortException)
            { }
            // Other exception: show error only if it affects to the process 
            catch (Exception ex)
            {
                if (_isActive)
                {
                    _mappingUI.SetException(ex);
                }
            }
        }

        #endregion

        // check if any data
        private bool CheckIfAnyData()
        {
            if (DbInfo.Table1.RowCount == 0 && DbInfo.Table2.RowCount == 0)
            {
                return false;
            }

            return true;
        }

        // relation mirroring
        private void ProcessRelationMirroring()
        {
            // relations
            foreach (var relation in DbInfo.Table5
                .Where(a => (RelationType)a.RELATION_TYPE != RelationType.Self)
                .ToList())
            {
                var mirroredRelation = new Relation()
                    {
                        // switch the nodes
                        NODE_ID = relation.RELATED_ID,
                        RELATED_ID = relation.NODE_ID,

                        // change FK->RK -> RK->FK
                        RELATION_TYPE = relation.RELATION_TYPE + 1,

                        // keep the relationID and other flags
                        RELATION_ID = relation.RELATION_ID,
                        //RELATION_IX = relation.RELATION_IX,
                        HAS_MANY = relation.HAS_MANY,

                        // set IsMirrored flag
                        MirroringIndex = 1,

                        // stored mirrored relation
                        MirroredRelation = relation             // RK side
                    };
                relation.MirroredRelation = mirroredRelation;   // store mirrored relation (FK side)
                DbInfo.Table5.Add(mirroredRelation);
            }
            
            // relation columns
            foreach (var column in DbInfo.Table6
                .Where(a => (RelationType)a.RELATION_TYPE != RelationType.Self)
                .ToList())
            {
                DbInfo.Table6.Add(new RelationColumn()
                {
                    // switch the nodes
                    NODE_ID = column.RELATED_ID,
                    RELATED_ID = column.NODE_ID,
                    RELATED_COLUMN_NAME = column.COLUMN_NAME,
                    RELATED_COLUMN_ORDINAL = column.COLUMN_ORDINAL,             
                    COLUMN_NAME = column.RELATED_COLUMN_NAME,
                    COLUMN_ORDINAL = column.RELATED_COLUMN_ORDINAL,                   

                    // change FK->RK -> RK->FK
                    RELATION_TYPE = column.RELATION_TYPE + 1,

                    // keep the relation ID
                    RELATION_ID = column.RELATION_ID,

                    // set IsMirrored flag
                    IsMirrored = 1
                });
            }
        }

        // process nodes 
        private void ProcessNodes()
        {
            _nodes = new List<IName>();
            _nodes.AddRange(DbInfo.Table1);     // tables, views
            _nodes.AddRange(DbInfo.Table2);     // functions, procedures

            // object names (FAST)
            ClrName.ProcessObjectNames(_nodes, DbName);

            // add ALL tables into TableObjects collection (DONE IN ProcessRelations method)
            bool isSampleTablePicked = false;
            foreach (var table in DbInfo.Table1.Where(t => (ObjectType)t.OBJECT_TYPE == ObjectType.Table))
            {
                TableObjects[table.OBJECT_ID] = table; // add table to table dictionary (by OBJECT_ID)

                // store sample table CLR name
                if (!isSampleTablePicked)
                {
                    Program.Start.SampleTable = ((IName)table).NodeName;
                    isSampleTablePicked = true;
                }
            }
        }

        // process all columns and parameters type
        private void ProcessDataTypes()
        {
            // columns
            foreach (var column in DbInfo.Table3)
            {
                column.SetTypeInfo();
            }

            // params
            foreach (var param in DbInfo.Table4.Where(p => (ParameterMode)p.PARAMETER_MODE != ParameterMode.None))
            {
                param.SetTypeInfo();
            }
        }

        // process column names (OPTIMIZED)
        private void ProcessColumnNames()
        {
            // order nodes by name
            var orderedNodes = _nodes
                .Where(a => ((IColumn)a).OBJECT_TYPE <= 3)
                .OrderBy(a => ((IColumn)a).SCHEMA)
                .ThenBy(a => ((IColumn)a).OBJECT_NAME)
                .ToList();

            // node count (for checking)
            //var nodeCount = orderedNodes.Count;

            // we use this flag to skip ALL columns that do not have a corresponding node 
            //bool skip = false;  

            // declarations 
            var nodeix = -1;                        // current node index 
            var columnSet = new HashSet<string>();  // columns hash set (to check duplicates)
            IColumn node = null;                    // current node

            // loop through ordered columns (the same order as of the nodes)
            // WE ASSUME THAT THE COLUMN NODE ORDER IS THE SAME AS THE NODE ORDER ITSELF
            // Important! Inside the node the columns are ordered by ORDINAL_POSITION
            foreach (var column in DbInfo.Table3
                .OrderBy(a => a.SCHEMA)
                .ThenBy(a => a.OBJECT_NAME)
                .ThenBy(c => c.ORDINAL_POSITION))
            {
                // is new table?
                if (column.ORDINAL_POSITION == 1)
                {
                    ++nodeix;                               // move to the next table
                    node = (IColumn)orderedNodes[nodeix];   // with no check (should exists) !!
                    node.Columns = new List<Column>();
                    columnSet.Clear();                      // prepare empty column set
                }

                // ----------------------------------------------------------------------------------
                // IMPORTANT VALIDITY CHECK
                // Exclude objects that does not fulfill the following rules:
                //   - First column should have the ordinal position 0.
                // Reason why this can happen is that either the SQL Server does not return 
                // correct data (not likely) or the user permissions causes that certain columns 
                // are excluded because the user does not have the access permission to certain 
                // underlying user defined types (likely to happen).
                // ----------------------------------------------------------------------------------
                if (nodeix >= 0 &&
                    !(column.SCHEMA == node.SCHEMA && column.OBJECT_NAME == node.OBJECT_NAME))
                {
                    node.SetNotCompliant();
                    ++nodeix;   // if node && param's node do not match => move to next node
                    continue;
                }

                // append columns to node
                ((IName)column).Name = Api.GetClrColumnName(column.COLUMN_NAME, columnSet, ((IName)node).NodeName, _dbName);
                node.Columns.Add(column);
            }
        }

        // process parameter names (OPTIMIZED)
        private void ProcessParamNames()
        {
            // order nodes by name
            var orderedNodes = _nodes
                .Where(a => ((IColumn)a).OBJECT_TYPE >= 3)
                .OrderBy(a => ((IColumn)a).SCHEMA)
                .ThenBy(a => ((IColumn)a).OBJECT_NAME)
                .ToList();

            // declarations 
            var nodeix = -1;                        // current node index
            var paramSet = new HashSet<string>();   // columns hash set (to check duplicates)
            FuncOrProc node = null;                 // current node
            int renameIndex = 1;                    // rename index (inside the node)
            string clrName = null;                  // param CLR name

            // loop through ordered columns (the same order as of the nodes)
            // WE ASSUME THAT THE COLUMN NODE ORDER IS THE SAME AS THE NODE ORDER ITSELF
            // Important! Inside the node the columns are ordered by ORDINAL_POSITION
            foreach (var param in DbInfo.Table4
                .OrderBy(a => a.SCHEMA)
                .ThenBy(a => a.OBJECT_NAME)
                .ThenBy(c => c.ORDINAL_POSITION))
            {
                // ordinal position = 0 => always move to the next node
                if (param.ORDINAL_POSITION == 0)
                {
                    ++nodeix;                                       // mode to the next table
                    node = (FuncOrProc)orderedNodes[nodeix];        // with no check (should exists) !!
                    paramSet.Clear();                               // prepare empty param set
                }
                // ordinal position = 0 => move to the next node if the current node does not have a ZERO param 
                else if (param.ORDINAL_POSITION == 1)
                {
                    //if (node == null || !(node.SCHEMA == param.SCHEMA && node.OBJECT_NAME == param.OBJECT_NAME))
                    if (node == null || !param.BelongsTo(node))
                    {
                        // clear param set if this is the first param
                        if (!param.BelongsTo(node))
                        {
                            paramSet.Clear();
                        }

                        ++nodeix;                                   // mode to the next table
                        node = (FuncOrProc)orderedNodes[nodeix];    // with no check (should exists) !!
                    }
                }

                // ----------------------------------------------------------------------------------
                // IMPORTANT VALIDITY CHECK
                // Exclude objects that does not fulfill the following rules:
                //   - First parameter should have the ordinal position 0 or 1.
                // Reason why this can happen is that either the SQL Server does not return 
                // correct data (not likely) or the user permissions causes that certain parameters 
                // are excluded because the user does not have the access permission to certain 
                // underlying user defined types (likely to happen).
                // ----------------------------------------------------------------------------------
                if (nodeix >= 0 && !param.BelongsTo(node))
                    //!(param.SCHEMA == node.SCHEMA && param.OBJECT_NAME == node.OBJECT_NAME))
                {
                    ((IColumn)node).SetNotCompliant();
                    ++nodeix;   // if node && param's node do not match => move to next node
                    continue;
                }

                // skip NONE params
                if ((ParameterMode)param.PARAMETER_MODE == ParameterMode.None)
                {
                    continue;
                }

                // get param CLR name (for non-OUT params)
                if ((ParameterMode)param.PARAMETER_MODE != ParameterMode.Out)
                {
                    // remove @
                    clrName = Regex.Replace(param.PARAMETER_NAME, "^@", "");
                    clrName = Api.GetClrName(clrName);
                    clrName = ClrName.GetArgName(clrName);

                    if (!ClrName.CheckParamName(clrName, ((IName)node).Name))
                    {
                        clrName = ClrName.Rename(clrName, 1); 
                    }

                    // prepare schema_node name
                    //var schema_node = param.SCHEMA + "_" + param.OBJECT_NAME;

                    // ensure that the param name is unique (inside the node)
                    renameIndex = 1; // reset rename index
                    //while (!paramSet.Add(schema_node + "_" + clrName))
                    while (!paramSet.Add(clrName))
                    {
                        clrName = ClrName.Rename(clrName, renameIndex++);
                    }

                    // append columns to table
                    ((IName)param).Name = clrName;
                }

                // add param
                node.Parameters.Add(param);
            }
        }

        // process relations
        private void ProcessRelations()
        {
            // prepare relations (append columns)
            int t = 0;    // index of tables
            int tp = -1;  // index of previous table
            var tables = DbInfo.Table1
                .Where(a => (ObjectType)a.OBJECT_TYPE == ObjectType.Table)
                .OrderBy(a => a.OBJECT_ID)
                .ToList();

            // columns
            int c = 0; // index of relation columns
            var columns = DbInfo.Table6
                .OrderBy(a => a.NODE_ID)
                .ThenBy(a => a.RELATED_ID)
                .ThenBy(a => a.RELATION_ID)
                .ThenBy(a => a.IsMirrored)
                .ToList();
            var cc = columns.Count - 1; // columns count

            // loop through RELATIONS
            TableOrView table = null;
            int rid = -1;           // previous related node
            Relation prev = null;   // previous relation (of the same link)
            foreach (var relation in DbInfo.Table5
                .OrderBy(a => a.NODE_ID)
                .ThenBy(a => a.RELATED_ID)
                .ThenBy(a => a.RELATION_ID)
                .ThenBy(a => a.MirroringIndex))
            {
                // find table of relation
                while (tables[t].OBJECT_ID < relation.NODE_ID) { ++t; }

                // ATTENTION! We anticipate that we must have found the table (!) - no failure here.
                // (Since ALL the tables are included in the 'tables' collection.)

                // now find the relation column
                while (!(
                    columns[c].NODE_ID == relation.NODE_ID && 
                    columns[c].RELATED_ID == relation.RELATED_ID &&
                    columns[c].RELATION_ID == relation.RELATION_ID &&
                    columns[c].IsMirrored == relation.MirroringIndex)) 
                { ++c; }

                // ATTENTION! We anticipate that we must have found the first matching column (!) - no failure here

                // loop through the columns and include them in the relation object
                relation.Columns = new List<RelationColumn>();
                while (
                    columns[c].NODE_ID == relation.NODE_ID &&
                    columns[c].RELATED_ID == relation.RELATED_ID &&
                    columns[c].RELATION_ID == relation.RELATION_ID &&
                    columns[c].IsMirrored == relation.MirroringIndex)
                {
                    relation.Columns.Add(columns[c]);
                    ++c;

                    // check
                    if (c > cc)
                    {
                        break;
                    }
                }
                
                // we assume that this is THE FIRST RELATION of the link
                relation.IsLink = true;

                // is new node
                if (t != tp)
                {
                    table = tables[t];
                    table.Relations = new List<Relation>();
                    tp = t;
                }
                // establish a connecton between the current relation and the next relation of the same link (multiple relations)
                else if (rid == relation.RELATED_ID
                    && prev.RELATION_TYPE == relation.RELATION_TYPE)       // Only the familiar relation type (to support cross relations)
                {
                    prev.Next = relation;

                    // if a relation has a predecessor => it cannot be the first relation
                    relation.IsLink = false;
                }

                // add relation to table
                table.Relations.Add(relation);

                // set next relation handler's variables
                rid = relation.RELATED_ID;
                prev = relation;
            }
        }

        // finalizer
        private void Finalizer()
        {
            foreach (var node in _nodes)
            {
                ((IFinalizer)node).Finalizer();
            }
        }

        // get a list of non-compliants objects
        internal List<string> GetNonCompliantObjects()
        {
            List<string> nonCompliants = new List<string>();
            nonCompliants.AddRange(DbInfo.Table1.Where(a => !a.IsCompliant).Select(a => String.Format("{0}.{1}", a.SCHEMA, a.OBJECT_NAME)));
            nonCompliants.AddRange(DbInfo.Table2.Where(a => !a.IsCompliant).Select(a => String.Format("{0}.{1}", a.SCHEMA, a.OBJECT_NAME)));
            return nonCompliants;
        }

    }
}
