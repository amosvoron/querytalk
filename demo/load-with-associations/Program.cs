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

            // Load person #1 with country, continent, address, and city data
            var result = s.Person.At(1)
                .With(s.Country
                    .With(s.CountryContinent
                        .With(s.Continent)))
                .With(s.Address
                    .With(s.City))
                .Go();

            Console.ReadLine();
        }
    }
}
