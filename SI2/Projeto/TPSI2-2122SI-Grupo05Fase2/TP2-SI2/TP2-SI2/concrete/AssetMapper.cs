using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using TP2SI2.model;
using TP2SI2.mapper.interfaces;
using TP2SI2.mapper;
using TP2SI2.dal;

namespace TP2SI2.concrete
{
    class AssetMapper : AbstractMapper<Asset, int?, List<Asset>>, IAssetMapper
    {
        #region HELPER METHODS 
        internal Asset LoadAsset(Asset asset)
        {
            AssetMapper am = new AssetMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", asset.Id));
            using (IDataReader rd = ExecuteReader("select asset_reference from asset where id=@id", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return am.Read(key);
                }
            }
            return null;
        }

        internal Employee LoadEmployee(Asset asset)
        {
            EmployeeMapper em = new EmployeeMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", asset.Id));
            using (IDataReader rd = ExecuteReader("select manager from asset where id=@id", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return em.Read(key);
                }
            }
            return null;
        }

        internal model.Type LoadType(Asset asset)
        {
            TypeMapper tm = new TypeMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", asset.Id));
            using (IDataReader rd = ExecuteReader("select type from asset where id=@id", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return tm.Read(key);
                }
            }
            return null;
        }
        #endregion
        public AssetMapper(IContext ctx) : base(ctx) { }
        protected override string SelectAllCommandText
        {
            get
            {
                return "select id, asset_name, acquisition_date, state, brand, model," +
                    "location, asset_reference, manager, type from asset";
            }
        }
        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where id=@id", SelectAllCommandText);
            }
        }
        protected override string UpdateCommandText
        {
            get
            {
                return "update asset set asset_name=@asset_name, acquisition_date=@acquisition_date, state=@state," +
                    " brand=@brand, model=@model, location=@location, asset_reference=@asset_reference, manager=@manager," +
                    " type=@type where id=@id";
            }
        }
        protected override string DeleteCommandText
        {
            get
            {
                return "delete from asset where id=@id";
            }
        }
        protected override string InsertCommandText
        {
            get
            {
                return "insert into asset(asset_name, acquisition_date, state, brand, model, location, " +
                    "asset_reference, manager, type) values(@asset_name, @acquisition_date, @state, @brand, " +
                    "@model, @location, @asset_reference, @manager, @type);" +
                    "select @id=intervention_code from intervention;";
            }
        }
        protected override void DeleteParameters(IDbCommand command, Asset assset)
        {
            SqlParameter p1 = new SqlParameter("@id", assset.Id);
            command.Parameters.Add(p1);
        }
        protected override void InsertParameters(IDbCommand command, Asset asset)
        {
            UpdateParameters(command, asset);
        }
        protected override Asset Map(IDataRecord record)
        {
            Asset asset = new Asset();
            asset.Id = record.GetInt32(0);
            asset.AssetName = record.GetString(1);
            asset.AcquisitionDate = record.GetDateTime(2);
            asset.State = record.GetBoolean(3);
            asset.Brand = record.GetString(4);
            asset.Model = record.GetString(5);
            asset.Location = record.GetString(6);
            return new AssetProxy(asset, context, record.GetInt32(7), record.GetInt32(8), record.GetInt32(9));
        }
        protected override void SelectParameters(IDbCommand command, int? k)
        {
            SqlParameter p = new SqlParameter("@id", k);
            command.Parameters.Add(p);
        }
        protected override Asset UpdateEntityID(IDbCommand cmd, Asset asset)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            asset.Id = int.Parse(param.Value.ToString());
            return asset;
        }
        protected override void UpdateParameters(IDbCommand command, Asset asset)
        {
            SqlParameter p1 = new SqlParameter("@id", asset.Id);
            SqlParameter p2 = new SqlParameter("@assetName", asset.AssetName);
            SqlParameter p3 = new SqlParameter("@acquisitionDate", asset.AcquisitionDate);
            SqlParameter p4 = new SqlParameter("@state", asset.State);
            SqlParameter p5 = new SqlParameter("@brand", asset.Brand);
            SqlParameter p6 = new SqlParameter("@model", asset.Model);
            SqlParameter p7 = new SqlParameter("@location", asset.Location);
            SqlParameter p8 = new SqlParameter("@assetReference", asset.AssetReference == null ? null : asset.AssetReference.Id);
            SqlParameter p9 = new SqlParameter("@manager", asset.Manager == null ? null : asset.Manager.Ssn);
            SqlParameter p10 = new SqlParameter("@type", asset.Type == null ? null : asset.Type.Id);

            p1.Direction = ParameterDirection.InputOutput;

            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
            command.Parameters.Add(p3);
            command.Parameters.Add(p4);
            command.Parameters.Add(p5);
            command.Parameters.Add(p6);
            command.Parameters.Add(p7);
            command.Parameters.Add(p8);
            command.Parameters.Add(p9);
            command.Parameters.Add(p10);
        }
    }
}
