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

            // Query persons which presently work for an European company 
            // and live in the country different from their native country
            s.Person
                .Whose(s.PersonJob.IsCurrent, true)
                .Whose(s.Job.Company.Address.City.Country.CountryContinent.Continent.Name, "EUROPE")
                .Whose(s.Address.City.Country.CountryID.NotEqualTo(s.Person.CountryOfBirthID))
                .Test("Querying");

            Console.ReadLine();
        }
    }
}
