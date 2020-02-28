using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QueryTalk.Wall;

namespace QueryTalk.Mapper
{
    // ------------------------------------------
    // Mapper names are case-sensitive (!!!) 
    // ------------------------------------------
    internal static class ClrName
    {
        // the character used in CLR name conversion
        private const string REGEX = @"[^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Nl}\p{Mn}\p{Mc}\p{Cf}\p{Pc}\p{Lm}]";
        internal const string UNDERSCORE = "_";
        internal const string RENAMED = "Renamed";
        private const string RENAMED_REGEX = @"Renamed\d*$";
        internal const string SELF = "Self";

        private static string[] RESERVED_OBJECT_NAMES = new string[] 
        { 
            "System",
            "QueryTalk",
            "Mapper",
            "Data",
            "Table", 
            "View", 
            "TableFunc", 
        	"ScalarFunc",
	        "Proc",
            "Self",
            "Designer",
            "Design",
            "InsertChildren",
            "Pass",
            "TRoot",
            "TNode",
            "TNew",
            "Map",
            "ReferenceEquals",
            "Equals",
            "ClientAssembly",
            "Not",
            "Or",
            "At",
            "GoAt"
        };

        // provides the name of the private field (underscore + small letter)
        internal static string GetFieldName(int index)
        {
            return String.Format("_f{0}", index);
        }

        // provides the name of the method argument/parameter (@ + small letter)
        // Note: by @ we avoid collisions with the reserved names
        internal static string GetArgName(string name)
        {
            var name2 = name.Substring(0, 1).ToLowerInvariant();
            if (name.Length > 1)
            {
                name2 = name2 + name.Substring(1);
            }

            return "@" + name2;
        }

        #region Process Object/Column/Param Names

        // provide a unique CLR name for every object inside the set of db objects
        // following the rules:
        //   (1) Every object identifier has only <object name>.
        //   (2) If two objects have the same name, a <schema name> is appended as a suffix.
        //   (3) If the object identifier already exists, a <schema name> is appended as a suffix.
        //   (4) If the object identifier still exist, a _REN<n> suffix is provided.
        internal static void ProcessObjectNames(IEnumerable<IName> setOfNames, string dbName)
        {
            var names = new HashSet<string>();
            int nodeID = 1;

            // main loop
            var j = 1;
            var set = setOfNames.OrderBy(a => a.OBJECT_NAME).ToList();
            var count = set.Count;
            foreach (var name in set)
            {
                // get Name
                name.Name = Api.GetClrName(name.OBJECT_NAME);

                // is schema needed: check OBJECT_NAME duplicates
                if (j < count)
                {
                    var next = set[j];
                    if (String.Compare(name.OBJECT_NAME, next.OBJECT_NAME, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        name.HasSchemaName = true;
                        next.HasSchemaName = true;
                    }
                }
                ++j;

                // provide unique name
                ProvideUniqueName(name, names, dbName);

                // set NodeID (! - every object must have its nodeID)
                ((INode)name).NodeID = nodeID;
                ++nodeID;
            }
        }

        // check column CLR name 
        // params
        //   reserved : list of internal reserved names (e.g. database name and its renamed name)
        private static bool IsValidClrObjectName(string name, string dbName)
        {
            // is the same as database name (if given)
            if (name == dbName)
            {
                return false;   // not ok
            }
            // renamed name is valid
            if (Regex.IsMatch(name, RENAMED_REGEX))
            {
                return true;
            }
            // is QueryTalk_* (RESERVED)
            else if (Regex.IsMatch(name, "^QueryTalk_"))
            {
                return false;   // not ok
            }
            // is RESERVED
            else if (RESERVED_OBJECT_NAMES.Contains(name))
            {
                return false;   // not ok
            }

            return true;  // ok
        }

        // provide unique name (for IName)
        private static void ProvideUniqueName(IName name, HashSet<string> names, string dbName)
        {
            // default CLR name
            var name2 = ClrName.GetNodeName(name);

            var renameIndex = 1;
            while (true)
            {
                //if (!names.Add(name2.ToUpperInvariant()) || !IsValidClrObjectName(name2, dbName))
                if (!names.Add(name2) || !IsValidClrObjectName(name2, dbName))
                {
                    name.RenameIndex = renameIndex;     // set node's renaming index (for late renaming)
                    name2 = Rename(name2, renameIndex++);
                }
                else
                {
                    name.NodeName = name2;              // set node name
                    break;
                }
            }
        }

        // provide unique related name (for Data class)
        internal static void ProvideUniqueNameForData(Relation relation, TableOrView node, TableOrView related, HashSet<string> names)
        {
            // set related node name
            int i = 1;
            while (true)
            {
                // get related name
                var relatedName = relation.GetRelatedNameForData(node, related);

                // check uniqueness
                //if (!(names.Add(relatedName.ToUpperInvariant())))
                if (relatedName == ((IName)node).NodeName || !(names.Add(relatedName)))
                {
                    relation.RenameIndexForData = i;
                }
                else // ok
                {
                    relation.RelatedNameForData = relatedName;  // set related name
                    break;
                }

                ++i;
            }
        }

        // provide unique related name (for Designer class)
        // return foreign key column (or null) : foreign key is needed (in case of multiple relations)
        internal static Column ProvideUniqueNameForDesigner(Relation relation, TableOrView node, TableOrView related, HashSet<string> names)
        {
            // set related node name
            int i = 1;
            while (true)
            {
                // get related name
                //var relatedName = relation.GetRelatedNameForDesigner(related);
                var relatedName = relation.GetRelatedNameForDesigner(node, related);

                // check uniqueness
                if (relatedName == ((IName)node).NodeName || !(names.Add(relatedName)))
                {
                    relation.RenameIndexForDesigner = i;
                }
                else // ok
                {
                    relation.RelatedNameForDesigner = relatedName;  // set related name
                    break;
                }

                ++i;
            }

            // return foreign key (null if not exposed)
            return relation.ExposedForeignKeyColumn;
        }

        // get final node name
        internal static string GetNodeName(IName name)
        {
            // index that will be added to the end of the name
            int? index = null;
            if (name.RenameIndex > 0)
            {
                if (name.HasSchemaName)
                {
                    index = name.RenameIndex + 1;   // TableOfSchema2
                }
                else if (name.RenameIndex > 1)
                {
                    index = name.RenameIndex;       // TableRenamed
                }
            }

            if (name.HasSchemaName)
            {
                // append Of<Schema><i> and remove multiple underscores
                return Regex.Replace(String.Format(String.Format("{0}Of{1}{2}", 
                    name.Name, Api.GetClrName(name.SCHEMA), index)), "_{2,}", "_");
            }
            else if (name.RenameIndex > 0)
            {
                // append Renamed<i> and remove multiple underscores
                return Regex.Replace(String.Format(String.Format("{0}{1}{2}", name.Name, RENAMED, index)), "_{2,}", "_");
            }
            else
            {
                return name.Name; 
            }
        }

        // append schema name
        internal static string AppendSchema(string clrObjectName, string clrSchemaName)
        {
            // append CLR schema
            var name = String.Format(String.Format("{0}_{1}", clrObjectName, clrSchemaName));

            // remove reduntant underscore characters
            name = Regex.Replace(name, "_{2,}", "_");

            return name.TrimEnd('_');
        }

        // check param CLR name 
        internal static bool CheckParamName(string name, string tableName)
        {
            // is the same as the table name
            if (name == tableName)
            {
                return false;   // not ok
            }

            return true;  // ok
        }

        // provides the renamed name with a form <name>Rename<ix>
        // NOTE: COPY OF THE QueryTalk.Wall.Naming.Rename method (!!!)
        internal static string Rename(string name, int ix)
        {
            var renReg = new Regex(RENAMED_REGEX);
            bool isRenamed = renReg.IsMatch(name);

            if (ix == 1)
            {
                if (isRenamed)
                {
                    return renReg.Replace(name.TrimEnd('_'), RENAMED);
                }
                else
                {
                    return String.Format("{0}{1}", name.TrimEnd('_'), RENAMED);
                }
            }
            else
            {
                if (isRenamed)
                {
                    return String.Format("{0}{1}", renReg.Replace(name.TrimEnd('_'), RENAMED), ix);
                }
                else
                {
                    return String.Format("{0}{1}{2}", name.TrimEnd('_'), RENAMED, ix);
                }
            }
        }

        #endregion

    }
}
