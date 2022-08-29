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

using DAL.mapper.interfaces;
using DAL.model;
using System.Data;
using System.Data.SqlClient;

namespace DAL.mapper.concrete
{
    class StudentMapper : IStudentMapper
    {
        private IContext context;
        public StudentMapper(IContext ctx)
        {
            context = ctx;
        }
        public Student Create(Student entity)
        {
            using (SqlCommand cmd = context.createCommand())
            {
                        
                cmd.CommandText = "insert into STUDENT(studentNumber,name,sex,dateBirth,country) values(@id,@name,@sex,@dateBirth,@country)";
                SqlParameter p1 = new SqlParameter("@id", entity.Number);
                SqlParameter p2 = new SqlParameter("@name", entity.Name);
                SqlParameter p3 = new SqlParameter("@sex", entity.Sex);
                SqlParameter p4 = new SqlParameter("@dateBirth", entity.DateOfBirth);
                SqlParameter p5 = new SqlParameter("@country", entity.HomeCountry == null ? -1 : entity.HomeCountry.Id);


                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);

                cmd.ExecuteNonQuery();
            }
            if (entity.EnrolledCourses != null)
            {
                using (SqlCommand cmd = context.createCommand())
                {
                    CourseMapper courseMap = new CourseMapper(context);
                    SqlParameter p = new SqlParameter("@courseId", SqlDbType.Int);
                    SqlParameter p1 = new SqlParameter("@studentId",entity.Number);
                    cmd.Parameters.Add(p);
                    cmd.Parameters.Add(p1);
                    foreach (var course in entity.EnrolledCourses)
                    {
                        Course c = courseMap.Create(course);
                        cmd.CommandText = "INSERT INTO StudentCourse (studentId,courseId) values(@studentId,@courseId)";                                
                        p.Value = c.Id;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
                    
              
            return entity;
        }
        public Student Read(int id)
        {
            Student s = null;
            using (SqlCommand cmd = context.createCommand())
            {
                cmd.CommandText = "select name,sex,dateBirth,country from Student where studentNumber=@id";
                SqlParameter p = new SqlParameter("@id", id);
                cmd.Parameters.Add(p);
                        
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        s = new Student();
                        s.Number = id;
                        s.Name = rd.GetString(0);
                        s.Sex = (rd.GetString(1).ToCharArray())[0];
                        s.DateOfBirth = rd.GetDateTime(2).ToLongDateString();

                        CountryMapper countryMap = new CountryMapper(context);
                        s.HomeCountry = countryMap.Read(rd.GetInt32(3));
                    }
                }
                //Uncommnt and see what happens
                /*
                cmd.CommandText = "select courseid from studentcourse where studentId=@id";
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    CourseMapper courseMap = new CourseMapper(connectionString);
                    while (rd.Read())
                    {
                        s.EnrolledCourses.Add(courseMap.Read(rd.GetInt32(0)));
                    }
                }
                */
                return s;
            }
        }
        public void Update(Student entity)
        {
            using (SqlCommand cmd = context.createCommand())
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
                    
                cmd.ExecuteNonQuery();
            }
            
        }
        public void Delete(Student entity)
        {
            using (SqlCommand cmd = context.createCommand())
            {
                cmd.CommandText = "delete from Student where studentId=@id";
                SqlParameter p1 = new SqlParameter("@id", entity.Number);
                cmd.Parameters.Add(p1);
                cmd.ExecuteNonQuery();
            }
            
        }

    }
}
