using System;

namespace TP2SI2.model
{
    public class Asset
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public bool State { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Location { get; set; }
        public virtual Asset AssetReference { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual Type Type { get; set; }
    }
}
