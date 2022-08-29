using TP2SI2.dal;
using TP2SI2.model;

namespace TP2SI2.concrete
{
    class MaintencanceTeamProxy : MaintenanceTeam
    {
        private IContext context;
        private int? employeeId;

        public MaintencanceTeamProxy(MaintenanceTeam maintenance,IContext ctx, int? employeeId)
        {
            base.TeamCode = maintenance.TeamCode;
            base.Location = maintenance.Location;
            base.NElements = maintenance.NElements;
            
            
            base.Supervisor = null;
            context = ctx;
            this.employeeId = employeeId;
        }


        public override Employee Supervisor
        {
            get
            {
                if (base.Supervisor == null) //lazy load
                {
                    MaintenanceTeamMapper mt = new MaintenanceTeamMapper(context);
                    base.Supervisor = mt.LoadEmployee(this);
                }
                return base.Supervisor;
            }

            set
            {
                base.Supervisor = value;
            }
        }
    }
}