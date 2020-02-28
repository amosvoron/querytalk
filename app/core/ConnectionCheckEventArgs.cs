using System;

namespace QueryTalk.Mapper
{
    internal class ConnectionCheckEventArgs : EventArgs
    {
        
        internal string Database { get; private set; }

        internal Exception Exception { get; private set; }

        internal bool Success
        {
            get
            {
                return Exception == null;
            }
        }

        internal bool Failed
        {
            get
            {
                return Exception != null;
            }
        }

        public ConnectionCheckEventArgs()
        { }

        public ConnectionCheckEventArgs(string database)
        {
            Database = database;
        }

        public ConnectionCheckEventArgs(Exception exception)
        {
            Exception = exception;
        }

        public ConnectionCheckEventArgs(string database, Exception exception)
        {
            Database = database;
            Exception = exception;
        }
    }
}
