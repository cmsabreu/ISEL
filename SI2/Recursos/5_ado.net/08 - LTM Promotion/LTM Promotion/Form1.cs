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
using System.Configuration;
using System.Threading;
using System.Data.SqlClient;
using System.Transactions;

namespace ConnectionStringsEx
{
    public partial class Form1 : Form
    {
        private String connString;
        public Form1()
        {
            InitializeComponent();
            ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings["CS"];
            connString = cs.ConnectionString;
        }

        
        private void but1_Click(object sender, EventArgs ew)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        SqlCommand cmd = new SqlCommand("select @@version", con);
                        status.Text = "A abrir Ligação 1 ...";
                        con.Open();
                        cmd.ExecuteScalar();
                        status.Text = status.Text + "OK!";

                    }
                    Thread.Sleep(2000);


                    using (SqlConnection con = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Data Source=.; "))
                    {
                        SqlCommand cmd = new SqlCommand("select @@version", con);
                        status.Text = "A abrir Ligação 2...";
                        MessageBox.Show("DTC!");
                        con.Open();
                        MessageBox.Show("Now!");
                        cmd.ExecuteScalar();
                        status.Text = status.Text + "OK!";
                        Thread.Sleep(10000);

                    }
                    status.Text = "Recursos Libertados...";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ts.Complete();
            }
            
        }

        private void but2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    Thread.Sleep(10000);
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

       
    }
}