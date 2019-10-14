using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ConsoleDemo
{
    class Program
    {
        static void Main()
        {
            string cs = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;

            using (SqlConnection sourceConn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from Products_Source", sourceConn);
                sourceConn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    using (SqlConnection destinationConn = new SqlConnection(cs))
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destinationConn))
                        {
                            bc.BatchSize = 10000;
                            bc.NotifyAfter = 5000;// raise SqlRowsCopied Event after 5000 records
                            bc.SqlRowsCopied += new SqlRowsCopiedEventHandler(bc_SqlRowsCopied);
                            bc.DestinationTableName = "Products_Destination";
                            destinationConn.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }
            }
        }

        static void bc_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            Console.WriteLine(e.RowsCopied + " loaded....");
        }
    }
}
