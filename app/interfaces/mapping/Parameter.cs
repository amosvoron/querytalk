using System;

namespace QueryTalk.Mapper
{
    internal class Parameter : IName
    {
        public string SCHEMA { get; set; }
        public string OBJECT_NAME { get; set; }
        public string PARAMETER_NAME { get; set; }
        public int PARAMETER_MODE { get; set; }
        public int ORDINAL_POSITION { get; set; }
        public string DTYPE { get; set; }
        public int LENGTH { get; set; }
        public int PRECISION { get; set; }
        public int SCALE { get; set; }
        public string UDT_SCHEMA { get; set; }
        public string UDT_NAME { get; set; }

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
                return PARAMETER_NAME;
            }
        }

        string IName.Name { get; set; }    
        string IName.NodeName
        {
            get
            {
                return ((IName)this).Name;
            }
            set
            {
                throw new NotImplementedException();    // not used
            }
        }
        bool IName.HasSchemaName { get; set; }  // not used
        int IName.RenameIndex { get; set; }     // not used

        #endregion

        // type info
        internal TypeInfo _typeInfo;
        internal TypeInfo TypeInfo 
        {
            get
            {
                return _typeInfo;
            }
        }
        internal void SetTypeInfo()
        {
            // check user defined type first
            if (DTYPE.ToUpper() == "TABLE TYPE")
            {
                _typeInfo = new TypeInfo(UDT_SCHEMA, UDT_NAME);
                return;
            }

            // CLR compliant types
            _typeInfo = TypeMapping.GetClrTypeInfo(DTYPE, LENGTH, PRECISION, SCALE);
        }

        // returns true if this parameter belongs to the specified node
        internal bool BelongsTo(FuncOrProc node)
        {
            if (node == null)
            {
                return false;
            }

            return SCHEMA == node.SCHEMA && OBJECT_NAME == node.OBJECT_NAME;
        }

        public override string ToString()
        {
            return String.Format("{0}.{1}: {2}", SCHEMA, OBJECT_NAME,
                PARAMETER_NAME ?? String.Format("({0})", (ParameterMode)PARAMETER_MODE));
        }

    }
}
