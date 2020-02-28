using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryTalk.Mapper
{
    // Mapper assembly structure:
    // QueryTalk.Db.<dbname>.Data
    //   ++ data classes
    // QueryTalk.Db.<dbname>.Design
    //   ++ designer classes 
    //   ++ Invokers
    internal partial class Builder
    {
        private MappingHandler _mappingHandler;
        private StringBuilder _csharp = new StringBuilder(3000);
        private string _repositoryPath;

        #region Properties

        // db CLR name
        private string _dbName
        {
            get
            {
                return _mappingHandler.DbName;
            }
        }

        // QueryTalk.Db.<Name>.Data
        private string _dataNamespace;
        private string DataNamespace
        {
            get
            {
                if (_dataNamespace == null)
                {
                    _dataNamespace = String.Format("QueryTalk.Db.{0}.Data", _dbName);
                }
                return _dataNamespace;
            }
        }

        // QueryTalk.Db.<Name>.Design
        private string _designNamespace;
        private string DesignNamespace
        {
            get
            {
                if (_designNamespace == null)
                {
                    _designNamespace = String.Format("QueryTalk.Db.{0}.Design", _dbName);
                }
                return _designNamespace;
            }
        }

        // tables
        private TableOrView[] _tables;
        private TableOrView[] Tables
        {
            get
            {
                if (_tables == null)
                {
                    _tables = _mappingHandler.DbInfo.Table1
                        .Where(a => a.OBJECT_TYPE == (int)ObjectType.Table)
                        .Where(a => a.IsCompliant)   // only CLR compliant objects
                        .OrderBy(a => a.SCHEMA).ThenBy(a => a.OBJECT_NAME)
                        .ToArray();
                }

                return _tables;
            }
        }

        // views
        private TableOrView[] _views;
        private TableOrView[] Views
        {
            get
            {
                if (_views == null)
                {
                    _views = _mappingHandler.DbInfo.Table1
                        .Where(a => a.OBJECT_TYPE == (int)ObjectType.View)
                        .Where(a => a.IsCompliant)   // only CLR compliant objects
                        .OrderBy(a => a.SCHEMA).ThenBy(a => a.OBJECT_NAME)
                        .ToArray();
                }

                return _views;
            }
        }

        // table functions
        private FuncOrProc[] _tableFunctions;
        private FuncOrProc[] TableFunctions
        {
            get
            {
                if (_tableFunctions == null)
                {
                    _tableFunctions = _mappingHandler.DbInfo.Table2
                        .Where(a => a.OBJECT_TYPE == (int)ObjectType.TableFunc)
                        .Where(a => a.IsCompliant)   // only CLR compliant objects
                        .OrderBy(a => a.SCHEMA).ThenBy(a => a.OBJECT_NAME)
                        .ToArray();
                }

                return _tableFunctions;
            }
        }

        // scalar functions
        private FuncOrProc[] _scalarFunctions;
        private FuncOrProc[] ScalarFunctions
        {
            get
            {
                if (_scalarFunctions == null)
                {
                    _scalarFunctions = _mappingHandler.DbInfo.Table2
                        .Where(a => a.OBJECT_TYPE == (int)ObjectType.ScalarFunc)
                        .Where(a => a.IsCompliant)   // only CLR compliant objects
                        .OrderBy(a => a.SCHEMA).ThenBy(a => a.OBJECT_NAME)
                        .ToArray();
                }

                return _scalarFunctions;
            }
        }

        // procedures
        private FuncOrProc[] _procedures;
        private FuncOrProc[] Procedures
        {
            get
            {
                if (_procedures == null)
                {
                    _procedures = _mappingHandler.DbInfo.Table2
                        .Where(a => a.OBJECT_TYPE == (int)ObjectType.Proc)
                        .Where(a => a.IsCompliant)   // only CLR compliant objects
                        .OrderBy(a => a.SCHEMA).ThenBy(a => a.OBJECT_NAME)
                        .ToArray();
                }

                return _procedures;
            }
        }

        // table objects
        private Dictionary<int, TableOrView> TableObjects
        {
            get
            {
                return _mappingHandler.TableObjects;
            }
        }

        #endregion

        // ctor
        internal Builder(MappingHandler mappingHandler, string repositoryPath)
        {
            _mappingHandler = mappingHandler;
            _repositoryPath = repositoryPath;

            // create _csharp code
            AppendAssemblyInfo();
            BeginDatabaseClass();
            AppendDataClasses();
            AppendDesignerClasses();
            EndDatabaseClass();
        }

        private void BeginDatabaseClass()
        {
            // header
            _csharp
                .AppendLine("namespace QueryTalk.Db")
                .AppendLine("{")
                .AppendFormatLine("public static class {0} ", _mappingHandler.DbName)
                .AppendLine("{")
                .AppendLine("public static readonly QueryTalk.Wall.DatabaseMap Map;");

            // static node properties: TABLES
            foreach (var table in Tables)
            {
                _csharp.AppendFormatLine(
                    "public static {2}.{0}<{1}.{0}> {0} {{ get {{ return new {2}.{0}<{1}.{0}>(); }}}}", ((IName)table).NodeName, DataNamespace, DesignNamespace);
            }

            // static node properties: VIEWS
            foreach (var view in Views)
            {
                _csharp.AppendFormatLine(
                    "public static {2}.{0}<{1}.{0}> {0} {{ get {{ return new {2}.{0}<{1}.{0}>(); }}}}", ((IName)view).NodeName, DataNamespace, DesignNamespace);
            }

            // static node properties: TABLE FUNCTIONS
            foreach (var func in TableFunctions)
            {
                _csharp.AppendFormatLine(
                    "public static {2}.{0}<{1}.{0}> {0} {{ get {{ return new {2}.{0}<{1}.{0}>(); }}}}", ((IName)func).NodeName, DataNamespace, DesignNamespace);
            }

            // static node properties: SCALAR FUNCTIONS
            foreach (var func in ScalarFunctions)
            {
                _csharp.AppendFormatLine(
                    "public static {1}.{0} {0} {{ get {{ return new {1}.{0}(); }}}}", ((IName)func).NodeName, DesignNamespace);
            }

            // static node properties: PROCEDURES
            foreach (var proc in Procedures)
            {
                _csharp.AppendFormatLine(
                    "public static {1}.{0} {0} {{ get {{ return new {1}.{0}(); }}}}", ((IName)proc).NodeName, DesignNamespace);
            }

            // ctor#0 (static)
            _csharp
                .AppendFormatLine("static {0}() {{", _mappingHandler.DbName)
                .AppendFormatLine("Map = QueryTalk.Wall.Api.AddDatabase(new QueryTalk.Wall.DatabaseMap(QueryTalk.Designer.Identifier(\"{0}\"), System.DateTime.Now, true));", _mappingHandler.DbName.EscapeDoubleQuote())
                .AppendLine("}");
        }

        private void EndDatabaseClass()
        {
            // append not browsable
            AppendNotBrowsable();

            // footer
            _csharp.AppendLine("}}");

            // show progress
            _mappingHandler.SetProgress(5);
        }

        private void AppendNotBrowsable()
        {
            // Equals
            _csharp
                .AppendLine("[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]")
                .AppendLine("public static new bool Equals(object objA, object objB) { ")
                .AppendLine("return Equals(objA, objB); ")
                .AppendLine("}");

            // ReferenceEquals
            _csharp
                .AppendLine("[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]")
                .AppendLine("public static new bool ReferenceEquals(object objA, object objB) { ")
                .AppendLine("return ReferenceEquals(objA, objB); ")
                .AppendLine("}");
        }

        #region Supporting Methods

        // check if related node in the relation is CLR compliant
        private bool IsRelationCompliant(Relation relation)
        {
            return relation.IsCompliant(TableObjects);
        }

        // concatenate the columns 
        private string _ConcatenateColumns(IEnumerable<Column> columns, string format)
        {
            return String.Join(",", columns.Select(c => String.Format(format, ((IName)c).NodeName)));
        }

        // concatenate the arguments
        private string _ConcatenateArgs(IEnumerable<Column> columns, string format)
        {
            return String.Join(",", columns.Select(c => String.Format(format, c.ArgName)));
        }

        // concatenate the parameters 
        private string _ConcatenateParams(IEnumerable<Parameter> parameters, string format)
        {
            return String.Join(",", parameters.Select(p => String.Format(format, ((IName)p).NodeName)));
        }

        // concatenate the parameters 
        private string _ConcatenateParamsWithAssembly(IEnumerable<Parameter> parameters, string format)
        {
            if (parameters.Count() > 0)
            {
                return "System.Reflection.Assembly.GetCallingAssembly()," + String.Join(",", parameters.Select(p => String.Format(format, ((IName)p).NodeName)));
            }
            else
            {
                return "System.Reflection.Assembly.GetCallingAssembly()";
            }
        }

        #endregion

        #region Linq

        private static string LinqToList(string collection)
        {
            return String.Format("System.Linq.Enumerable.ToList({0})", collection);
        }

        private static string LinqOrderBy(string collection, string keySelector, bool hasByteComparer = false, bool isFirst = true)
        {
            var byteComparer = hasByteComparer ? ", new QueryTalk.Wall.ByteArrayComparer()" : "";

            if (isFirst)
            {
                return String.Format("System.Linq.Enumerable.OrderBy({0}, {1}{2})", collection, keySelector, byteComparer);
            }
            else
            {
                return String.Format("System.Linq.Enumerable.ThenBy({0}, {1}{2})", collection, keySelector, byteComparer);
            }
        }

        #endregion

    }
}
