using System;
using QueryTalk.Wall;

namespace QueryTalk.Mapper
{
    internal class TypeInfo
    {
        internal DT DT { get; set; }
        internal string Name { get; set; }
        internal TypeArgs TypeArgs { get; set; }
        internal bool IsCompliant { get; set; }  // default: false
        internal ColumnType ColumnType { get; set; }

        // udtt
        internal string UdttSchema { get; set; }
        internal string UdttName { get; set; }

        // data type { Value, Reference, Udtt}
        private DataGroup _dataType;
        internal DataGroup DataType
        {
            get
            {
                return _dataType;
            }
        }

        // is sql_variant
        internal bool IsSqlVariant
        {
            get
            {
                return Name == "System.Object";
            }
        }

        // is special data type (needed in graph invoker) {String, Byte[], Guid}
        internal bool IsSpecial { get; set; }

        // parameter type used in functions
        // Note that QueryTalk does not support direct Object type which has to be passed through the QueryTalk.Value object.
        internal string ParameterType
        {
            get
            {
                // value type
                if (_dataType == DataGroup.ValueType)
                {
                    return string.Format("System.Nullable<{0}>", Name);
                }
                // udtt
                else if (_dataType == DataGroup.Udtt)
                {
                    return Name;
                }
                // reference type
                else
                {
                    // convert System.Object to QueryTalk.Value
                    if (Name == "System.Object")
                    {
                        return "QueryTalk.Value";
                    }
                    else
                    {
                        return Name; // all reference types + user-defined TableType
                    }
                }
            }
        }

        // return CLR type is a type of the return value of a scalar function
        // Note that the scalar function cannot return QueryTalk.Value type.
        internal string ReturnType
        {
            get
            {
                // value type
                if (_dataType == DataGroup.ValueType)
                {
                    return string.Format("System.Nullable<{0}>", Name);
                }
                // reference type + udtt
                else
                {
                    return Name;
                }
            }
        }

        // parameter type used in procedures
        // Exception:
        //    1) OUTPUT parameters are handled through the QueryTalk.Value object.
        //    2) UserDefined table-valued types are handled through QueryTalk.View object.
        internal string ProcType(ParameterMode paramMode)
        {
            // output param
            if (paramMode == ParameterMode.InOut)
            {
                return "QueryTalk.Value";
            }
            // all other types
            else
            {
                return ParameterType;
            }
        }

        // non CLR compliant type
        internal TypeInfo()
        { }

        // CLR compliant type
        internal TypeInfo(DT dt, string clrName, TypeArgs typeArgs, DataGroup dataType, bool isSpecial)
        {
            IsCompliant = true;
            DT = dt;
            Name = clrName;
            TypeArgs = typeArgs;
            _dataType = dataType;
            IsSpecial = isSpecial;
        }

        // UDTT type
        internal TypeInfo(string udttSchema, string udttName)
        {
            IsCompliant = true;
            DT = DT.Udtt;
            UdttSchema = udttSchema;
            UdttName = udttName;
            _dataType = DataGroup.Udtt;
            Name = "QueryTalk.View";
        }

        // build Dbt() ctor method (exception safe)
        internal string BuildDbt(int length, int precision, int scale)
        {
            if (IsCompliant)
            {
                // user defined type
                if (DT == Wall.DT.Udtt)
                {
                    return String.Format("new QueryTalk.Wall.DataType(QueryTalk.Wall.DT.{0}, QueryTalk.Designer.Identifier(\"{1}\", \"{2}\"))", DT, 
                        UdttSchema.EscapeDoubleQuote(), UdttName.EscapeDoubleQuote());
                }

                // single argument
                if (TypeArgs == TypeArgs.Single)
                {
                    return String.Format("new QueryTalk.Wall.DataType(QueryTalk.Wall.DT.{0}, {1})", DT, length);
                }

                // two arguments
                if (TypeArgs == TypeArgs.Two)
                {
                    return String.Format("new QueryTalk.Wall.DataType(QueryTalk.Wall.DT.{0}, {1}, {2})", DT, precision, scale);
                }

                // none
                return String.Format("new QueryTalk.Wall.DataType(QueryTalk.Wall.DT.{0})", DT);
            }
            else
            {
                return null;
            }
        }
    }
}
