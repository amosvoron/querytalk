using System;
using QueryTalk;

namespace QueryTalkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            d.SetConnection(@"Data Source=<your server>;Initial Catalog=QueryTalkBase;Integrated Security=True;");

            // Select all persons 
            var result = d.From("dbo.Person").Select()   
                .Go();
        }
    }
}
