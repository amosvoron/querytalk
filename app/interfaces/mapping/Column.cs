using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryTalk.Wall;

namespace QueryTalk.Mapper
{
    internal class Column : INode, IName
    {
        public string SCHEMA { get; set; }
        public string OBJECT_NAME { get; set; }
        public string COLUMN_NAME { get; set; }
        public int ORDINAL_POSITION { get; set; }
        public int COLUMN_TYPE { get; set; }
        public bool IS_NULLABLE { get; set; }
        //public int KEY_TYPE { get; set; }
        public bool IS_RK { get; set; }
        public bool IS_UK { get; set; }
        public bool IS_FK { get; set; }
        public string DTYPE { get; set; }
        public int LENGTH { get; set; }
        public int PRECISION { get; set; }
        public int SCALE { get; set; }
        public bool HAS_DEFAULT { get; set; }

        #region INode

        int INode.NodeID { get; set; }

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
                return COLUMN_NAME;
            }
        }
        private string _name;
        string IName.Name 
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                ArgName = ClrName.GetArgName(value);  // set argument name
            }
        }
        
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

        // IsRK
        //internal bool IsRK
        //{
        //    get
        //    {
        //        return (ColumnKeyType)KEY_TYPE == ColumnKeyType.RK
        //            || (ColumnKeyType)KEY_TYPE == ColumnKeyType.RKFK; 
        //    }
        //}

        // is a nullable type taking into account IS_NULLABLE flag and CLR type
        private bool IsNullable
        {
            get
            {
                return IS_NULLABLE && _typeInfo.DataType == DataGroup.ValueType;
            }
        }

        // returns "?" if column CLR type is nullable or "" if not
        internal string NullableSign
        {
            get
            {
                return IsNullable ? "?" : "";
            }
        }

        // column argument name (small caps)
        internal string ArgName { get; private set; }

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
            _typeInfo = TypeMapping.GetClrTypeInfo(DTYPE, LENGTH, PRECISION, SCALE);
        }

        // column type
        internal ColumnType ColumnType
        {
            get
            {
                return (ColumnType)COLUMN_TYPE;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}.{1}: {2}", SCHEMA, OBJECT_NAME, COLUMN_NAME);
        }

    }
}
