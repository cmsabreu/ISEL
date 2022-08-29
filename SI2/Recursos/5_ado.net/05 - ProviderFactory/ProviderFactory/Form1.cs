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
using System.Data.Common;


namespace ProviderFactory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }

        private void btExec_Click(object sender, EventArgs e)
        {
            try
            {
                string provider = (string)ConfigurationManager.AppSettings["provider"];
                DbProviderFactory fact = DbProviderFactories.GetFactory(provider);
                using (DbConnection con = fact.CreateConnection())
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                    using (DbCommand cmd = con.CreateCommand())
                    { 
                        cmd.CommandText = "select @@version";
                        con.Open();
                        string ver = (string)cmd.ExecuteScalar();
                        MessageBox.Show("provider:"+provider+'\n'+ver);
                    }
                }
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btFactList_Click(object sender, EventArgs e)
        {
            frmProviderFactories pf = new frmProviderFactories();
            pf.ShowDialog();
        }
    }
}