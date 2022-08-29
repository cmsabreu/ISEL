/*
*  ISEL-ADEETC-SI2
*   ND 2014-2021
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


using DAL.mapper.interfaces;
using DAL.model;
using System.Data;
using System.Data.SqlClient;

namespace DAL.mapper.concrete
{
    class CourseMapper : ICourseMapper
    {
        private string connectionString;
        public CourseMapper(string cs)
        { connectionString = cs; }
        public void Create(Course entity)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                   cmd.CommandText = "INSERT INTO Course (Name) VALUES(@Name)";
                    SqlParameter p = new SqlParameter("@Name", entity.Name);
                    cmd.Parameters.Add(p);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }               
            }
        }
        public Course Read(int id)
        {
            Course c = null;
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "select name from Course where courseId=@id";
                    SqlParameter p = new SqlParameter("@id", id);
                    cmd.Parameters.Add(p);
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            c = new Course();
                            c.Id = id;
                            c.Name = rd.GetString(0);
                        }
                    }

                    //Colocar carregamento dos alunos inscritos
                }
            }
            return c;
        }
        public void Update(Course entity)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "update Course set name=@name where courseId=@id";
                    SqlParameter p1 = new SqlParameter("@id", entity.Id);
                    SqlParameter p2 = new SqlParameter("@name", entity.Name);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(Course entity)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "delete from Course where courseId=@id";
                    SqlParameter p1 = new SqlParameter("@id", entity.Id);
                    cmd.Parameters.Add(p1);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
