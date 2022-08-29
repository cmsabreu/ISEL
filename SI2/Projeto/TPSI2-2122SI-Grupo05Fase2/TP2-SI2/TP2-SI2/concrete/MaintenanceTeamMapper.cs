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
    class MaintenanceTeamMapper : AbstractMapper<MaintenanceTeam, int?, List<MaintenanceTeam>>, IMaintenanceTeamMapper
    {

        #region HELPER METHODS  

        internal Employee LoadEmployee(MaintenanceTeam mt)
        {
            EmployeeMapper eMapper = new EmployeeMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@team_code", mt.TeamCode));
            using (IDataReader rd = ExecuteReader("select supervisor from MAINTENANCE_TEAM where team_code=@team_code", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return eMapper.Read(key);
                }
            }
            return null;

        }

        #endregion

        public MaintenanceTeamMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string SelectAllCommandText 
        {
            get 
            {
                return "select team_code, location, n_elements, supervisor from maintenance_team";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where team_code=@team_code", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Maintenance_Team set location=@location AND n_elements=@n_elements AND  supervisor = @supervisorwhere team_code=@team_code";
            }
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Maintenance_Team where team_code=@team_code";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Maintenance_Team (location, n_elements, supervisor) VALUES(@location, @n_elements, @supervisor); select @team_code=scope_identity()";
            }
        }

        internal void CreateTeam(String location, int ssn_supervisor, List<int> ssn_list)
        {
            using (IDbCommand cmd = context.createCommand())
            {
                cmd.CommandText = "dbo.p_create_team";
                cmd.CommandType = CommandType.StoredProcedure;


                SqlParameter p1 = new SqlParameter("@location", location);
                SqlParameter p2 = new SqlParameter("@ssn_supervisor", ssn_supervisor);

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
            table.Columns.Add("ssn_team_member", typeof(int));
            int i = 1;
            foreach (int ssn in ssn_list)
            {
                table.Rows.Add(i++, ssn);
            }
            return table;
        }
        protected override void DeleteParameters(IDbCommand command, MaintenanceTeam maintenanceTeam)
        {
            SqlParameter p1 = new SqlParameter("@team_code", maintenanceTeam.TeamCode);
            command.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand command, MaintenanceTeam maintenanceTeam)
        {
            SqlParameter p1 = new SqlParameter("@location", maintenanceTeam.Location);
            SqlParameter p2 = new SqlParameter("@n_elements", maintenanceTeam.NElements);
            SqlParameter p3 = new SqlParameter("@supervisor", maintenanceTeam.Supervisor);

            SqlParameter p = new SqlParameter("@team_code", SqlDbType.Int);
            p.Direction = ParameterDirection.InputOutput;

            if (maintenanceTeam.TeamCode != null)
                p.Value = maintenanceTeam.TeamCode;
            else
                p.Value = DBNull.Value;

            command.Parameters.Add(p);
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
            command.Parameters.Add(p3);

        }

        protected override MaintenanceTeam Map(IDataRecord record)
        {
            MaintenanceTeam maintenanceTeam = new MaintenanceTeam();
            maintenanceTeam.TeamCode = record.GetInt32(0);
            maintenanceTeam.Location = record.GetString(1);
            maintenanceTeam.NElements = record.GetInt32(2);
            

            return new MaintencanceTeamProxy(maintenanceTeam, context, record.GetInt32(3));
        }

        public MaintenanceTeam Map2(IDataRecord record)
        {
            return Map(record);
        }

        protected override void SelectParameters(IDbCommand command, int? k)
        {
            SqlParameter p1 = new SqlParameter("@team_code", k);
            command.Parameters.Clear();
            command.Parameters.Add(p1);
        }


        protected override MaintenanceTeam UpdateEntityID(IDbCommand cmd, MaintenanceTeam maintenanceTeam)
        {

            var param = cmd.Parameters["@team_code"] as SqlParameter;
            maintenanceTeam.TeamCode = int.Parse(param.Value.ToString());
            return maintenanceTeam;
        }


        protected override void UpdateParameters(IDbCommand command, MaintenanceTeam maintenanceTeam)
        {
            InsertParameters(command, maintenanceTeam);
        }

    }
}
