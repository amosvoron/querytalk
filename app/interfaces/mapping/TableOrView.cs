using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryTalk.Mapper
{
    internal class TableOrView : INode, IName, IColumn, IFinalizer
    {
        public int OBJECT_ID { get; set; }
        public string SCHEMA { get; set; }
        public string OBJECT_NAME { get; set; }
        public int OBJECT_TYPE { get; set; }

        #region IFinalizer

        // is object compliant
        private bool _isCompliant = true;
        internal bool IsCompliant
        {
            get
            {
                return _isCompliant;
            }
        }

        // set non-compliant: used only in MappingHandler.ProcessParamNames method
        void IColumn.SetNotCompliant()
        {
            _isCompliant = false;
        }

        // true if RK is provided as ALL columns due to the absence of the PK/UK
        internal bool HasGenuineRK { get; private set; }

        // provide IsCompliant flag and some other stuff (CompliantColumns, RKColumns) 
        // (Note: Columns are already added.)
        void IFinalizer.Finalizer()
        {
            // ------------------------------------------
            // set IsCompliant flag
            // Rule: all columns must be compliant.
            // ------------------------------------------
            if (_isCompliant)
            {
                _isCompliant = !Columns.Exists(a => !a.TypeInfo.IsCompliant);
            }

            // ser ordered columns (by Name)
            OrderedColumns = Columns        
                .OrderBy(c => ((IName)c).Name)
                .ToList();

            // set hasRK flag: true if there is any RK column 
            bool hasRK = Columns.Exists(c => c.IS_RK);

            // set RK columns
            if (hasRK)
            {
                RKColumns = Columns.Where(c => c.IS_RK).ToList();
                HasGenuineRK = true;
            }
            // not RK => include all columns in RK
            else
            {
                Columns.ForEach(c => c.IS_RK = true);
                RKColumns = Columns;
                HasGenuineRK = false;
            }
        }

        #endregion

        #region Other properties

        // columns ordered by ORDINAL_POSITION (first ordering: needed for optimization)
        // (Note: Columns are added by MappingHandler.ProcessColumnNames method.)
        public List<Column> Columns { get; set; }

        // columns ordered by Name (second ordering: the property order)
        internal List<Column> OrderedColumns { get; private set; }

        // RK columns (all if no PK-UK is given)
        internal List<Column> RKColumns { get; private set; }

        // relations
        internal List<Relation> Relations { get; set; }

        #endregion

        #region IName

        string IName.SCHEMA
        {
            get
            {
                return SCHEMA;
            }
        }
        string IName.OBJECT_NAME
        {
            get
            {
                return OBJECT_NAME;
            }
        }

        string IName.Name { get; set; }
        bool IName.HasSchemaName { get; set; }
        int IName.RenameIndex { get; set; }  
        string IName.NodeName { get; set; }

        #endregion

        #region INode

        int INode.NodeID { get; set; }

        #endregion

        // ctor
        public TableOrView()
        { }

        public override string ToString()
        {
            return String.Format("{0}.{1}", SCHEMA, OBJECT_NAME);
        }

    }
}
