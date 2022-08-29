
namespace TP2SI2.model
{
    class MaintenanceTeam
    {
        public int TeamCode { get; set; }
        public string Location { get; set; }
        public int NElements { get; set; }
        public virtual Employee Supervisor { get; set; }
    }
}
