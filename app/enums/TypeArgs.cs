namespace QueryTalk.Mapper
{
    // "families" of the SQL data type depending on how many arguments are needed to declare a type
    internal enum TypeArgs
    {
        None = 0,       // e.g. int
        Single = 1,     // e.g. nvarchar(), datetime2()
        Two = 2         // e.g. decimal(,)
    }
}
