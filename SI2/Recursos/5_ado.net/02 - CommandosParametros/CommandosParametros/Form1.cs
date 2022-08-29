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

namespace CommandosPreparados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int exec() 
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    SqlParameter nome = cmd.CreateParameter();
                    cmd.CommandText = String.Format("insert into Cliente(nome,apelido,idade) values('{0}','{1}',{2})",
                                                txtNome.Text, txtApelido.Text, txtIdade.Text);
                    MessageBox.Show(cmd.CommandText);
                    con.Open();
                    //para mostrar SQL injections...
                    int results=1;
                    String res = "";
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {
                            res += dr.GetString(0) + '\n';
                            ++results;
                        }
                    }
                    MessageBox.Show(res);
                    return results;
                    


                }
            }
        }
        private int comParam() 
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                using (SqlCommand cmd = con.CreateCommand())
                {

                    SqlParameter nome = new SqlParameter("@nome", SqlDbType.VarChar,50);
                    SqlParameter apelido = new SqlParameter("@apelido", SqlDbType.VarChar,50);
                    SqlParameter idade = new SqlParameter("@idade", SqlDbType.TinyInt);
                    nome.Value = txtNome.Text;
                    apelido.Value = txtApelido.Text;
                    idade.Value = Convert.ToSByte(txtIdade.Text);

                    cmd.Parameters.Add(nome);
                    cmd.Parameters.Add(apelido);
                    cmd.Parameters.Add(idade);
                    
                    
                    cmd.CommandText = "insert into Cliente(nome,apelido,idade) values(@nome,@apelido,@idade)";
                    con.Open();
                    return cmd.ExecuteNonQuery();
                     
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int result = (Params.Checked ) ? comParam() :exec();
                
                MessageBox.Show(String.Format("{0} tuplo(s) afectado(s).", result));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }
        
    }
}