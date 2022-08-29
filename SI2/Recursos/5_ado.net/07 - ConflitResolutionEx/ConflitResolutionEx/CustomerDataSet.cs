using System.Data;
using System;

namespace ConflitResolutionEx
{
    partial class CustomerDataSet
    {
        private bool createDefaultGUIDS = false;
        public void CreateDefaultGUIDS()
        {
            if (!createDefaultGUIDS)
            {
                createDefaultGUIDS = true;
                foreach (DataTable dt in this.Tables)
                {
                    if (dt.Columns["Id"] != null)
                        dt.TableNewRow += new DataTableNewRowEventHandler(TableNewRow);
                }
            }
        }

        private void TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            if (e.Row["Id"] is DBNull)
                e.Row["Id"] = Guid.NewGuid();
        }
    }
}

namespace ConflitResolutionEx.CustomerDataSetTableAdapters
{
    public partial class TblCustomersTableAdapter
    {
        public bool ContinueUpdateOnError
        {
            get { return Adapter.ContinueUpdateOnError; }
            set { Adapter.ContinueUpdateOnError = value; }
        }
    }
}