using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataAdapter
{
    class VendorDataTable : DataTable
    {
        public VendorDataTable() : base("Vendor") 
        {
            //definição do esquema
            this.Columns.Add("ID", typeof(Int32));
            this.Columns.Add("name", typeof(String));
            this.PrimaryKey=new DataColumn[]{this.Columns["ID"]};
        }
        public DataColumn ID
        {
            get { return this.Columns["Id"]; }
        }
        public DataColumn Name
        {
            get { return this.Columns["Name"]; }
        }
    }
}
