using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryTalk.Wall;

namespace QueryTalk.Mapper
{
    // Represents a relation between the node (NODE_ID) and the related node (RELATED_ID).
    internal class Relation 
    {
        public int RELATION_ID { get; set; }
        public int NODE_ID { get; set; }        // FK table
        public int RELATED_ID { get; set; }     // RK table 
        public bool HAS_MANY { get; set; }      // are there many relations between node and related node
        public int RELATION_TYPE { get; set; }

        // columns
        internal List<RelationColumn> Columns { get; set; }

        // mirrored relation (always given except self-relations)
        internal Relation MirroredRelation { get; set; }

        // 1 when the relation is mirrored
        internal int MirroringIndex { get; set; }
        internal bool IsMirrored
        {
            get
            {
                return MirroringIndex == 1;
            }
        }

        // true if this relation is set as a LINK relation (primordial - if there are multiple relations)
        internal bool IsLink { get; set; }

        // is relation compliant
        private Nullable<bool> _isCompliant;
        internal bool IsCompliant(Dictionary<int, TableOrView> tables) 
        {
            if (_isCompliant == null)
            {
                // store it
                _isCompliant = tables[RELATED_ID].IsCompliant;
            }

            return (bool)_isCompliant;
        }

        // true if Self relation
        internal bool IsSelf
        {
            get
            {
                return NODE_ID == RELATED_ID;
            }
        }

        // related CLR name
        internal string RelatedName { get; set; }

        // related node's CLR name that appears in Data classes
        // (set by ClrName.ProvideUniqueDataName method)
        internal string RelatedNameForData { get; set; }

        // related node's CLR name that appears in Designer classes
        // (set by ClrName.ProvideUniqueDesignerName method)
        internal string RelatedNameForDesigner { get; set; }

        #region Exposed ForeignKey

        // given if there are many relations
        // (set by .SetForeignKey method)
        internal Column ExposedForeignKeyColumn { get; private set; }

        // exposed foreign key is a foreign key that appears in the name of the relation
        // when there are more than one relation between the two tables
        //private string _exposedForeignKey;
        internal string ExposedForeignKey
        {
            get
            {
                return ((IName)ExposedForeignKeyColumn).Name;
            }
        }

        // set exposed foreign key and returns FK node (in case of many relations)
        private void SetForeignKey(TableOrView node, TableOrView related)
        {
            if (HAS_MANY)
            {
                if (!IsMirrored)
                {
                    ExposedForeignKeyColumn = node.Columns[ForeignKeyOrdinal - 1];
                }
                else
                {
                    ExposedForeignKeyColumn = related.Columns[ForeignKeyOrdinal - 1];
                }
            }
        }

        // has foreign key
        private bool _hasExposedForeignKey
        {
            get
            {
                return ExposedForeignKeyColumn != null;
            }
        }

        #endregion

        // rename index (for Data class)
        internal int RenameIndexForData { get; set; }

        // rename index (for Designer class)
        internal int RenameIndexForDesigner { get; set; }

        // next relation in the sequence of the relations of the same two nodes
        // (Used when we need to pass through all the relations of a given link.)
        internal Relation Next { get; set; }

        // get related name (for Data class)
        // (late call by Builder.AppendMapperNamespace method)
        internal string GetRelatedNameForData(TableOrView node, TableOrView related)
        {
            string name;

            // related is the same node (self relation)
            if (IsSelf)
            {
                name = ClrName.SELF;
            }
            // related is other node
            else
            {
                name = ((IName)related).NodeName;
            }

            // set foreign key
            SetForeignKey(node, related);

            // rename index
            int? index = null;
            if (RenameIndexForData > 0)
            {
                if (_hasExposedForeignKey)
                {
                    index = RenameIndexForData + 1;   // ByForeignKey2
                }
                else if (RenameIndexForData > 1)
                {
                    index = RenameIndexForData;       // TableRenamed
                }
            }

            if (_hasExposedForeignKey)
            {
                return String.Format("{0}By{1}{2}", name, ExposedForeignKey, index); 
            }
            else if (RenameIndexForData > 0)
            {
                return String.Format("{0}{1}{2}", name, ClrName.RENAMED, index);
            }
            else
            {
                return name;
            }
        }

        // set related name (for Designer class)
        // (late call by Builder.AppendMapperNamespace method)
        internal string GetRelatedNameForDesigner(TableOrView node, TableOrView related)
        {
            string name;

            // related is the same node (self relation)
            if (IsSelf)
            {
                name = ClrName.SELF;
            }
            // related is other node
            else
            {
                name = ((IName)related).NodeName;
            }

            // rename index
            int? index = null;
            if (RenameIndexForDesigner > 0)
            {
                if (_hasExposedForeignKey)
                {
                    index = RenameIndexForDesigner + 1;   // ByForeignKey2
                }
                else if (RenameIndexForDesigner > 1)
                {
                    index = RenameIndexForDesigner;       // TableRenamed
                }
            }

            if (_hasExposedForeignKey)
            {
                return String.Format("{0}By{1}{2}", name, ExposedForeignKey, index);
            }
            else if (RenameIndexForData > 0)
            {
                return String.Format("{0}{1}{2}", name, ClrName.RENAMED, index);
            }
            else
            {
                return name;
            }
        }

        // set related name (for Designer class)
        // (late call by Builder.AppendMapperNamespace method)
        internal string GetRelatedNameForDesigner_DEPR(TableOrView related)
        {
            string name;

            // related is the same node (self relation)
            if (IsSelf)
            {
                name = ClrName.SELF;
            }
            // related is other node
            else
            {
                name = ((IName)related).NodeName;
            }

            if (RenameIndexForDesigner > 0)
            {
                return String.Format("{0}{1}{2}", name, ClrName.RENAMED, 
                    RenameIndexForDesigner > 1 ? RenameIndexForDesigner.ToString() : "");
            }
            else
            {
                return name;
            }
        }

        #region Foreign Key

        // returns the ordinal of the first foreign key
        // NOTE: FOREIGN KEY IS USED ALL OVER THE LIBRARY!
        // To access the FK column one should use: (IName)table.Columns[relation.ForeignKeyOrdinal - 1]
        private int _foreignKeyOrdinal;
        internal int ForeignKeyOrdinal
        {
            get
            {
                if (_foreignKeyOrdinal == 0)
                {
                    // FK->RK
                    if (!IsMirrored)
                    {
                        _foreignKeyOrdinal = Columns[0].COLUMN_ORDINAL;
                    }
                    // RK->FK
                    else
                    {
                        _foreignKeyOrdinal = Columns[0].RELATED_COLUMN_ORDINAL;
                    }
                }

                return _foreignKeyOrdinal;
            }
        }

        // DB3 index of the foreign key
        private int _foreignKeyColumnZ;

        // called by AppendRelations method
        internal int GetForeignKeyColumnsZ(Dictionary<int, TableOrView> tables)
        {
            // store it
            if (_foreignKeyColumnZ == 0)
            {
                // FK->RK
                if (!IsMirrored)
                {
                    _foreignKeyColumnZ = ((INode)tables[NODE_ID].Columns[ForeignKeyOrdinal - 1]).NodeID;
                }
                // RK->FK
                else
                {
                    _foreignKeyColumnZ = ((INode)tables[RELATED_ID].Columns[ForeignKeyOrdinal - 1]).NodeID;
                }
            }

            return _foreignKeyColumnZ;
        }

        // foreign key of this relation
        // param
        //    fknode : FK table of this relation
        internal Column GetForeignKey(TableOrView fknode)
        {
            return fknode.Columns[ForeignKeyOrdinal - 1];
        }

        // return the column that is a part of a FK
        internal Column GetForeignColumn(TableOrView fknode, int ordinal)
        {
            return fknode.Columns[ordinal - 1];
        }

        #endregion

        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}", NODE_ID, RELATED_ID, RELATION_TYPE);
        }
    }
}
