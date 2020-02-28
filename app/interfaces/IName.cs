namespace QueryTalk.Mapper
{
    internal interface IName
    {
        // SQL
        string SCHEMA { get; }
        string OBJECT_NAME { get; }

        // CLR
        string Name { get; set; }           // Person
        bool HasSchemaName { get; set; }    // dbo
        int RenameIndex { get; set; }       // ix of REN<ix>; ix > 0
        string NodeName { get; set; }       // Person or Person_dbo or Person_REN
                                            // Note: in case of the column it is the same as Name!!! 
    }
}
