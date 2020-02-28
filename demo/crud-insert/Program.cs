using System;
using QueryTalk;
using s = QueryTalk.Db.QueryTalkBase;

namespace QueryTalkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            d.SetConnection(@"Data Source=UNICE\SQL2016;Initial Catalog=QueryTalkBase;Integrated Security=True;");

            // Create person extra data
            var person = new s.Data.PersonExtra()
            {
                PersonID = 2,
                Extra = "Insert test"
            };

            // Insert
            person.InsertGo();

            // Test it
            s.PersonExtra.Whose(s.PersonExtra.PersonID, 2)
                .Test();

            Console.ReadLine();
        }
    }
}
