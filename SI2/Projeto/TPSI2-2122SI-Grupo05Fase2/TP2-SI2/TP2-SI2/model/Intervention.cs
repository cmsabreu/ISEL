using System;

namespace TP2SI2.model
{
    class Intervention
    {
        public int InterventionCode { get; set; } //NOT NULL
        public string Description { get; set; }
        public string State { get; set; }
        public decimal Price { get; set; } //NOT NULL
        public DateTime StartDate { get; set; } //NOT NULL
        public DateTime? EndDate { get; set; } //pode ser NULL
        public virtual Asset AssetId { get; set; }
    }
}
