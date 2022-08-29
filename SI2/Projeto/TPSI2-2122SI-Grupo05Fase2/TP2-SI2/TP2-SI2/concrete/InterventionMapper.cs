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
    class InterventionMapper : AbstractMapper<Intervention, int?, List<Intervention>>, IInterventionMapper
    {

        #region HELPER METHODS 
        internal Asset LoadAsset(Intervention intervention)
        {
            AssetMapper cm = new AssetMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@intervention_code", intervention.InterventionCode));
            using (IDataReader rd = ExecuteReader("select asset_id from intervention where intervention_code=@intervention_code", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return cm.Read(key);
                }
            }
            return null;
        }

        //alinea 1.a
        //Chama a função "get_code_from_team" e retorna o codigo da equipa livre
        internal int? GetCodeFromTeam(string description)
        {
            using (SqlCommand cmd = context.createCommand())
            {
                cmd.CommandText = "SELECT team_code from dbo.get_code_from_team(@description)";
                cmd.CommandType = CommandType.Text;
                SqlParameter p = new SqlParameter("@description", Convert.ToString(description));
                cmd.Parameters.Add(p);
                var c = cmd.ExecuteScalar();

                int? team_code = c == DBNull.Value ? null : Convert.ToInt32(c);
                cmd.Parameters.Clear();
                return team_code;  
            }
        }
        //alinea 1.a.f
        //Cria uma intervenção executando o procedimento "p_criaInter"
        internal void CreateWithProcedure(Intervention intervention, int? frequency, string skill)
        {
            using (IDbCommand cmd = context.createCommand())
            {
                cmd.CommandText = "dbo.p_criaInter";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter p1 = new SqlParameter("@description", intervention.Description);
                SqlParameter p2 = new SqlParameter("@price", intervention.Price);
                SqlParameter p3 = new SqlParameter("@startDate", intervention.StartDate);
                SqlParameter p4 = new SqlParameter("@endDate", intervention.EndDate == null ? DBNull.Value : intervention.EndDate);
                SqlParameter p5 = new SqlParameter("@frequency", frequency == null ? DBNull.Value : frequency);
                SqlParameter p6 = new SqlParameter("@asset_id", intervention.AssetId.Id);
                SqlParameter p7 = new SqlParameter("@skillDescription", skill);

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
        }
        internal List<Intervention> ListInterventionYear(int year)
        {
            using (IDbCommand cmd = context.createCommand())
            {

                List<Intervention> list = new List<Intervention>();
                cmd.CommandText = "select * from dbo.interByYear(@year)";
                cmd.CommandType = CommandType.Text;

                SqlParameter p1 = new SqlParameter("@year", year);

                cmd.Parameters.Add(p1);


                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Intervention intervention = Map(reader);
                        list.Add(intervention);
                    }
                }

                return list;
            }

        }
        #endregion

        public InterventionMapper(IContext ctx) : base(ctx)
        {
        }
        protected override string SelectAllCommandText
        {
            get
            {
                return "select intervention_code, description, state, price, start_date, end_date, asset_id from intervention";
            }
        }
        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where intervention_code=@intervention_code", SelectAllCommandText);
            }
        }
        protected override string UpdateCommandText
        {
            get
            {
                return "update intervention set description=@description, state=@state, price=@price, " +
                    "start_date=@start_date, end_date=@end_date, asset_id=@asset_id where intervention_code=@intervention_code";
            }
        }
        protected override string DeleteCommandText
        {
            get
            {
                return "delete from intervention where intervention_code=@intervention_code";
            }
        }
        protected override string InsertCommandText
        {
            get
            {
                return "insert into intervention(description, state, price, start_date, end_date, asset_id) " +
                    "values(@description, @state, @price, @start_date, @end_date, @asset_id);" +
                    "select @intervention_code=intervention_code from intervention;";
            }
        }
        protected override void DeleteParameters(IDbCommand command, Intervention i)
        {
            SqlParameter p1 = new SqlParameter("@intervention_code", i.InterventionCode);
            command.Parameters.Add(p1);
        }
        protected override void InsertParameters(IDbCommand command, Intervention i)
        {
            UpdateParameters(command, i);
        }
        protected override Intervention Map(IDataRecord record)
        {
            Intervention i = new Intervention();
            i.InterventionCode = record.GetInt32(0);
            i.Description = record.GetString(1);
            i.State = record.GetString(2);
            i.Price = record.GetDecimal(3);
            i.StartDate = record.GetDateTime(4);
            i.EndDate = record.IsDBNull(5) ? null : record.GetDateTime(5);

            return new InterventionProxy(i, context, record.GetInt32(6));
        }
        protected override void SelectParameters(IDbCommand command, int? id)
        {
            SqlParameter p = new SqlParameter("@intervention_code", id);
            command.Parameters.Add(p);
        }
        protected override Intervention UpdateEntityID(IDbCommand cmd, Intervention i)
        {
            var param = cmd.Parameters["@intervention_code"] as SqlParameter;
            i.InterventionCode = int.Parse(param.Value.ToString());
            return i;
        }

        protected override void UpdateParameters(IDbCommand command, Intervention i)
        {

            SqlParameter p1 = new SqlParameter("@intervention_code", i.InterventionCode);
            SqlParameter p2 = new SqlParameter("@description", i.Description);
            SqlParameter p3 = new SqlParameter("@state", i.State == null ? "por atribuir" : i.State);
            SqlParameter p4 = new SqlParameter("@price", i.Price);
            SqlParameter p5 = new SqlParameter("@start_date", i.StartDate);
            SqlParameter p6 = new SqlParameter("@end_date", i.EndDate == null ? DBNull.Value : i.EndDate);
            SqlParameter p7 = new SqlParameter("@asset_id", i.AssetId == null ? DBNull.Value : i.AssetId.Id);
            p1.Direction = ParameterDirection.InputOutput;

            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
            command.Parameters.Add(p3);
            command.Parameters.Add(p4);
            command.Parameters.Add(p5);
            command.Parameters.Add(p6);
            command.Parameters.Add(p7);
        }

        //1.b e 1.c
        public Intervention Create(Intervention intervention, int? frequency, string description)
        {

            //A data de intervenção deve ser superior à data de aquisicao do ativo intervencionado
            if (DateTime.Compare(intervention.AssetId.AcquisitionDate, intervention.StartDate) >= 0)
            {
                throw new Exception("Invalid asset. Acquisition date of asset must be bigger than intervention date");
            }
            int? teamCode = GetCodeFromTeam(description);

            if (teamCode == null)
            {
                intervention.State = "por atribuir";
            }
            else
            {
                intervention.State = "em analise";

            }

            intervention = Create(intervention);
            if (teamCode != null)
            {
                MaintenanceTeamMapper mtMapper = new MaintenanceTeamMapper(context);

                Scheduling scheduling = new Scheduling();

                scheduling.TeamCode = mtMapper.Read(teamCode);
                scheduling.InterventionCode = intervention;
                scheduling.SchedulingDate = intervention.StartDate;

                SchedulingMapper schedulingMapper = new SchedulingMapper(context);

                schedulingMapper.Create(scheduling);


            }
            return intervention;
        }
    }
}
