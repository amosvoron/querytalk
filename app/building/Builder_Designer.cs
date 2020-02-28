using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using QueryTalk.Wall;

namespace QueryTalk.Mapper
{
    internal partial class Builder
    {

        private void AppendDesignerClasses()
        {
            _csharp.AppendLine("public static class Design {");

            AppendDesignerTables();
            AppendDesignerViews();
            AppendDesignerTableFunctions();
            AppendDesignerScalarFunctions();
            AppendDesignerProcedures();
            AppendInvokers();
            AppendNotBrowsable();
            _csharp.AppendLine("}");  
        }

        private void AppendDesignerTables()
        {
            // loop through tables
            foreach (var table in Tables)
            {
                var tableName = ((IName)table).NodeName;
                var columnSet = new HashSet<string>();  // column uniqueness (with relations) checker

                _csharp
                    .AppendFormatLine("public sealed class {0}<TRoot> : QueryTalk.Wall.DbTable<TRoot>, QueryTalk.Wall.ITable where TRoot : QueryTalk.Wall.DbRow", tableName)
                    .AppendLine("{")
                    .AppendLine("[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]")
                    .AppendLine("internal static QueryTalk.Wall.DB3 _NodeID;");

                // columns
                foreach (var column in table.OrderedColumns)
                {
                    var columnName = ((IName)column).NodeName;
                    //columnSet.Add(propertyName.ToUpperInvariant());  // no need to check if added because that was already done by .ProcessColumnNames method
                    columnSet.Add(columnName);  // no need to check if added because that was already done by .ProcessColumnNames method
                    _csharp.AppendFormatLine("public readonly QueryTalk.Wall.DbColumn {0};", columnName);
                }

                _AppendDesignerRelations(table, columnSet);

                // Not
                _csharp
                    .AppendFormatLine("[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]")
                    .AppendFormatLine("public new {0}<TRoot> Not {{ get {{ return ({0}<TRoot>)base.Not; }}}}", tableName);

                // Or
                _csharp
                    .AppendFormatLine("[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]")
                    .AppendFormatLine("public new {0}<TRoot> Or {{ get {{ return ({0}<TRoot>)base.Or; }}}}", tableName);

                // ctor#0 (static)
                _csharp
                    .AppendFormatLine("static {0}() {{ QueryTalk.Wall.Api.Call(() => {{", tableName)
                    .AppendFormatLine("_NodeID = QueryTalk.Wall.DB3.Get({0}.Map.ID.DbX, {1});", _dbName, ((INode)table).NodeID)
                    .AppendFormatLine("var nodeMap = new QueryTalk.Wall.NodeMap(_NodeID, QueryTalk.Designer.Identifier(\"{0}\", \"{1}\"), {2});",
                        table.SCHEMA.EscapeDoubleQuote(), table.OBJECT_NAME.EscapeDoubleQuote(), table.HasGenuineRK.ToClrString())
                    .AppendLine("QueryTalk.Wall.Api.AddNode(nodeMap);");

                // column definitions
                _AppendDesignerColumns(table);

                // add relations
                _AppendDesignerLinks(table);

                // end static ctor
                _csharp.AppendLine("});}");

                // ctor#1
                _csharp
                    .AppendFormatLine("internal {0}(QueryTalk.Wall.DbNode prev) : base(prev)", tableName)
                    .AppendLine("{")
                    .AppendLine("NodeID = _NodeID;");
                foreach (var column in table.OrderedColumns)   // ordered
                {
                    _csharp.AppendFormatLine("{0} = new QueryTalk.Wall.DbColumn(this, {1}<TRoot>._NodeID.GetColumnID({2}));",
                        ((IName)column).NodeName, tableName, ((INode)column).NodeID);
                }
                _csharp.AppendLine("}");

                // ctor#2
                _csharp.AppendFormatLine("internal {0}() : this((QueryTalk.Wall.DbNode)null) {{ }}", tableName);

                // method: At (Note: .EqualTo method do not accept System.Object type => is gets wrapped by Value object)
                var atArgs = String.Join(",",
                    table.RKColumns
                        .Select(c => String.Format("{0}{1} {2}", c.TypeInfo.Name, c.NullableSign, c.ArgName))
                            .ToArray());

                _csharp
                    .AppendFormatLine("public {0}<TRoot> At({1}) {{", tableName, atArgs)
                    .AppendFormatLine("return QueryTalk.Extensions.Whose(CanBeReused<{0}<TRoot>>(), null, {1});",
                        tableName,
                        String.Join(".And", table.RKColumns.Select(c => String.Format("(this.{0}.EqualTo({1}))",
                            ((IName)c).NodeName, c.TypeInfo.IsSqlVariant ? String.Format("QueryTalk.Designer.Value({0})", c.ArgName) : c.ArgName)).ToArray()))
                    .AppendLine("}");

                // method: GoAt
                _csharp
                    .AppendFormatLine("public TRoot GoAt({0}) {{", atArgs)
                    .AppendFormatLine("QueryTalk.Wall.Api.CheckGoAt<TRoot, {1}.{0}>();", ((IName)table).NodeName, DataNamespace)
                    .AppendFormatLine("return System.Linq.Enumerable.FirstOrDefault(At({0}).Go(System.Reflection.Assembly.GetCallingAssembly()));", _ConcatenateArgs(table.RKColumns, "{0}"))
                    .AppendLine("}");

                // mapped: LoadChildren
                _csharp.AppendFormatLine("void QueryTalk.Wall.ITable.LoadChildren(ref System.Collections.Generic.List<QueryTalk.Wall.NodeTree> children, int level, int maxLevels) {{");

                // MANY PROPERTY
                if (table.Relations != null)
                {
                    //foreach (var relation in table.Relations.Where(r => r.IsLink && r.IsOneToMany()))
                    foreach (var relation in table.Relations.Where(r => r.IsLink && r.IsFromRKToFK()))
                    {
                        _csharp.AppendFormatLine("children.Add(new QueryTalk.Wall.NodeTree(_NodeID, QueryTalk.Wall.DB3.Get({0}.Map.ID.DbX, {1}, 0), level, maxLevels));",
                            _dbName, ((INode)TableObjects[relation.RELATED_ID]).NodeID);
                        _csharp.AppendFormatLine("((QueryTalk.Wall.ITable){0}.{1}).LoadChildren(ref children, level + 1, maxLevels);",
                            _dbName, relation.RelatedName);
                    }
                }
                _csharp.AppendLine("}");

                // end table class
                _csharp.AppendLine("}");
            }

            // progress
            _mappingHandler.SetProgress(5);
        }

        private void _AppendDesignerColumns(TableOrView table)
        {
            foreach (var column in table.OrderedColumns)   // ordered by name
            {
                _csharp.AppendFormatLine("nodeMap.Columns.Add(new QueryTalk.Wall.ColumnMap(_NodeID.GetColumnID(" +
                        "{0}), {8}, \"{1}\".I(false), {2}, {3}, {4}, {5}, {6}, QueryTalk.Wall.ColumnType.{7}, {9}));",
                    ((INode)column).NodeID,
                    column.COLUMN_NAME.EscapeDoubleQuote(),
                    column.TypeInfo.BuildDbt(column.LENGTH, column.PRECISION, column.SCALE),
                    column.IS_NULLABLE.ToClrString(),
                    column.IS_RK.ToClrString(),
                    column.IS_UK.ToClrString(),
                    column.IS_FK.ToClrString(),
                    column.ColumnType,
                    column.ORDINAL_POSITION,
                    column.HAS_DEFAULT.ToClrString());
            }
        }

        private void _AppendDesignerRelations(TableOrView table, HashSet<string> columnSet)
        {
            // relations 
            if (table.Relations != null)
            {
                // loop thru relations
                foreach (var relation in table.Relations) // .Where(r => r.IsLink)) DEPRECATED: Data & Design classes have the same structure
                {
                    // check if relation is compliant
                    if (!IsRelationCompliant(relation))
                    {
                        continue;
                    }

                    // related node
                    var related = TableObjects[relation.RELATED_ID];

                    // set related node name (as unique)
                    var foreignKey = ClrName.ProvideUniqueNameForDesigner(relation, table, related, columnSet);

                    if (foreignKey != null)
                    {
                        _csharp.AppendFormatLine("public {0}<TRoot> {1} {{ get {{", // return new {0}<TRoot>(this, {2}); }} }}",                    
                            ((IName)related).NodeName, relation.RelatedNameForDesigner);
                        _csharp.AppendFormatLine("var r = new {0}<TRoot>(this);", ((IName)related).NodeName);

                        // get fk node
                        TableOrView fknode = !relation.IsMirrored ? table : related;

                        _csharp.AppendFormatLine("((QueryTalk.Wall.IRelation)r).FK = {0}<TRoot>._NodeID.GetColumnID({1});",
                            ((IName)fknode).NodeName, ((INode)relation.ExposedForeignKeyColumn).NodeID);
                        _csharp.AppendLine("return r;");
                        _csharp.AppendLine("}}");
                    }
                    else
                    {
                        _csharp.AppendFormatLine("public {0}<TRoot> {1} {{ get {{ return new {0}<TRoot>(this); }} }}",
                            ((IName)related).NodeName, relation.RelatedNameForDesigner);
                    }
                }
            }
        }

        private void _AppendDesignerLinks(TableOrView table)
        {
            // check if any relations
            if (table.Relations == null)
            {
                return;
            }

            int i = 0;
            int related_id = -1;
            foreach (var relation in table.Relations.OrderBy(r => r.RELATED_ID))
            {
                // check if relation is compliant
                if (!IsRelationCompliant(relation))
                {
                    continue;
                }

                var related = TableObjects[relation.RELATED_ID];

                // is new link
                if (related_id != relation.RELATED_ID)
                {
                    ++i;
                    _csharp.AppendFormatLine("var id{0} = QueryTalk.Wall.DB3.Get({1}.Map.ID.DbX, {2}, 0);", i, _dbName, ((INode)related).NodeID);
                    _csharp.AppendFormatLine("var link{0} = QueryTalk.Wall.Api.AddLink(new QueryTalk.Wall.Link(_NodeID, id{0}));", i);
                    related_id = relation.RELATED_ID;

                    // append AddNodeInvoker (if not self relation)
                    if (!relation.IsSelf)
                    {
                        _csharp.AppendFormatLine("QueryTalk.Wall.Api.AddNodeInvoker(new QueryTalk.Wall.NodeInvoker(id{0}, () => {{ var x = {1}<TRoot>._NodeID; }}));",
                            i, ((IName)related).NodeName);
                    }
                }

                // build relations
                var fkColumnZ = relation.GetForeignKeyColumnsZ(TableObjects);
                string fkBuild = null, fkColumnsBuild = null, rkColumnsBuild = null;

                fkColumnsBuild = String.Format("new[] {{{0}}}",
                    String.Join(",", relation.Columns.Select(c =>
                        String.Format("_NodeID.GetColumnID({0})", ((INode)table.Columns[c.COLUMN_ORDINAL - 1]).NodeID))));
                rkColumnsBuild = String.Format("new[] {{{0}}}",
                    String.Join(",", relation.Columns.Select(c =>
                        String.Format("id{0}.GetColumnID({1})", i, ((INode)related.Columns[c.RELATED_COLUMN_ORDINAL - 1]).NodeID))));

                // FK->RK
                if (relation.IsFromFKToRK())
                {
                    fkBuild = String.Format("_NodeID.GetColumnID({0})", fkColumnZ);
                    _csharp.AppendFormatLine("link{0}.AddRelation(new QueryTalk.Wall.Relation({1}, {2}, {3}));",
                        i, fkBuild, fkColumnsBuild, rkColumnsBuild);
                    _csharp.AppendFormatLine("QueryTalk.Wall.Api.AddGraphInvoker(Invokers.GetGraphInvoker_{0}(_NodeID.GetColumnID({1}), id{2}));",
                        relation.RELATION_ID, fkColumnZ, i);
                }
                // RK->FK => reverse collections 
                else
                {
                    fkBuild = String.Format("id{0}.GetColumnID({1})", i, fkColumnZ);
                    _csharp.AppendFormatLine("link{0}.AddRelation(new QueryTalk.Wall.Relation({1}, {2}, {3}));",
                        i, fkBuild, rkColumnsBuild, fkColumnsBuild);
                    _csharp.AppendFormatLine("QueryTalk.Wall.Api.AddGraphInvoker(Invokers.GetGraphInvoker_{0}(id{1}.GetColumnID({2}), _NodeID));",
                        relation.RELATION_ID, i, fkColumnZ);
                }
            }
        }

        private void AppendDesignerViews()
        {
            // loop through views
            foreach (var view in Views)
            {
                var viewName = ((IName)view).NodeName;

                _csharp
                    .AppendFormatLine("public sealed class {0}<TRoot> : QueryTalk.Wall.DbTable<TRoot>, QueryTalk.Wall.IView where TRoot : QueryTalk.Wall.DbRow", viewName)
                    .AppendLine("{")
                    .AppendLine("[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]")
                    .AppendLine("internal static QueryTalk.Wall.DB3 _NodeID;");

                // columns
                foreach (var column in view.OrderedColumns)
                {
                    _csharp.AppendFormatLine("public readonly QueryTalk.Wall.DbColumn {0};", ((IName)column).NodeName);
                }

                // ctor#0 (static)
                _csharp
                    .AppendFormatLine("static {0}() {{ QueryTalk.Wall.Api.Call(() => {{", viewName)
                    .AppendFormatLine("_NodeID = QueryTalk.Wall.DB3.Get({0}.Map.ID.DbX, {1});", _dbName, ((INode)view).NodeID)
                    .AppendFormatLine("var nodeMap = new QueryTalk.Wall.NodeMap(_NodeID, QueryTalk.Designer.Identifier(\"{0}\", \"{1}\"));",
                        view.SCHEMA.EscapeDoubleQuote(), view.OBJECT_NAME.EscapeDoubleQuote())
                    .AppendLine("QueryTalk.Wall.Api.AddNode(nodeMap);");

                // column definitions
                var ix = 1;
                foreach (var column in view.OrderedColumns)   // ordered
                {
                    _csharp.AppendFormatLine("nodeMap.Columns.Add(new QueryTalk.Wall.ColumnMap(_NodeID.GetColumnID({0}), {4}, \"{1}\".I(false), {2}, {3}));",
                        ix++, 
                        column.COLUMN_NAME.EscapeDoubleQuote(), 
                        column.TypeInfo.BuildDbt(column.LENGTH, column.PRECISION, column.SCALE),
                        column.IS_NULLABLE.ToClrString(),
                        column.ORDINAL_POSITION);
                }

                _csharp.AppendLine("});}");

                // ctor#1
                _csharp
                    .AppendFormatLine("internal {0}(QueryTalk.Wall.DbNode prev) : base(prev)", viewName)
                    .AppendLine("{")
                    .AppendLine("NodeID = _NodeID;");
                ix = 1;
                foreach (var column in view.OrderedColumns)   // ordered
                {
                    _csharp.AppendFormatLine("{0} = new QueryTalk.Wall.DbColumn(this, {1}<TRoot>._NodeID.GetColumnID({2}));",
                        ((IName)column).NodeName, viewName, ix++);
                }
                _csharp.AppendLine("}");

                // ctor#2
                _csharp.AppendFormatLine("internal {0}() : this((QueryTalk.Wall.DbNode)null) {{ }}", viewName);

                // end table class
                _csharp.AppendLine("}");

            }

            // progress
            _mappingHandler.SetProgress(5);
        }

        private void AppendDesignerTableFunctions()
        {
            foreach (var func in TableFunctions)
            {
                var funcName = ((IName)func).NodeName;

                _csharp
                    .AppendFormatLine("public sealed class {0}<TNode> : QueryTalk.Wall.DbTableFunc<TNode> where TNode : QueryTalk.Wall.DbRow", funcName)
                    .AppendLine("{")
                    .AppendLine("[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]")
                    .AppendLine("internal static QueryTalk.Wall.DB3 _NodeID;");

                // columns
                foreach (var column in func.OrderedColumns)
                {
                    _csharp.AppendFormatLine("public readonly QueryTalk.Wall.DbColumn {0};", ((IName)column).NodeName);
                }

                // ctor#0 (static)
                _csharp
                    .AppendFormatLine("static {0}() {{ QueryTalk.Wall.Api.Call(() => {{", funcName)
                    .AppendFormatLine("_NodeID = QueryTalk.Wall.DB3.Get({0}.Map.ID.DbX, {1});", _dbName, ((INode)func).NodeID)
                    .AppendFormatLine("var nodeMap = new QueryTalk.Wall.NodeMap(_NodeID, QueryTalk.Designer.Identifier(\"{0}\", \"{1}\"));",
                        func.SCHEMA.EscapeDoubleQuote(), func.OBJECT_NAME.EscapeDoubleQuote())
                    .AppendLine("QueryTalk.Wall.Api.AddNode(nodeMap);");

                // column definitions
                var ix = 1;
                foreach (var column in func.OrderedColumns)   // ordered
                {
                    _csharp.AppendFormatLine("nodeMap.Columns.Add(new QueryTalk.Wall.ColumnMap(_NodeID.GetColumnID({0}), {4}, \"{1}\".I(false), {2}, {3}));",
                        ix++, 
                        column.COLUMN_NAME.EscapeDoubleQuote(), 
                        column.TypeInfo.BuildDbt(column.LENGTH, column.PRECISION, column.SCALE),
                        column.IS_NULLABLE.ToClrString(),
                        column.ORDINAL_POSITION);
                }

                // params
                ix = 1;
                foreach (var param in func.InParameters)
                {
                    _csharp.AppendFormatLine("nodeMap.Params.Add(new QueryTalk.Wall.ParamMap(_NodeID.GetColumnID({0}), \"{1}\", {2}));",
                        ix++, param.PARAMETER_NAME.EscapeDoubleQuote(), param.TypeInfo.BuildDbt(param.LENGTH, param.PRECISION, param.SCALE));
                }

                _csharp.AppendLine("});}");

                // ctor#1
                _csharp
                    .AppendFormatLine("internal {0}()", funcName)
                    .AppendLine("{")
                    .AppendLine("NodeID = _NodeID;");
                ix = 1;
                foreach (var column in func.OrderedColumns)   // ordered
                {
                    _csharp.AppendFormatLine("{0} = new QueryTalk.Wall.DbColumn(this, {1}<TNode>._NodeID.GetColumnID({2}));",
                        ((IName)column).NodeName, funcName, ix++);
                }
                _csharp.AppendLine("}");

                // .Go
                _csharp
                    .AppendFormatLine("public {0}QueryTalk.Result<TNode> Go({1}) {{",
                        func.InParameters.Count() == 0 ? "override " : "",
                        String.Join(",", func.InParameters.Select(p => String.Format("{0} {1}", p.TypeInfo.ParameterType, ((IName)p).NodeName)).ToArray()))
                    .AppendFormatLine("return base.GoFunc({0});", _ConcatenateParamsWithAssembly(func.InParameters, "{0}"))
                    .AppendLine("}");

                // .Pass
                _csharp
                    .AppendFormatLine("public {0}<TNode> Pass({1}) {{", funcName, _ConcatenateParams(func.InParameters, "QueryTalk.FunctionArgument {0}"))
                    .AppendFormatLine("return ({0}<TNode>)(object)base.Pass({1});", funcName, _ConcatenateParams(func.InParameters, "{0}"))
                    .AppendLine("}");

                // end table class
                _csharp.AppendLine("}");
            }

            // progress
            _mappingHandler.SetProgress(5);
        }

        private void AppendDesignerScalarFunctions()
        {
            foreach (var func in ScalarFunctions)
            {
                var funcName = ((IName)func).NodeName;

                _csharp
                    .AppendFormatLine("public sealed class {0} : QueryTalk.Wall.DbScalarFunc", funcName)
                    .AppendLine("{")
                    .AppendLine("[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]")
                    .AppendLine("internal static QueryTalk.Wall.DB3 _NodeID;");

                // ctor#0 (static)
                _csharp
                    .AppendFormatLine("static {0}() {{ QueryTalk.Wall.Api.Call(() => {{", funcName)
                    .AppendFormatLine("_NodeID = QueryTalk.Wall.DB3.Get({0}.Map.ID.DbX, {1});", _dbName, ((INode)func).NodeID)
                    .AppendFormatLine("var nodeMap = new QueryTalk.Wall.NodeMap(_NodeID, QueryTalk.Designer.Identifier(\"{0}\", \"{1}\"));",
                        func.SCHEMA.EscapeDoubleQuote(), func.OBJECT_NAME.EscapeDoubleQuote())
                    .AppendLine("QueryTalk.Wall.Api.AddNode(nodeMap);");

                // params
                var ix = 1;
                foreach (var param in func.InParameters)
                {
                    _csharp.AppendFormatLine("nodeMap.Params.Add(new QueryTalk.Wall.ParamMap(_NodeID.GetColumnID({0}), \"{1}\", {2}));",
                        ix++, param.PARAMETER_NAME.EscapeDoubleQuote(), param.TypeInfo.BuildDbt(param.LENGTH, param.PRECISION, param.SCALE));
                }

                _csharp.AppendLine("});}");

                // ctor#1
                _csharp
                    .AppendFormatLine("internal {0}()", funcName)
                    .AppendLine("{")
                    .AppendLine("NodeID = _NodeID;")
                    .AppendLine("}");

                // .Go
                var returnClrType = func.ReturnParameter.TypeInfo.ReturnType;
                _csharp
                    .AppendFormatLine("public {0} Go({1}) {{",
                        returnClrType,
                        String.Join(",", func.InParameters.Select(p => String.Format("{0} {1}", p.TypeInfo.ParameterType, ((IName)p).NodeName)).ToArray()))
                    .AppendFormatLine("return base.GoFunc<{0}>({1});", returnClrType, _ConcatenateParamsWithAssembly(func.InParameters, "{0}"))
                    .AppendLine("}");

                // .Pass
                _csharp
                    .AppendFormatLine("public QueryTalk.Wall.Udf Pass({0}) {{", _ConcatenateParams(func.InParameters, "QueryTalk.FunctionArgument {0}"))
                    .AppendFormatLine("return base.Pass({0});", _ConcatenateParams(func.InParameters, "{0}"))
                    .AppendLine("}");

                // end table class
                _csharp.AppendLine("}");
            }

            // progress
            _mappingHandler.SetProgress(5);
        }

        private void AppendDesignerProcedures()
        {
            foreach (var proc in Procedures)
            {
                var procName = ((IName)proc).NodeName;

                _csharp
                    .AppendFormatLine("public sealed class {0} : QueryTalk.Wall.DbProcedure", procName)
                    .AppendLine("{")
                    .AppendLine("[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]")
                    .AppendLine("internal static QueryTalk.Wall.DB3 _NodeID;");

                // ctor#0 (static)
                _csharp
                    .AppendFormatLine("static {0}() {{ QueryTalk.Wall.Api.Call(() => {{", procName)
                    .AppendFormatLine("_NodeID = QueryTalk.Wall.DB3.Get({0}.Map.ID.DbX, {1});", _dbName, ((INode)proc).NodeID)
                    .AppendFormatLine("var nodeMap = new QueryTalk.Wall.NodeMap(_NodeID, QueryTalk.Designer.Identifier(\"{0}\", \"{1}\"));",
                        proc.SCHEMA.EscapeDoubleQuote(), proc.OBJECT_NAME.EscapeDoubleQuote())
                    .AppendLine("QueryTalk.Wall.Api.AddNode(nodeMap);");

                // params
                var ix = 1;
                foreach (var param in proc.InParameters)
                {
                    _csharp.AppendFormatLine("nodeMap.Params.Add(new QueryTalk.Wall.ParamMap(_NodeID.GetColumnID({0}), \"{1}\", {2}, {3}));",
                        ix++, param.PARAMETER_NAME.EscapeDoubleQuote(), param.TypeInfo.BuildDbt(param.LENGTH, param.PRECISION, param.SCALE),
                        ((ParameterMode)param.PARAMETER_MODE == ParameterMode.InOut).ToClrString());
                }

                _csharp.AppendLine("});}");

                // ctor#1
                _csharp
                    .AppendFormatLine("internal {0}()", procName)
                    .AppendLine("{")
                    .AppendLine("NodeID = _NodeID;")
                    .AppendLine("}");

                // .Go
                _csharp
                    .AppendFormatLine("public QueryTalk.Result Go({0}) {{",
                        String.Join(",", proc.InParameters.Select(p => String.Format("{0} {1}",
                            p.TypeInfo.ProcType((ParameterMode)p.PARAMETER_MODE), ((IName)p).NodeName)).ToArray()))
                    .AppendFormatLine("return base.Go({0});", _ConcatenateParamsWithAssembly(proc.InParameters, "{0}"))
                    .AppendLine("}");

                // .Go<T>
                _csharp
                    .AppendFormatLine("public QueryTalk.Result<T> Go<T>({0}) {{",
                        String.Join(",", proc.InParameters.Select(p => String.Format("{0} {1}", 
                            p.TypeInfo.ProcType((ParameterMode)p.PARAMETER_MODE), ((IName)p).NodeName)).ToArray()))
                    .AppendFormatLine("return base.Go<T>({0});", _ConcatenateParamsWithAssembly(proc.InParameters, "{0}"))
                    .AppendLine("}");

                // .Pass
                _csharp
                    .AppendFormatLine("public QueryTalk.Wall.PassChainer Pass({0}) {{", _ConcatenateParams(proc.InParameters, "QueryTalk.ParameterArgument {0}"))
                    .AppendFormatLine("return base.Pass({0});", _ConcatenateParams(proc.InParameters, "{0}"))
                    .AppendLine("}");

                // end table class
                _csharp.AppendLine("}");
            }
            // progress
            _mappingHandler.SetProgress(5);
        }

    }
}
