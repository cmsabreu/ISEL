using TP2SI2.dal;
using TP2SI2.model;

namespace TP2SI2.concrete
{
    internal class SchedulingProxy : Scheduling
    {
    
        private IContext context;
        private int team_code;
        private int intervention_code;

        public SchedulingProxy(Scheduling s, IContext context, int v1, int v2)
        {
            base.SchedulingDate = s.SchedulingDate;
            base.TeamCode = null;
            base.InterventionCode = null;
        
            this.context = context;
            this.team_code = v1;
            this.intervention_code = v2;
        }
        /*
         * TODO:
        public override MaintenanceTeam TeamCode
        {
            get
            {
                if (base.TeamCode == null) //lazy load
                {
                    SchedulingMapper mt = new SchedulingMapper(context);
                    base.TeamCode = mt.LoadTeam(this);
                }
                return base.TeamCode;
            }

            set
            {
                base.TeamCode = value;
            }
        }

        public override Intervention InterventionCode
        {
            get
            {
                if (base.InterventionCode == null) //lazy load
                {
                    SchedulingMapper mt = new SchedulingMapper(context);
                    base.TeamCode = mt.LoadIntervention(this);
                }
                return base.InterventionCode;
            }

            set
            {
                base.InterventionCode = value;
            }
        }
        */
    }
}