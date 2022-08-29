using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataAdapter
{
    
    class VendorDataSet : DataSet
    {
        PartDataTable part;
        VendorDataTable vendor;
        public VendorDataSet():  base("VendorDs")
        {
           
            part = new PartDataTable();
            vendor = new VendorDataTable();
            this.Tables.Add(vendor);
            this.Tables.Add(part);
            this.Relations.Add("fk_vendor_part", vendor.ID, part.VendorId, true);
        }
        public DataTable Part
        {
            get{return part;}
        }
        public DataTable Vendor
        {
            get { return vendor; }
        }
        public DataRelation VendorPartRelation
        {
            get { return this.Relations["fk_vendor_part"]; }
        }
    }
}
