/*
*   ISEL-ADEETC-SI2
*   ND 2014-2018
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace HelloWorld
{
    class Program
    {
        /**
        *    Ilustrativo da utilização do
        */
        static void Main(string[] args)
        {
            //Executar o código presente em createTable.sql para criar o modelo de dados
           using(SqlConnection con = new SqlConnection())
           {
                try
                {
                    con.ConnectionString= @"Data Source=.;Initial Catalog=SI2_VLAB;Integrated Security=True";
                    using(SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "select value from DEMO";
                        con.Open();

                        using(SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                                Console.Write(dr["value"]+"\n");
                        }
                    }
                    
                }
                catch (DbException ex)
                { 
                    Console.WriteLine("E R R O : "+ex.Message);
                }
                
           }            
        }
    }
}
