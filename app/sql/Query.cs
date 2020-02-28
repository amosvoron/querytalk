namespace QueryTalk.Mapper
{
    internal static class SQL
    {
        // FULL VERSION:
        private static Procedure _proc = d.AsNon("MappingQuery")
            .Inject(Properties.Resources.SQL)
            .EndProc();

        internal static Procedure Proc
        {
            get
            {
                return _proc;
            }
        }

        // LIMITED VERSION:
        private static Procedure _procLimited = d.AsNon("MappingQuery-LIMITED")
            .Inject(Properties.Resources.SQL_LIMITED)
            .EndProc();

        internal static Procedure ProcLimited
        {
            get
            {
                return _procLimited;
            }
        }

    }
}
