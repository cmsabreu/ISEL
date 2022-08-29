using TP2SI2.model;
using TP2SI2.dal;

namespace TP2SI2.concrete
{
    class InterventionProxy : Intervention
    {
        private IContext context;
        private int? assetId;
        public InterventionProxy(Intervention intervention, IContext ctx, int? assetId) : base()
        {
            base.InterventionCode = intervention.InterventionCode;
            base.Description = intervention.Description;
            base.State = intervention.State;
            base.Price = intervention.Price;
            base.StartDate = intervention.StartDate;
            base.EndDate = intervention.EndDate;
            context = ctx;
            this.assetId = assetId;
        }
        public override Asset AssetId
        {
            get
            {
                if (base.AssetId == null) //lazy load
                {
                    InterventionMapper interventionMapper = new InterventionMapper(context);
                    base.AssetId = interventionMapper.LoadAsset(this);
                }
                return base.AssetId;
            }
            set
            {
                base.AssetId = value;
            }
        }
    }
}
