using TP2SI2.dal;
using TP2SI2.model;
using System.Data;
using System.Collections.Generic;

namespace TP2SI2.concrete
{
    class MaintenanceTeamProxy : MaintenanceTeam
    {
        private IContext context;
        private int? supervisorId;


        public MaintenanceTeamProxy(MaintenanceTeam maintenance, IContext ctx, int? supervisorId) :base()
        {
            base.TeamCode = maintenance.TeamCode;
            base.Location = maintenance.Location;
            base.NElements = maintenance.NElements;
            base.Supervisor = null;
            context = ctx;
            this.supervisorId = supervisorId;
        }

        public override Employee Supervisor
        {
            get
            {
                if (base.Supervisor == null)
                {
                    MaintenanceTeamMapper mt = new MaintenanceTeamMapper(context);
                    base.Supervisor = mt.LoadEmployee(this);
                }
                return base.Supervisor;
            }
        }
    }
}

