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
    class TypeMapper : AbstractMapper<model.Type, int?, List<model.Type>>, ITypeMapper
    {

        public TypeMapper(IContext ctx) : base(ctx)
        {
        }
        protected override string SelectAllCommandText
        {
            get
            {
                return "select id, description from type";
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
                return "update Type set description=@description where id=@id";
            }
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Type where id=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Type (description) VALUES(@description); select @id=scope_identity()";
            }
        }


        protected override void DeleteParameters(IDbCommand command, model.Type t)
        {
            SqlParameter p1 = new SqlParameter("@id", t.Id);
            command.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand command, model.Type t)
        {
            SqlParameter p = new SqlParameter("@description", t.Description);
            SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
            p1.Direction = ParameterDirection.InputOutput;

            if (t.Id != null)
                p1.Value = t.Id;
            else
                p1.Value = DBNull.Value;

            command.Parameters.Add(p);
            command.Parameters.Add(p1);
        }

        protected override model.Type Map(IDataRecord record)
        {
            model.Type t = new model.Type();
            t.Id = record.GetInt32(0);
            t.Description = record.GetString(1);
            return t;
        }

        protected override void SelectParameters(IDbCommand command, int? k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            command.Parameters.Clear();
            command.Parameters.Add(p1);
        }

        protected override model.Type UpdateEntityID(IDbCommand cmd, model.Type t)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            t.Id = int.Parse(param.Value.ToString());
            return t;
        }

        protected override void UpdateParameters(IDbCommand command, model.Type t)
        {
            InsertParameters(command, t);
        }
    }
}
