namespace QueryTalk.Mapper
{
    internal enum MessageType : int
    {
        None = 0,
        MissingObjectsInfo = 1,
        NoMappingDataInfo = 2,
        MappingConnectionError = 3,
        SqlException = 4,
        CompilerError = 5,
        UnknownError = 6,
        ApplicationCrashed = 7
    }
}
