using System;
using QueryTalk;

namespace QueryTalkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                d.SetConnection(@"Data Source=UNICE\SQL2016;Initial Catalog=QueryTalkBase;Integrated Security=True;");

                var result = d.From("dbo.Person").Select()   
                    .Go();
            }
            catch (QueryTalkException ex)
            {
                Console.WriteLine(ex.Report ?? ex.Message);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(String.Format("{0}: {1}", ex.Number, ex.Message));
            }
            catch (Exception ex)
            {
                if (ex.InnerException is QueryTalkException)
                {
                    Console.WriteLine(((QueryTalkException)ex.InnerException).Report);
                }
                else
                {
                    Console.WriteLine(ex);
                }
            }

            Console.ReadLine();
        }

    }
}
