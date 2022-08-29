/*
*  ISEL-ADEETC-SI2
*   ND 2014-2020
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*	Os exemplos podem não ser completos e/ou totalmente correctos
*	sendo desenvolvido com objectivos pedagógicos
*	Eventuais incorrecções são alvo de discussão
*	nas aulas.
*/

using System.Data.SqlClient;

namespace DAL.concrete
{
    class Context: IContext
    {
        private string connectionString;
        private SqlConnection con = null;
        private SqlTransaction tran = null;
        private bool? complete = null;

        public Context(string cs)
        {
            connectionString = cs;
        }

        public void Open()
        {
            if (con == null)
            {
                con = new SqlConnection(connectionString);
                con.Open();
                tran = con.BeginTransaction();
            }
        }
        public void Complete()
        {
            if (complete == null)
                complete = true;
        }
        public void Abort()
        {
            complete = false;
        }
        public SqlCommand createCommand()
        {
            Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.Transaction = tran;
            return cmd;
        }

        public void Dispose()
        {
            if (con != null)
            {
                if (tran != null)
                {
                    if (complete == null || complete == false)
                        tran.Rollback();
                    else
                        tran.Commit();
                }
                tran = null;
                con.Close();
                con = null;
            }
        }
        
    }
}
