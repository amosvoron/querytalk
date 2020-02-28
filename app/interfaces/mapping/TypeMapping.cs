using System;
using System.Collections.Generic;
using QueryTalk.Wall;

namespace QueryTalk.Mapper
{
    internal static class TypeMapping
    {
        // data type info as given by INFORMATION_SCHEMA.COLUMNS
        internal static Dictionary<string, TypeInfo> SqlTypes;

        static TypeMapping()
        {
            SqlTypes = new Dictionary<string, TypeInfo>();

            // none arguments
            SqlTypes.Add("bit", new TypeInfo(DT.Bit, "System.Boolean", TypeArgs.None, DataGroup.ValueType, true));
            SqlTypes.Add("tinyint", new TypeInfo(DT.Tinyint, "System.Byte", TypeArgs.None, DataGroup.ValueType, false));
            SqlTypes.Add("smallint", new TypeInfo(DT.Smallint, "System.Int16", TypeArgs.None, DataGroup.ValueType, false));
            SqlTypes.Add("int", new TypeInfo(DT.Int, "System.Int32", TypeArgs.None, DataGroup.ValueType, false));
            SqlTypes.Add("bigint", new TypeInfo(DT.Bigint, "System.Int64", TypeArgs.None, DataGroup.ValueType, false));
            SqlTypes.Add("real", new TypeInfo(DT.Real, "System.Single", TypeArgs.None, DataGroup.ValueType, false));
            SqlTypes.Add("float", new TypeInfo(DT.Float, "System.Double", TypeArgs.None, DataGroup.ValueType, false));
            SqlTypes.Add("money", new TypeInfo(DT.Money, "System.Decimal", TypeArgs.None, DataGroup.ValueType, false));
            SqlTypes.Add("smallmoney", new TypeInfo(DT.Smallmoney, "System.Decimal", TypeArgs.None, DataGroup.ValueType, false));
            SqlTypes.Add("date", new TypeInfo(DT.Date, "System.DateTime", TypeArgs.None, DataGroup.ValueType, false));
            SqlTypes.Add("smalldatetime", new TypeInfo(DT.Smalldatetime, "System.DateTime", TypeArgs.None, DataGroup.ValueType, false));
            SqlTypes.Add("datetime", new TypeInfo(DT.Datetime, "System.DateTime", TypeArgs.None, DataGroup.ValueType, false));
            SqlTypes.Add("sysname", new TypeInfo(DT.Sysname, "System.String", TypeArgs.None, DataGroup.ReferenceType, true));
            SqlTypes.Add("timestamp", new TypeInfo(DT.Timestamp, "System.Byte[]", TypeArgs.None, DataGroup.ReferenceType, true));
            SqlTypes.Add("uniqueidentifier", new TypeInfo(DT.Uniqueidentifier, "System.Guid", TypeArgs.None, DataGroup.ValueType, true));
            SqlTypes.Add("varchar(max)", new TypeInfo(DT.VarcharMax, "System.String", TypeArgs.None, DataGroup.ReferenceType, true));
            SqlTypes.Add("nvarchar(max)", new TypeInfo(DT.NVarcharMax, "System.String", TypeArgs.None, DataGroup.ReferenceType, true));
            SqlTypes.Add("varbinary(max)", new TypeInfo(DT.VarbinaryMax, "System.Byte[]", TypeArgs.None, DataGroup.ReferenceType, true));
            SqlTypes.Add("xml", new TypeInfo(DT.Xml, "System.String", TypeArgs.None, DataGroup.ReferenceType, false));
            SqlTypes.Add("sql_variant", new TypeInfo(DT.Sqlvariant, "System.Object", TypeArgs.None, DataGroup.ReferenceType, false));

            // single argument
            SqlTypes.Add("char", new TypeInfo(DT.Char, "System.String", TypeArgs.Single, DataGroup.ReferenceType, true));
            SqlTypes.Add("nchar", new TypeInfo(DT.NChar, "System.String", TypeArgs.Single, DataGroup.ReferenceType, true));
            SqlTypes.Add("varchar", new TypeInfo(DT.Varchar, "System.String", TypeArgs.Single, DataGroup.ReferenceType, true));
            SqlTypes.Add("nvarchar", new TypeInfo(DT.NVarchar, "System.String", TypeArgs.Single, DataGroup.ReferenceType, true));
            SqlTypes.Add("binary", new TypeInfo(DT.Binary, "System.Byte[]", TypeArgs.Single, DataGroup.ReferenceType, true));
            SqlTypes.Add("varbinary", new TypeInfo(DT.Varbinary, "System.Byte[]", TypeArgs.Single, DataGroup.ReferenceType, true));
            SqlTypes.Add("datetime2", new TypeInfo(DT.Datetime2, "System.DateTime", TypeArgs.Single, DataGroup.ValueType, false));
            SqlTypes.Add("datetimeoffset", new TypeInfo(DT.Datetimeoffset, "System.DateTimeOffset", TypeArgs.Single, DataGroup.ValueType, false));
            SqlTypes.Add("time", new TypeInfo(DT.Time, "System.TimeSpan", TypeArgs.Single, DataGroup.ValueType, false));

            // two arguments
            SqlTypes.Add("decimal", new TypeInfo(DT.Decimal, "System.Decimal", TypeArgs.Two, DataGroup.ValueType, false));
            SqlTypes.Add("numeric", new TypeInfo(DT.Numeric, "System.Decimal", TypeArgs.Two, DataGroup.ValueType, false));

            // DEPRECATED BY MICROSOFT (supported by QueryTalk)
            SqlTypes.Add("text", new TypeInfo(DT.Text, "System.String", TypeArgs.None, DataGroup.ReferenceType, false));
            SqlTypes.Add("ntext", new TypeInfo(DT.NText, "System.String", TypeArgs.None, DataGroup.ReferenceType, false));
            SqlTypes.Add("image", new TypeInfo(DT.Image, "System.Byte[]", TypeArgs.None, DataGroup.ReferenceType, false));
        }

        // general method for processing CLR type on columns and params
        internal static TypeInfo GetClrTypeInfo(string dtype, int length, int precision, int scale)
        {
            // just in case we make letter case conversion
            var sqlTypeName = dtype.ToLowerInvariant();

            // detect MAX types (varchar, nvarchar, varbinary)
            if ((sqlTypeName == "varchar" || sqlTypeName == "nvarchar" || sqlTypeName == "varbinary")
                && (length == -1))
            {
                sqlTypeName = String.Format("{0}(max)", sqlTypeName);
            }

            // if sql type is UNKNOWN we skip the column => will be set is non-compliant (IsCompliant = true)
            if (!SqlTypes.ContainsKey(sqlTypeName))
            {
                return new TypeInfo();    // null means skip the column (=> empty type info object)
            }

            return SqlTypes[sqlTypeName];
        }

        // returns true if db data type maps to Byte[]
        internal static bool IsBytes(this DT dt)
        {
            return dt == DT.Binary
                || dt == DT.Varbinary
                || dt == DT.VarbinaryMax
                || dt == DT.Timestamp
                || dt == DT.Image;
        }
    }
}
