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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace ClearPoolEx
{
    public partial class Form1 : Form
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }
        private void connectAndExecute()
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = cs;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "select @@version";
                    con.Open();
                    string ver = (string)cmd.ExecuteScalar();
                    MessageBox.Show(ver);
                }
            }

        }
        private void btLigar_Click(object sender, EventArgs e)
        {
            try
            {
                connectAndExecute();                    
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message+ "error:"+ex.Number, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Boolean fatalNetworkException(int n)
        {
            return n == 232 || n == 233;
        }
        private void bt_clearPool_Click(object sender, EventArgs e)
        {
            try
            {
                connectAndExecute();
            }
            catch (SqlException ex)
            {
                if (!fatalNetworkException(ex.Number) ) throw ex;
                SqlConnection.ClearAllPools();
                connectAndExecute();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btInit_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = cs;
                con.Open();
                using (SqlConnection con1 = new SqlConnection())
                {
                    con1.ConnectionString = cs;
                    con1.Open();
                    using (SqlConnection con2 = new SqlConnection())
                    {
                        con2.ConnectionString = cs;
                        con2.Open();
                    }
                }
            }
        }
       
        
    }
}