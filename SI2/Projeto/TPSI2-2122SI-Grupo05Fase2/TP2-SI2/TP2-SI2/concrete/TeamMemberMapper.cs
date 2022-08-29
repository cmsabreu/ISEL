using System;
using TP2SI2.model;
using TP2SI2.mapper.interfaces;
using TP2SI2.mapper;
using TP2SI2.dal;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TP2SI2.concrete
{
    class TeamMemberMapper : AbstractMapper<TeamMember, KeyValuePair<int, int>, List<TeamMember>>, ITeamMemberMapper
    {
        public TeamMemberMapper(IContext ctx) : base(ctx) { }
        protected override string SelectAllCommandText
        {
            get
            {
                return "select id, team_code from team_member";
            }
        }

        protected override string SelectCommandText {
            get
            {
                return SelectAllCommandText;
            }
            
        }

        protected override string UpdateCommandText
        {
            get
            {
                throw new NotImplementedException();
            }

        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from team_member where id=@id AND team_code=@team_code";
            }

        }


        protected override string InsertCommandText
        {
            get
            {
                return "insert into team_member(id, team_code) values(@id, @team_code);";
            }
        }

        protected override void DeleteParameters(IDbCommand command, TeamMember e)
        {
            SqlParameter p1 = new SqlParameter("@id", e.Id);
            SqlParameter p2 = new SqlParameter("@team_code", e.TeamCode);
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
        }

        protected override void InsertParameters(IDbCommand command, TeamMember e)
        {
            UpdateParameters(command, e);
        }

        protected override TeamMember Map(IDataRecord record)
        {
            throw new NotImplementedException();
        }

        protected override void SelectParameters(IDbCommand command, KeyValuePair<int, int> keyValuePair)
        {
            SqlParameter p1 = new SqlParameter("@id", keyValuePair.Key);
            SqlParameter p2 = new SqlParameter("@team_code", keyValuePair.Value);

            command.Parameters.Clear();
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);

        }

        protected override TeamMember UpdateEntityID(IDbCommand cmd, TeamMember e)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateParameters(IDbCommand command, TeamMember e)
        {
            SqlParameter p1 = new SqlParameter("@id", e.Id);
            SqlParameter p2 = new SqlParameter("@team_code", e.TeamCode);

            p1.Direction = ParameterDirection.InputOutput;

            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
        }

        internal void UpdateTeam(int team_code, int delete, List<int> ssn_list)
        {
            using (IDbCommand cmd = context.createCommand())
            {
                cmd.CommandText = "dbo.update_team_members";
                cmd.CommandType = CommandType.StoredProcedure;
                //problem with declare aux table
                SqlParameter p1 = new SqlParameter("@team_code", team_code);
                SqlParameter p2 = new SqlParameter("@toDelete", delete);
                SqlParameter p3 = new SqlParameter("@team_members_ssn", CreateDataTable(ssn_list));

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
        }

        private static DataTable CreateDataTable(IEnumerable<int> ssn_list)
        {
            DataTable table = new DataTable();
            table.Columns.Add("idx", typeof(int));
            table.Columns.Add("id", typeof(int));
            int i = 1;
            foreach (int ssn in ssn_list)
            {
                table.Rows.Add(i++, ssn);
            }
            return table;

        }

    }
}