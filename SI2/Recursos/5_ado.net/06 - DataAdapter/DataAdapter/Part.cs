using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataAdapter
{
    class PartDataTable: DataTable
    {
        public PartDataTable(): base("Part")
        {
            //definição do esquema
            this.Columns.Add("ID", typeof(Int32));
            this.Columns.Add("vendorId", typeof(Int32));           
            this.Columns.Add("descr", typeof(String));
            this.PrimaryKey = new DataColumn[] { this.Columns["ID"], this.Columns["vendorId"] };
        }
        public DataColumn ID
        {
            get { return this.Columns["Id"]; }
        }
        public DataColumn Descricao
        {
            get { return this.Columns["descr"]; }
        }
        public DataColumn VendorId
        {
            get { return this.Columns["vendorId"]; }
        }
    }
}
