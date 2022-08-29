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
using System.Threading;
using System.Data.OleDb;

namespace ConnectionPooling
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitConnection();
        }

        private void InitConnection()
        {
            //para mostrar no perfmonitor
            string cs = ConfigurationManager.ConnectionStrings["CS_2_1"].ConnectionString;
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = cs;
                con.Open();                
            }
        }

        private void bt_no_pool_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings["CS_no_pool"];
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = cs.ConnectionString;
                con.Open();
                Thread.Sleep(3000);
            }
            ((Button)sender).Enabled = true;
        }

        

        private void bt_oleDB_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings["CS_oleDB"];
            using (OleDbConnection con = new OleDbConnection())
            {
                con.ConnectionString = cs.ConnectionString;
                con.Open();
                Thread.Sleep(3000);
            }
            ((Button)sender).Enabled = true;
        }

        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = (String)e.Argument;
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "waitfor delay '00:00:15' select @@version";
                    con.Open();
                    string ver = (string)cmd.ExecuteScalar();
                    MessageBox.Show("[ Con 1 ]\n" + ver);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = (String)e.Argument;
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "waitfor delay '00:00:09' select @@version";
                    con.Open();
                    string ver = (string)cmd.ExecuteScalar();
                    MessageBox.Show("[Con 2]\n" + ver);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = (String)e.Argument;
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "select @@version";
                    con.Open();
                    string ver= (string)cmd.ExecuteScalar();
                    MessageBox.Show("[ Con 3 ]\n"+ver);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bt_3_con_Click(object sender, EventArgs e)
        {
            string cs = (string)csList.Items[csList.SelectedIndex];
            backgroundWorker1.RunWorkerAsync(cs);
            backgroundWorker2.RunWorkerAsync(cs);            
            backgroundWorker3.RunWorkerAsync(cs);            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            csList.Items.Add(ConfigurationManager.ConnectionStrings["CS_2_1"].ConnectionString);
            csList.Items.Add(ConfigurationManager.ConnectionStrings["CS_2_1_10"].ConnectionString);
            csList.Items.Add(ConfigurationManager.ConnectionStrings["CS_3_2_timeout"].ConnectionString);
            csList.SelectedIndex = 0;
        }
    }
}