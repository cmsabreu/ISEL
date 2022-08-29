using System;


namespace TP2SI2.model
{
    class Scheduling
    {
        public virtual MaintenanceTeam TeamCode { get; set; }
        public virtual Intervention InterventionCode { get; set; }
        public DateTime SchedulingDate { get; set; }

    }
}
