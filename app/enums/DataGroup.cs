namespace QueryTalk.Mapper
{
    // data types of the columns and parameters
    internal enum DataGroup : int
    {
        ValueType = 0,
        ReferenceType = 1,

        // A user-defined TABLE type.
        // Note that user-defined SCALAR types are based on the existing SQL data types.
        // QueryTalk handles user-defined scalar types as SQL data types (as given through INFORMATION_SCHEMA). 
        Udtt = 2
    }
}
