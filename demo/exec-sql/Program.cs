using System;
using QueryTalk;

namespace QueryTalkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            d.SetConnection(@"Data Source=UNICE\SQL2016;Initial Catalog=QueryTalkBase;Integrated Security=True;");

            // exec raw sql
            var result = d.ExecGo(
                "EXEC dbo.SelectCountry @CountryID, @Output".Pass(10, null));

            Console.ReadLine();
        }
    }
}
