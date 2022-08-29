using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DataAdapter
{
    class Program
    {
        static string getConnectionString() 
        {
            return ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
        }
        static void Main(string[] args)
        {
            try
            {
                VendorDataSet ds = new VendorDataSet();
                SqlDataAdapter adapterVendor,adapterPart;
                   
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = getConnectionString();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "select id,name from vendor";
                        adapterVendor = new SqlDataAdapter(cmd);
                        adapterVendor.Fill(ds.Vendor);

                        cmd.CommandText = "select id,descr,vendorId from part";
                        adapterPart = new SqlDataAdapter(cmd);
                        adapterPart.Fill(ds.Part);  
                    }
                }

                foreach (DataRow r in ds.Vendor.Rows)
                {
                    Console.WriteLine("{0}  | {1}", r[0], r[1]);
                    DataRow[] parts = r.GetChildRows(ds.VendorPartRelation);
                    
                    foreach (DataRow child in parts)
                        Console.WriteLine("\t{0} | {1}  |{2}", child[0], child[1], child[2]);
                }
                ds.WriteXml(@"C:\Users\siad\Downloads\VendorDS.XML", XmlWriteMode.WriteSchema);
                  
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.GetType(), ex.Message);
            }
            

        }
    }
}
