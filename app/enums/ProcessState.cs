namespace QueryTalk.Mapper
{
    // processing states
    internal enum ProcessState : int
    {
        Idle = 0,
        ProcessingTryConnection = 1,
        Processing = 2,
        Compiling = 3,
        Stopped = 4,
        Finished = 5,
        Failed = 6
    }
}
