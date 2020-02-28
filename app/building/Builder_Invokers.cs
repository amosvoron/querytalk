using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryTalk.Mapper
{
    internal partial class Builder
    {
        private void AppendInvokers()
        {
            //Thread.Sleep(300);
            _mappingHandler.SetProgress(5);

            _csharp.AppendLine("internal static class Invokers {");

            Relation linkRelation = null;           // the first, IsLink=true relation (in multiple relations)
            foreach (var relation in _mappingHandler.DbInfo.Table5
                .Where(r => r.IsMirrored == false)  // only FK-RK direction 
                .OrderBy(r => r.NODE_ID)            // first order by FK table
                .ThenBy(r => r.RELATED_ID)          // then order by RK table
                .ThenByDescending(r => r.IsLink))   // needed to be able to isolate the first, IsLink=true relation (in multiple relations)
            {
                // store link relation
                if (relation.IsLink)
                {
                    linkRelation = relation;
                }

                //var ccount = relation.Columns.Count - 1;
                var fknode = TableObjects[relation.NODE_ID];
                var rnode = TableObjects[relation.RELATED_ID];

                _csharp.AppendFormatLine("internal static QueryTalk.Wall.GraphInvoker GetGraphInvoker_{0}(QueryTalk.Wall.DB3 fkID, QueryTalk.Wall.DB3 refID) {{", relation.RELATION_ID);
                _csharp.AppendLine("return new QueryTalk.Wall.GraphInvoker(fkID, refID, (fko, ro) => {");

                // compound key preparation
                string orderByRK = String.Format("(System.Collections.Generic.IEnumerable<{0}.{1}>)ro", DataNamespace, ((IName)rnode).NodeName);
                string orderByFK = String.Format("(System.Collections.Generic.IEnumerable<{0}.{1}>)fko", DataNamespace, ((IName)fknode).NodeName);

                // ------------------------------------------------------------------------------------------------------------------------------------
                // loop through all relation columns
                // -----------------------------------
                //if ((QueryTalk.Wall.Api.IsGreater(fk[i].OwnerFirstName, row.FirstName)) ||
                //   ((QueryTalk.Wall.Api.IsEqual(fk[i].OwnerFirstName, row.FirstName)) && (QueryTalk.Wall.Api.IsGreater(fk[i].OwnerLastName, row.LastName))) ||
                //   ((QueryTalk.Wall.Api.IsEqual(fk[i].OwnerFirstName, row.FirstName)) && (QueryTalk.Wall.Api.IsEqual(fk[i].OwnerLastName, row.LastName)) && (fk[i].OwnerDateOfBirth > row.DateOfBirth)) ||
                //   ((QueryTalk.Wall.Api.IsEqual(fk[i].OwnerFirstName, row.FirstName)) && (QueryTalk.Wall.Api.IsEqual(fk[i].OwnerLastName, row.LastName)) && (fk[i].OwnerDateOfBirth == row.DateOfBirth) && (fk[i].OwnerCountryOfBirth > row.CountryOfBirthID)))
                // -----------------------------------
                
                // OUTER LOOP: provides equal expressions
                var ccount = relation.Columns.Count;
                var lessBuilder = new StringBuilder(); var greaterBuilder = new StringBuilder(); var equalBuilder = new StringBuilder(); var requalBuilder = new StringBuilder();  
                RelationColumn prevColumn = null;
                int cc = 0;  // from 0..ccount-1
                foreach (var column in relation.Columns)
                {
                    // provide disjunction operator between the logical blocks
                    if (cc > 0)
                    {
                        lessBuilder.AppendLine(" || ");
                        greaterBuilder.AppendLine(" || ");
                        equalBuilder.Append(" && ");
                        requalBuilder.Append(" && ");
                    }

                    var rk0 = _GetRKColumn(relation, rnode, cc);
                    var fk0 = _GetFKColumn(relation, fknode, cc);
                    var rkName0 = ((IName)rk0).NodeName;
                    var fkName0 = ((IName)fk0).NodeName;
                    var hasBytes = fk0.TypeInfo.DT.IsBytes();
                    orderByRK = LinqOrderBy(orderByRK, String.Format("a => a.{0}", rkName0), hasBytes, cc == 0);
                    orderByFK = LinqOrderBy(orderByFK, String.Format("a => a.{0}", fkName0), hasBytes, cc == 0);

                    // append equals
                    equalBuilder.Append(_FKEqual(rk0, fk0));
                    requalBuilder.Append(_RKEqual(rk0));

                    // prepare variables
                    string less = null;
                    string greater = null;
                    string equal = null;
                    string rkEqual = null;   // row key equality

                    // INNER LOOP : provides Greater/Less expressions
                    for (int c = 0; c <= cc; ++c)
                    {
                        var rk = _GetRKColumn(relation, rnode, c);
                        var fk = _GetFKColumn(relation, fknode, c);
                        var rkName = ((IName)rk).NodeName;
                        var fkName = ((IName)fk).NodeName;
                        var first = (c == 0);
                        var last = (c == cc);

                        // provide &&
                        if (!first)
                        {
                            less += " && "; greater += " && "; equal += " && "; rkEqual += " && ";
                        }
                        
                        // only the last element has "not-equal" method
                        if (last)
                        {
                            less += _Less(rk, fk);
                            greater += _Greater(rk, fk);
                        }
                        else
                        {
                            less += _Equal(rk, fk);
                            greater += _Equal(rk, fk);
                        }
                    }

                    // append less/greater
                    lessBuilder.AppendFormatLine("{0}", less);
                    greaterBuilder.AppendFormatLine("{0}", greater);
                    
                    ++cc;
                    prevColumn = column;
                }
                // ------------------------------------------------------------------------------------------------------------------------------------ END LOOP

                _csharp.AppendFormatLine("var r = {0};", LinqToList(orderByRK));
                _csharp.AppendFormatLine("var fk = {0};", LinqToList(orderByFK));
                _csharp.AppendFormatLine("{0}.{1} fkc; {0}.{2} p = null; var n = fk.Count; int i = 0;", 
                    DataNamespace, ((IName)fknode).NodeName, ((IName)rnode).NodeName);                                                            
                _csharp.AppendLine("foreach (var row in r) {");
                _csharp.AppendLine("bool e = false;");                                                                                       
                _csharp.AppendFormatLine("if (p != null && ({0})) {{ i = 0; }}", requalBuilder.ToString());                                

                // is One part (of the relation) in relation to Many? (False in case of self-relations and one-to-one relations.)
                var hasMany = !relation.IsSelf; // && !relation.IsSingleToOne(); (DEPRECATED SINGLE RELATION)
                string many = null;                         // FK table (as HashSet)
                string one = relation.RelatedNameForData;   // RK table

                // one-to-many
                if (hasMany)
                {
                    many = relation.MirroredRelation.RelatedNameForData;

                    if (!relation.IsSingle())
                    {
                        _csharp.AppendFormatLine("if (row.{0} == null) {{ row.{0} = new System.Collections.Generic.HashSet<QueryTalk.Db.{1}.Data.{2}>(); }}",
                            many, _dbName, ((IName)fknode).NodeName);
                    }
                }

                _csharp.AppendLine("do {");
                _csharp.AppendLine("if (i >= n) { break; }");                                   
                _csharp.AppendFormatLine("while ({0}) {{ ++i; if (i >= n) {{ e = true; break; }} }}", lessBuilder.ToString());  
                _csharp.AppendLine("if (e) { break; }");                                         
                _csharp.AppendLine("fkc = fk[i];");
                _csharp.AppendFormatLine("if ({0}) {{ continue; }}", greaterBuilder.ToString());

                if (hasMany)
                {
                    if (relation.IsSingle())
                    {
                        _csharp.AppendFormatLine("row.{0} = fkc;", many);
                    }
                    else // one-to-many / many-to-one
                    {
                        _csharp.AppendFormatLine("row.{0}.Add(fkc);", many);
                    }
                }

                _csharp.AppendFormatLine("fkc.{0} = row;", one);
                _csharp.AppendLine("++i;");

                // equal
                _csharp.AppendFormatLine("}} while ({0});", equalBuilder.ToString());
                _csharp.AppendLine("p = row;");

                _csharp.AppendLine("}");        // end foreach
                _csharp.AppendLine("});}");     // end invoker
            }

            _csharp.AppendLine("}");
        }

        private Column _GetFKColumn(Relation relation, TableOrView fknode, int c)
        {
            return relation.GetForeignColumn(fknode, relation.Columns[c].COLUMN_ORDINAL);
        }

        private Column _GetRKColumn(Relation relation, TableOrView rnode, int c)
        {
            return relation.GetForeignColumn(rnode, relation.Columns[c].RELATED_COLUMN_ORDINAL);
        }

        // while (fk[i].CountryID < row.CountryID) { ++i; if (i >= n) { e = true; break; } }
        private static string _Less(Column rk, Column fk)
        {
            var rkName = ((IName)rk).NodeName;
            var fkName = ((IName)fk).NodeName; 

            if (fk.TypeInfo.IsSpecial)
            {
                return String.Format("(QueryTalk.Wall.Api.IsLess(fk[i].{0}, row.{1}))", fkName, rkName);
            }
            else
            {
                if (fk.IS_NULLABLE && fk.TypeInfo.DataType == DataGroup.ValueType)
                {
                    return String.Format("(fk[i].{0} == null || fk[i].{0} < row.{1})", fkName, rkName);
                }
                else
                {
                    return String.Format("(fk[i].{0} < row.{1})", fkName, rkName);
                }
            }
        }

        // if (fk[i].CountryID > row.CountryID) { continue; }
        private static string _Greater(Column rk, Column fk)
        {
            var rkName = ((IName)rk).NodeName;
            var fkName = ((IName)fk).NodeName;

            if (fk.TypeInfo.IsSpecial)
            {
                return String.Format("(QueryTalk.Wall.Api.IsGreater(fk[i].{0}, row.{1}))", fkName, rkName);
            }
            else
            {
                if (fk.IS_NULLABLE && fk.TypeInfo.DataType == DataGroup.ValueType)
                {
                    return String.Format("(fk[i].{0} != null && fk[i].{0} > row.{1})", fkName, rkName);
                }
                else
                {
                    return String.Format("(fk[i].{0} > row.{1})", fkName, rkName);
                }
            }
        }

        // while (fkc.CountryID == row.CountryID);
        private static string _Equal(Column rk, Column fk)
        {
            var rkName = ((IName)rk).NodeName;
            var fkName = ((IName)fk).NodeName;

            if (fk.TypeInfo.IsSpecial)
            {
                return String.Format("(QueryTalk.Wall.Api.IsEqual(fk[i].{0}, row.{1}))", fkName, rkName);
            }
            else
            {
                return String.Format("(fk[i].{0} == row.{1})", fkName, rkName);
            }
        }

        // while (fkc.CountryID == row.CountryID);
        private static string _FKEqual(Column rk, Column fk)
        {
            var rkName = ((IName)rk).NodeName;
            var fkName = ((IName)fk).NodeName;

            if (fk.TypeInfo.IsSpecial)
            {
                return String.Format("(QueryTalk.Wall.Api.IsEqual(fkc.{0}, row.{1}))", fkName, rkName);
            }
            else
            {
                return String.Format("(fkc.{0} == row.{1})", fkName, rkName);
            }
        }

        // if (p != null && row.CountryID == p.CountryID) 
        private static string _RKEqual(Column rk)
        {
            var rkName = ((IName)rk).NodeName;

            if (rk.TypeInfo.IsSpecial)
            {
                return String.Format("(QueryTalk.Wall.Api.IsEqual(row.{0}, p.{0}))", rkName);
            }
            else
            {
                return String.Format("(row.{0} == p.{0})", rkName);
            }
        }

    }
}
