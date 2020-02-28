using System;
using System.Text.RegularExpressions;

namespace QueryTalk.Mapper
{
    internal class AccountEventArgs : EventArgs
    {
        internal Guid ConnectionToken { get; private set; }

        internal string ServerResponse { get; private set; }

        internal bool IsDenied
        {
            get
            {
                return Regex.IsMatch(ServerResponse ?? "", "^Denied");
            }
        }

        public AccountEventArgs()
        { }

        public AccountEventArgs(Guid connectionToken, string serverResponse)
        {
            ConnectionToken = connectionToken;
            ServerResponse = serverResponse;
        }

    }
}
