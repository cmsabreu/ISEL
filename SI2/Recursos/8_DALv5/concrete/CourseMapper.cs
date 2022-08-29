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
using System.Collections.Generic;
using System;

namespace DAL.mapper.concrete
{

    class CourseMapper : AbstracMapper<Course, int?, List<Course>>, ICourseMapper
    {
        public CourseMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Course where courseId=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Course (Name) VALUES(@Name); select @id = scope_identity()";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select courseId,name from Course";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0}  where courseId=@id", SelectAllCommandText);
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Course set name=@name where courseId=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Course e)
        {
            SelectParameters(cmd, e.Id);
        }

        protected override void InsertParameters(IDbCommand cmd, Course e)
        {
            SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
            SqlParameter p2 = new SqlParameter("@name", e.Name);

            p1.Direction = ParameterDirection.InputOutput;
            if (e.Id != null)
                p1.Value = e.Id;
            else
                p1.Value = DBNull.Value;

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

        }

        protected override Course Map(IDataRecord record)
        {
            Course c = new Course();
            c.Id = record.GetInt32(0);
            c.Name = record.GetString(1);
            return c;
        }

        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            cmd.Parameters.Add(p1);
        }

        protected override Course UpdateEntityID(IDbCommand cmd, Course e)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            e.Id = int.Parse(param.Value.ToString());
            return e;

        }

        protected override void UpdateParameters(IDbCommand command, Course e)
        {
            InsertParameters(command, e);
        }
    }
}