using TP2SI2.model;
using TP2SI2.dal;

namespace TP2SI2.concrete
{
    class AssetProxy : Asset
    {
        private IContext context;
        private int? assetId;
        private int? managerId;
        private int? typeId;
        public AssetProxy(Asset asset, IContext ctx, int? assetId, int? managerId, int? typeId) : base()
        {
            base.Id = asset.Id;
            base.AssetName = asset.AssetName;
            base.AcquisitionDate = asset.AcquisitionDate;
            base.State = asset.State;
            base.Model = asset.Model;
            base.Location = asset.Location;
            context = ctx;
            this.assetId = assetId;
            this.managerId = managerId;
            this.typeId = typeId;
        }
        public override Asset AssetReference
        {
            get
            {
                if (base.AssetReference == null) //lazy load
                {
                    AssetMapper assetMapper = new AssetMapper(context);
                    base.AssetReference = assetMapper.LoadAsset(this);
                }
                return base.AssetReference;
            }
            set
            {
                base.AssetReference = value;
            }
        }
        public override Employee Manager
        {
            get
            {
                if (base.Manager == null) //lazy load
                {
                    AssetMapper assetMapper = new AssetMapper(context);
                    base.Manager = assetMapper.LoadEmployee(this);

                }
                return base.Manager;
            }
            set
            {
                base.Manager = value;
            }
        }
        public override model.Type Type
        {
            get
            {
                if (base.Type == null) //lazy load
                {
                    AssetMapper assetMapper = new AssetMapper(context);
                    base.Type = assetMapper.LoadType(this);

                }
                return base.Type;
            }
            set
            {
                base.Type = value;
            }
        }
    }
}

