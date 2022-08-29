using TP2SI2.mapper.interfaces;
using TP2SI2.mapper;
using TP2SI2.dal;
using TP2SI2.model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;
namespace TP2SI2.concrete
{

    class SkillMapper : AbstractMapper<Skill, int?, List<Skill>>, ISkillMapper
    {

        public SkillMapper(IContext ctx) : base(ctx)
        {
        }


        protected override string SelectAllCommandText
        {
            get
            {
                return "select id, description from skill";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where id=@id", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Skill set description=@description where id=@id";
            }
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Skill where id=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Skill (description) VALUES(@description); select @id=scope_identity()";
            }
        }


        protected override void DeleteParameters(IDbCommand command, Skill s)
        {
            SqlParameter p1 = new SqlParameter("@id", s.Id);
            command.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand command, Skill s)
        {
            SqlParameter p = new SqlParameter("@description", s.Description);
            SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
            p1.Direction = ParameterDirection.InputOutput;

            if (s.Id != null)
                p1.Value = s.Id;
            else
                p1.Value = DBNull.Value;

            command.Parameters.Add(p);
            command.Parameters.Add(p1);
        }

        protected override Skill Map(IDataRecord record)
        {
            Skill s = new Skill();
            s.Id = record.GetInt32(0);
            s.Description = record.GetString(1);
            return s;
        }

        protected override void SelectParameters(IDbCommand command, int? k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            command.Parameters.Clear();
            command.Parameters.Add(p1);
        }

        protected override Skill UpdateEntityID(IDbCommand cmd, Skill s)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            s.Id = int.Parse(param.Value.ToString());
            return s;
        }

        protected override void UpdateParameters(IDbCommand command, Skill s)
        {
            InsertParameters(command, s);
        }
    }

}

