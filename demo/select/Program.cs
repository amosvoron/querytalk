using System;
using QueryTalk;

namespace QueryTalkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            d.SetConnection(@"Data Source=UNICE\SQL2016;Initial Catalog=QueryTalkBase;Integrated Security=True;");

            var result = d.From("dbo.Person").Select()   
                .Go();
        }
    }
}
