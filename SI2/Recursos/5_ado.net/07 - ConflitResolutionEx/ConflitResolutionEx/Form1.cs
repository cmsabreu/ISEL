using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConflitResolutionEx
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tblCustomersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();            
            this.tblCustomersBindingSource.EndEdit();
            this.tblCustomersTableAdapter.Update(this.customerDataSet.TblCustomers);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'customerDataSet.TblCustomers' table. You can move, or remove it, as needed.
            customerDataSet.CreateDefaultGUIDS();
            this.tblCustomersTableAdapter.ContinueUpdateOnError = true;
            this.tblCustomersTableAdapter.Fill(this.customerDataSet.TblCustomers);

        }

        private void syncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tblCustomersTableAdapter.Update(customerDataSet.TblCustomers);
            if (customerDataSet.HasErrors)
            { 
                status.Text = " Houve erros. Dados parcialmente salvos...";
            }
            else
            {
                this.tblCustomersTableAdapter.Fill(customerDataSet.TblCustomers);
                status.Text = " DB Sync.";
            }
        }

        private void resolveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (customerDataSet.HasErrors)
            {
                //criar um novo dataset com os dados mais recentes
                CustomerDataSet refreshCostumer = new CustomerDataSet();
                this.tblCustomersTableAdapter.Fill(refreshCostumer.TblCustomers);

                //iterar sobre os erros
                foreach (DataRow dr in customerDataSet.TblCustomers.GetErrors())
                {
                    DataRow currentDb= refreshCostumer.TblCustomers.Rows.Find(dr["Id"]);
                    using(frmConflit conflit = new frmConflit(dr,currentDb) )
                    {
                        if( conflit.ShowDialog(this) == DialogResult.OK )
                        {
                            dr.ClearErrors();
                            tblCustomersTableAdapter.Update(conflit.FinalDbDataRow);
                            customerDataSet.TblCustomers.LoadDataRow(conflit.FinalDbDataRow.ItemArray,LoadOption.OverwriteChanges);
                            status.Text = "tuplo actualizado";
                        }
                        else
                            status.Text = "actualização do tuplo cancelada...";
                    }
                    tblCustomersDataGridView.Refresh();
                }
            }

        }
    }
}