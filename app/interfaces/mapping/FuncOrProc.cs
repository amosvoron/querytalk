using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTalk.Mapper
{
    internal class FuncOrProc : INode, IName, IColumn, IFinalizer
    {
        public string SCHEMA { get; set; }
        public string OBJECT_NAME { get; set; }
        public int OBJECT_TYPE { get; set; }
        public string RETURN_DTYPE { get; set; }
        public int RETURN_LENGTH { get; set; }
        public int RETURN_PRECISION { get; set; }
        public int RETURN_SCALE { get; set; }

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

        // provide IsCompliant flag and some other stuff (CompliantColumns, RKColumns) 
        // (Note: Columns are already added.)
        void IFinalizer.Finalizer()
        {
            // provide empty column collection (if no columns)
            if (Columns == null)
            {
                Columns = new List<Column>();
            }

            // ----------------------------------------------------------
            // set IsCompliant flag
            // Rule: all columns and all parameters must be compliant.
            // ----------------------------------------------------------
            if (_isCompliant)    // skip non-compliant objects
            {
                _isCompliant = !Columns.Exists(a => !a.TypeInfo.IsCompliant);
                if (IsCompliant)
                {
                    _isCompliant = !Parameters.Exists(a => !a.TypeInfo.IsCompliant);
                }
            }

            // ser ordered columns (by Name)
            OrderedColumns = Columns
                .OrderBy(c => ((IName)c).Name)
                .ToList();

            // set InParameters
            InParameters = Parameters
                .Where(p => (ParameterMode)p.PARAMETER_MODE == ParameterMode.In
                    || (ParameterMode)p.PARAMETER_MODE == ParameterMode.InOut)
                .ToList();

            // set return parameter of the scalar func
            if ((ObjectType)OBJECT_TYPE == ObjectType.ScalarFunc)
            {
                ReturnParameter = Parameters
                    .Where(p => (ParameterMode)p.PARAMETER_MODE == ParameterMode.Out)
                    .FirstOrDefault();

                // validity check
                if (ReturnParameter == null)
                {
                    _isCompliant = false;
                }
            }
        }

        #endregion

        #region Other properties

        // columns ordered by ORDINAL_POSITION (first ordering: needed for optimization)
        // (Note: Columns are added by MappingHandler.ProcessColumnNames method.)
        public List<Column> Columns { get; set; }

        // columns ordered by Name (second ordering: the property order)
        internal List<Column> OrderedColumns { get; private set; }

        // parameters ordered by ORDINAL_POSITION
        internal List<Parameter> Parameters { get; set; }

        // func/proc IN/OUTPUT parameters
        internal List<Parameter> InParameters { get; private set; }

        // scalar func RETURN param
        internal Parameter ReturnParameter { get; private set; }

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
        public FuncOrProc()
        {
            // to assure that the property is not null 
            // (every table has columns but not every func or proc has parameters)
            Parameters = new List<Parameter>();
        }

        public override string ToString()
        {
            return String.Format("{0}.{1}", SCHEMA, OBJECT_NAME);
        }

    }
}
