/**  ISEL-ADEETC-SI2*   ND 2014-2017**   Material didático para apoio *   à unidade curricular de *   Sistemas de Informação II**   O código pode não ser completo.*/

using DAL.mapper.interfaces;
using DAL.model;
using System.Data;
using System.Data.SqlClient;

namespace DAL.mapper.concrete
{
    class StudentMapper : IStudentMapper
    {
        private string connectionString;
        public StudentMapper(string cs)
        {
            connectionString = cs;
        }
        public void Create(Student entity)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "insert into STUDENT(studentNumber,name,sex,dateBirth,country) values(@id,@name,@sex,@dateBirth,@country)";
                    SqlParameter p1 = new SqlParameter("@id", entity.Number);
                    SqlParameter p2 = new SqlParameter("@name", entity.Name);
                    SqlParameter p3 = new SqlParameter("@sex", entity.Sex);
                    SqlParameter p4 = new SqlParameter("@dateBirth", entity.DateOfBirth);
                    SqlParameter p5 = new SqlParameter("@country", entity.HomeCountry==null?-1:entity.HomeCountry.Id);


                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    cmd.Parameters.Add(p5);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
              }
                
           }
        public Student Read(int id)
        {
            Student s = null;
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "select name,sex,dateBirth,country from Student where studentNumber=@id";
                    SqlParameter p = new SqlParameter("@id", id);
                    cmd.Parameters.Add(p);
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            s = new Student();
                            s.Number = id;
                            s.Name = rd.GetString(0);
                            s.Sex = (rd.GetString(1).ToCharArray())[0];
                            s.DateOfBirth = rd.GetDateTime(2).ToLongDateString();

                            CountryMapper countryMap = new CountryMapper( connectionString);
                            s.HomeCountry = countryMap.Read(rd.GetInt32(3));
                        }
                    }
                }
                return s;
            }
        }
        public void Update(Student entity)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "update Student set name=@name, sex=@sex, dateBirth=@dateBirth where studentNumber=@id";
                    SqlParameter p1 = new SqlParameter("@id", entity.Number);
                    SqlParameter p2 = new SqlParameter("@name", entity.Name);
                    SqlParameter p3 = new SqlParameter("@sex", entity.Sex);
                    SqlParameter p4 = new SqlParameter("@dateBirth", entity.DateOfBirth);

                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(Student entity)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "delete from Student where studentId=@id";
                    SqlParameter p1 = new SqlParameter("@id", entity.Number);
                    cmd.Parameters.Add(p1);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
