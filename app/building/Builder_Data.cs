using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryTalk.Mapper
{
    internal partial class Builder
    {

        private void AppendDataClasses()
        {
            _csharp.AppendLine("public static class Data {");
            _AppendDataTables();
            _AppendDataViews();
            _AppendDataTableFunc();
            AppendNotBrowsable();
            _csharp.AppendLine("}");  
        }

        private void _AppendDataTables()
        {
            foreach (var table in Tables)
            {
                var tableName = ((IName)table).NodeName;
                var columnSet = new HashSet<string>();  // column uniqueness (with relations) checker

                // begin table class
                _csharp
                    .AppendFormat("public sealed class {0} : QueryTalk.Wall.DbRow, QueryTalk.Wall.INode, QueryTalk.Wall.INode<{0}>", tableName)   // modified
                    .AppendLine("{");

                // columns (ORDERED)
                var columnZ = 1;
                foreach (var column in table.OrderedColumns)
                {
                    ((INode)column).NodeID = columnZ;
                    var propertyName = ((IName)column).NodeName;
                    columnSet.Add(propertyName);  // no need to check if added because that was already done by .ProcessColumnNames method
                    var fieldName = ClrName.GetFieldName(columnZ);

                    // field
                    _csharp
                        .AppendLine("[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]")
                        .AppendFormatLine("private {0}{1} {2}; ", column.TypeInfo.Name, column.NullableSign, fieldName);

                    // property
                    _csharp
                        .AppendFormatLine("public {0}{1} {2} {{", column.TypeInfo.Name, column.NullableSign, propertyName)
                        .AppendFormatLine("get {{ return {0}; }}", fieldName)
                        .AppendLine("set {")
                        .AppendFormatLine("if (value != {0}) {{", fieldName)
                        .AppendFormatLine("if (CanModify) {{ OnModify({0}, {1}); }}", columnZ, fieldName)
                        .AppendFormatLine("{0} = value;", fieldName)
                        .AppendLine("}")
                        .AppendFormatLine("OnSet({0});", columnZ)
                        .AppendLine("}}");

                    ++columnZ;
                }

                _AppendDataRelations(table, columnSet);

                // ctor#0 (static)
                _csharp
                    .AppendFormatLine("static {0}() {{", tableName)
                    .AppendFormatLine("QueryTalk.Wall.Api.AddRowType(typeof({1}.{0}), {2}.{0}<{1}.{0}>._NodeID);", tableName, DataNamespace, DesignNamespace)
                    .AppendLine("}");

                // ctor#1
                _csharp
                    .AppendFormatLine("public {0}() {{", tableName)
                    .AppendFormatLine("NodeID = {2}.{0}<{1}.{0}>._NodeID;", tableName, DataNamespace, DesignNamespace)
                    .AppendLine("}");

                // INode.GetNode
                _csharp
                    .AppendLine("[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]")
                    .AppendLine("QueryTalk.Wall.DbNode QueryTalk.Wall.INode.Node { get { ")
                    .AppendFormatLine("return {0}.{1};", _dbName, tableName)
                    .AppendLine("} }");

                // INode.InsertGraph
                _csharp
                    .AppendLine("void QueryTalk.Wall.INode.InsertGraph(System.Reflection.Assembly client, QueryTalk.Wall.ConnectBy connectBy) {")
                    .AppendFormatLine("InsertChildren(client, new {0}[] {{ this }}, connectBy);", tableName)
                    .AppendLine("}");

                // INode<T>.GetNode
                _csharp
                    .AppendLine("[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]")
                    .AppendFormatLine("QueryTalk.Wall.DbTable<{0}> QueryTalk.Wall.INode<{0}>.Node {{ get {{ ", tableName)
                    .AppendFormatLine("return {0}.{1};", _dbName, tableName)
                    .AppendLine("}}");

                // MANY PROPERTY
                // .InsertChildren
                _AppendDataInsertChildren(table);

                _csharp.AppendLine("}");   // end of table class
            }
        }

        private void _AppendDataViews()
        {
            foreach (var view in Views)
            {
                var viewName = ((IName)view).NodeName;

                // begin view class
                _csharp
                    .AppendFormatLine("public sealed class {0} : QueryTalk.Wall.DbRow", viewName)
                    .AppendLine("{");

                // columns (ORDERED)
                foreach (var column in view.OrderedColumns)
                {
                    _csharp
                        .AppendFormatLine("public {0} {1} {{ get; set; }}", column.TypeInfo.Name, ((IName)column).NodeName);
                }

                // ctor#0 (static)
                _csharp
                    .AppendFormatLine("static {0}() {{", viewName)
                    .AppendFormatLine("QueryTalk.Wall.Api.AddRowType(typeof({1}.{0}), {2}.{0}<{1}.{0}>._NodeID);", viewName, DataNamespace, DesignNamespace)
                    .AppendLine("}");

                // ctor#1
                _csharp
                    .AppendFormatLine("public {0}() {{", viewName)
                    .AppendFormatLine("NodeID = {2}.{0}<{1}.{0}>._NodeID;", viewName, DataNamespace, DesignNamespace)
                    .AppendLine("}");

                _csharp.AppendLine("}");   // end of view class
            }
        }

        private void _AppendDataTableFunc()
        {
            foreach (var func in TableFunctions)
            {
                var funcName = ((IName)func).NodeName;

                // begin view class
                _csharp
                    .AppendFormatLine("public sealed class {0} : QueryTalk.Wall.DbRow", funcName)
                    .AppendLine("{");

                // columns (ORDERED)
                foreach (var column in func.OrderedColumns)
                {
                    _csharp
                        .AppendFormatLine("public {0} {1} {{ get; set; }}", column.TypeInfo.Name, ((IName)column).NodeName);
                }

                // ctor#0 (static)
                _csharp
                    .AppendFormatLine("static {0}() {{", funcName)
                    .AppendFormatLine("QueryTalk.Wall.Api.AddRowType(typeof({1}.{0}), {2}.{0}<{1}.{0}>._NodeID);", funcName, DataNamespace, DesignNamespace)
                    .AppendLine("}");

                // ctor#1
                _csharp
                    .AppendFormatLine("public {0}() {{", funcName)
                    .AppendFormatLine("NodeID = {2}.{0}<{1}.{0}>._NodeID;", funcName, DataNamespace, DesignNamespace)
                    .AppendLine("}");

                _csharp.AppendLine("}");   // end of func class
            }
        }

        private void _AppendDataRelations(TableOrView table, HashSet<string> columnSet)
        {
            // relations 
            if (table.Relations != null)
            {
                // loop through publicly exposed relations
                Relation prev = null;
                foreach (var relation in table.Relations)
                // .OrderByDescending(r => r.IsLink))   // DEPRECATED (NOT NEEDED)
                {
                    // check if relation is compliant
                    if (!IsRelationCompliant(relation))
                    {
                        continue;
                    }

                    // related node
                    var related = TableObjects[relation.RELATED_ID];

                    // set related node name (as unique)
                    ClrName.ProvideUniqueNameForData(relation, table, related, columnSet);

                    if (relation.IsOneToMany())
                    {
                        // MANY PROPERTY
                        _csharp.AppendFormatLine("public System.Collections.Generic.HashSet<{0}> {1} {{ get; set; }}", ((IName)related).NodeName, relation.RelatedNameForData);
                    }
                    else
                    {
                        _csharp.AppendFormatLine("public {0} {1} {{ get; set; }}", ((IName)related).NodeName, relation.RelatedNameForData);
                    }

                    prev = relation;
                }
            }
        }

        private void _AppendDataInsertChildren(TableOrView table)
        {
            // method signature
            _csharp.AppendFormatLine("internal static void InsertChildren(System.Reflection.Assembly client, System.Collections.Generic.IEnumerable<{0}> parents, QueryTalk.Wall.ConnectBy connectBy) {{",
                ((IName)table).NodeName);

            // check if any RK->FK relation
            if (table.Relations == null || !table.Relations.Exists(r => r.IsFromRKToFK()))
            {
                _csharp.AppendLine("}");
                return;
            }

            /* 
               cs1: var cc1 = new HashSet<Person>();
               cs2: foreach (var child in parent.Persons) { child.CountryOfBirthID = parent.CountryID; cc1.Add(child); }    
               cs3: QueryTalk.Wall.Crud.GoInsertRows(client, cc1, false, connectBy);
                    Person.InsertChildren(client, cc1, connectBy);          
            */
            var cs1 = new StringBuilder();
            var cs2 = new StringBuilder();
            var cs3 = new StringBuilder();

            var i = 1;
            foreach (var relation in table.Relations
                //.Where(r => r.IsLink)   // DEPRECATED
                .Where(r => r.IsFromRKToFK()))
            {
                // check if relation is compliant
                if (!IsRelationCompliant(relation))
                {
                    continue;
                }

                // get related
                var related = TableObjects[relation.RELATED_ID];
                relation.RelatedName = ((IName)related).NodeName;
                var relatedNameForData = relation.RelatedNameForData;

                // cs1
                cs1.AppendFormatLine("var cc{0} = new System.Collections.Generic.HashSet<{1}>();", i, relation.RelatedName);

                // cs2
                //if (!relation.IsOneToSingle())
                if ((RelationType)relation.RELATION_TYPE != RelationType.OneToSingle)
                {
                    cs2.AppendFormatLine("if (parent.{0} != null) {{", relatedNameForData);
                    cs2.AppendFormatLine("foreach (var child in parent.{0}) {{", relatedNameForData);    // foreach child
                    Relation rel = relation;

                    foreach (var column in rel.Columns)
                    {
                        cs2.AppendFormatLine("child.{0} = parent.{1};",
                            ((IName)related.Columns[column.RELATED_COLUMN_ORDINAL - 1]).NodeName,
                            ((IName)table.Columns[column.COLUMN_ORDINAL - 1]).NodeName);
                    }

                    cs2.AppendFormatLine("cc{0}.Add(child);", i);
                    cs2.AppendLine("}");    // end of foreach child
                    cs2.AppendLine("}");    // end of if
                }
                // add relation OneToSingle
                else
                {
                    cs2.AppendFormatLine("if (parent.{0} != null) {{", relatedNameForData);

                    foreach (var column in relation.Columns)
                    {
                        cs2.AppendFormatLine("parent.{0}.{1} = parent.{2};", relatedNameForData,
                            ((IName)related.Columns[column.RELATED_COLUMN_ORDINAL - 1]).NodeName,
                            ((IName)table.Columns[column.COLUMN_ORDINAL - 1]).NodeName);
                    }

                    cs2.AppendFormatLine("cc{0}.Add(parent.{1});", i, relatedNameForData);
                    cs2.AppendLine("}");
                }

                // cs3
                string isIdentity = "false";
                if ((RelationType)relation.RELATION_TYPE == RelationType.OneToSingle)
                {
                    isIdentity = "true";
                }
                cs3.AppendFormatLine("QueryTalk.Wall.Crud.InsertRowsGo(client, cc{0}, {1}, connectBy);", i, isIdentity);
                cs3.AppendFormatLine("{2}.{0}.InsertChildren(client, cc{1}, connectBy);", relation.RelatedName, i, DataNamespace);

                ++i;
            }

            // final build
            _csharp
                .AppendLine(cs1.ToString())
                .AppendLine("foreach (var parent in parents) {")
                .AppendLine(cs2.ToString())
                .AppendLine("}")
                .AppendLine(cs3.ToString())
                .AppendLine("}");  // end of method
        }

    }
}
