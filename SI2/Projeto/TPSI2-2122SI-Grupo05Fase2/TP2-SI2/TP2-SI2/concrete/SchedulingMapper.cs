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
    class SchedulingMapper : AbstractMapper<Scheduling, KeyValuePair<int, int>, List<Scheduling>>, ISchedulingMapper
    {

        public SchedulingMapper(IContext ctx) : base(ctx)
        {
        }


        protected override string SelectAllCommandText
        {
            get
            {
                return "select team_code, intervention_code, scheduling_date from scheduling";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where team_code=@team_code AND intervention_code = @intervention_code"
                    , SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update scheduling set scheduling_date=@scheduling_date where " +
                    "team_code=@team_code AND intervention_code = @intervention_code";
            }
        } 

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from scheduling where team_code = @team_code AND intervention_code = @intervention_code";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO scheduling (team_code, intervention_code, scheduling_date) " +
                    "VALUES(@team_code, @intervention_code,@scheduling_date);";
            }
        }


        protected override void DeleteParameters(IDbCommand command, Scheduling s)
        {
            SqlParameter p1 = new SqlParameter("@team_code", s.TeamCode);
            SqlParameter p2 = new SqlParameter("@intervention_code", s.InterventionCode);

            command.Parameters.Add(p1);
            command.Parameters.Add(p2);

        }

        protected override void InsertParameters(IDbCommand command, Scheduling s)
        {
            SqlParameter p1 = new SqlParameter("@team_code", s.TeamCode.TeamCode);
            SqlParameter p2 = new SqlParameter("@intervention_code", s.InterventionCode.InterventionCode);
            SqlParameter p3 = new SqlParameter("@scheduling_date", s.SchedulingDate);
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
            command.Parameters.Add(p3);

        }
        protected override Scheduling Map(IDataRecord record)
        {
            throw new NotImplementedException();
        }


        protected override void SelectParameters(IDbCommand command, KeyValuePair<int, int> keyValuePair)
        {
            SqlParameter p1 = new SqlParameter("@team_code", keyValuePair.Key);
            SqlParameter p2 = new SqlParameter("@intervention_code", keyValuePair.Value);

            command.Parameters.Clear();
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);

        }

        public override Scheduling Create(Scheduling entity)
        {
            EnsureContext();
            using (IDbCommand cmd = context.createCommand())
            {
                cmd.CommandText = InsertCommandText;
                cmd.CommandType = InsertCommandType;
                InsertParameters(cmd, entity);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return entity;
            }
        }

        protected override void UpdateParameters(IDbCommand command, Scheduling s)
        {
            InsertParameters(command, s);
        }

        protected override Scheduling UpdateEntityID(IDbCommand cmd, Scheduling e)
        {
            return e;
        }
    }
}


