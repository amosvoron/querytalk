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

            // Select all persons (by mapper)
            var result = s.Person
                .Test()
                .Go();

            Console.ReadLine();
        }
    }
}
