namespace QueryTalk.Mapper
{
    internal enum TaskStatus
    {
        None,         // there is no task (task == null)
        Running,      // task is running
        //Denied,     // task has completed successfully, server responded with Denied
        //Completed   // task is completed successfully, server responded Allowed
    }

}
