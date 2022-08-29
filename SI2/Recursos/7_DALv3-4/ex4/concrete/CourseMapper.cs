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
using System.Transactions;

namespace DAL.mapper.concrete
{
    class CourseMapper : ICourseMapper
    {
        private IContext context;
        public CourseMapper(IContext ctx)
        { context = ctx; }
        public Course Create(Course entity)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                 using (SqlCommand cmd = context.createCommand())
                {
                    cmd.CommandText = "INSERT INTO Course (Name) output INSERTED.courseId VALUES(@Name)";
                    SqlParameter p = new SqlParameter("@Name", entity.Name);
                    cmd.Parameters.Add(p);
                    context.Open();
                    entity.Id = (int)cmd.ExecuteScalar();
                }
                if (entity.EnroledStudents != null)
                {
                    using (SqlCommand cmd = context.createCommand())
                    {


                        //StudentMapper studentMap = new StudentMapper(context);
                        SqlParameter p = new SqlParameter("@courseId", entity.Id);
                        SqlParameter p1 = new SqlParameter("@studentId", SqlDbType.Int);
                        cmd.Parameters.Add(p);
                        cmd.Parameters.Add(p1);
                        foreach (var student in entity.EnroledStudents)
                        {
                            //Student s = studentMap.Create(student);
                            cmd.CommandText = "INSERT INTO StudentCourse (studentId,courseId) values(@studentId,@courseId)";
                            //cmd.CommandText = "raiserror('Simulação de erro',15,1)"; //descomentar e ver o conteúdo da tabela Student
                            p1.Value = student.Number;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                ts.Complete();

            }
                    
             return entity;
           
        }
        public Course Read(int id)
        {
            Course c = null;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                 using (SqlCommand cmd = context.createCommand())
                {
                    cmd.CommandText = "select name from Course where courseId=@id";
                    SqlParameter p = new SqlParameter("@id", id);
                    cmd.Parameters.Add(p);
                    context.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            c = new Course();
                            c.Id = id;
                            c.Name = rd.GetString(0);
                        }
                    }
                    cmd.CommandText = "select studentid from studentcourse where courseId=@id";
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        StudentMapper studentMap = new StudentMapper(context);
                        while (rd.Read())
                        {
                            c.EnroledStudents.Add(studentMap.Read(rd.GetInt32(0)));
                        }
                    }



                }
                ts.Complete();
            }
            return c;
        }
        public void Update(Course entity)
        {
             using (SqlCommand cmd = context.createCommand())
            {
                cmd.CommandText = "update Course set name=@name where courseId=@id";
                SqlParameter p1 = new SqlParameter("@id", entity.Id);
                SqlParameter p2 = new SqlParameter("@name", entity.Name);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                context.Open();
                cmd.ExecuteNonQuery();
            }
           
        }
        public void Delete(Course entity)
        {

             using (SqlCommand cmd = context.createCommand())
            {
                cmd.CommandText = "delete from Course where courseId=@id";
                SqlParameter p1 = new SqlParameter("@id", entity.Id);
                cmd.Parameters.Add(p1);
                context.Open();
                cmd.ExecuteNonQuery();
            }
        }
        
    }
}
