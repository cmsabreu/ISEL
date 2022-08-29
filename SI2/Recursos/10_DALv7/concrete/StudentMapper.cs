﻿/*
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
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System;

namespace DAL.mapper.concrete
{

    class StudentMapper : AbstracMapper<Student, int?, List<Student>>, IStudentMapper
    {
        #region HELPER METHODS  
        internal List<Course> LoadCourses(Student s)
        {
            List<Course> lst = new List<Course>();

            CourseMapper cm = new CourseMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", s.Number));
            using (IDataReader rd = ExecuteReader("select courseid from studentcourse where studentId=@id", parameters))
            {
                while (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    lst.Add(cm.Read(key));
                }
            }
            return lst;
        }

        internal Country LoadCountry(Student s)
        {
            CountryMapper cm = new CountryMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", s.Number));
            using (IDataReader rd = ExecuteReader("select country from student where studentNumber=@id", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return cm.Read(key);
                }
            }
            return null;

        }

        #endregion
        public StudentMapper(IContext ctx) : base(ctx) { }

        public override Student Create(Student entity)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                EnsureContext();
                context.EnlistTransaction();

                using (IDbCommand cmd = context.createCommand())
                {
                    cmd.CommandText = InsertCommandText;
                    cmd.CommandType = InsertCommandType;
                    InsertParameters(cmd, entity);
                    cmd.ExecuteNonQuery();
                    entity = UpdateEntityID(cmd, entity);
                }
                if (entity != null && entity.EnrolledCourses != null && entity.EnrolledCourses.Count > 0)
                {
                    SqlParameter p = new SqlParameter("@courseId", SqlDbType.Int);
                    SqlParameter p1 = new SqlParameter("@studentId", entity.Number);

                    List<IDataParameter> parameters = new List<IDataParameter>();
                    parameters.Add(p);
                    parameters.Add(p1);
                    foreach (var course in entity.EnrolledCourses)
                    {
                        p.Value = course.Id;
                        ExecuteNonQuery("INSERT INTO StudentCourse (studentId,courseId) values(@studentId,@courseId)", parameters);
                    }

                }
                ts.Complete();
                return entity;
            }

        }

     

        public override Student Delete(Student entity)
        {
            if (entity == null)
                throw new ArgumentException("The " + typeof(Student) + " to delete cannot be null");

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                EnsureContext();
                context.EnlistTransaction();
                if (entity.EnrolledCourses != null && entity.EnrolledCourses.Count > 0)
                {
                    SqlParameter p = new SqlParameter("@studentId", entity.Number);

                    List<IDataParameter> parameters = new List<IDataParameter>();
                    parameters.Add(p);
                    ExecuteNonQuery("delete from StudentCourse where studentId=@studentId", parameters);
                }

                Student del = base.Delete(entity);
                ts.Complete();
                return del;
            }
        }
        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Student where studentNumber=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "insert into STUDENT(studentNumber,name,sex,dateBirth,country) values(@id,@name,@sex,@dateBirth,@country); select @id=studentNumber from STUDENT;";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select studentNumber,name,sex,dateBirth,country from Student";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where studentNumber=@id", SelectAllCommandText);
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Student set name=@name, sex=@sex, dateBirth=@dateBirth where studentNumber=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Student entity)
        {
            SqlParameter p1 = new SqlParameter("@id", entity.Number);
            cmd.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand cmd, Student entity)
        {
            UpdateParameters(cmd, entity);
        }

        protected override Student Map(IDataRecord record)
        {
            Student s = new Student();
            s.Number = record.GetInt32(0);
            s.Name = record.GetString(1);
            s.Sex = (record.GetString(2).ToCharArray())[0];
            s.DateOfBirth = record.GetDateTime(3).ToLongDateString();
            
            return new StudentProxy(s, context, record.GetInt32(4));
        }
        
        protected override void SelectParameters(IDbCommand cmd, int? id)
        {
            SqlParameter p = new SqlParameter("@id", id);
            cmd.Parameters.Add(p);
        }

        protected override Student UpdateEntityID(IDbCommand cmd, Student e)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            e.Number = int.Parse(param.Value.ToString());
            return e;
        }

        protected override void UpdateParameters(IDbCommand cmd, Student entity)
        {
            SqlParameter p1 = new SqlParameter("@id", entity.Number);
            SqlParameter p2 = new SqlParameter("@name", entity.Name);
            SqlParameter p3 = new SqlParameter("@sex", entity.Sex);
            SqlParameter p4 = new SqlParameter("@dateBirth", entity.DateOfBirth);
            SqlParameter p5 = new SqlParameter("@country", entity.HomeCountry == null ? null : entity.HomeCountry.Id);
            p1.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
        }
    }
}
