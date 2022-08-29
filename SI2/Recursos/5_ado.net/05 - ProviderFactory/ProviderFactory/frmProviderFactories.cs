using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.ProviderBase;
using System.Data.Common;


namespace ProviderFactory
{
   public partial class frmProviderFactories : Form
   {
      public frmProviderFactories()
      {
         InitializeComponent();
      }

      DataTable providersList = null;
      private void button1_Click(object sender, EventArgs e)
      {
          providersList = DbProviderFactories.GetFactoryClasses();
         dataGridView1.DataSource = providersList;
      }

private void dataGridView1_RowHeaderMouseDoubleClick(
   object sender, DataGridViewCellMouseEventArgs e)
{
   DataRow providerRow = providersList.DefaultView[e.RowIndex].Row;
   DbProviderFactory factory = DbProviderFactories.GetFactory(providerRow);
   //get SQL Server instances
   DataTable sources =
      factory.CreateDataSourceEnumerator().GetDataSources();
   frmSources f = new frmSources();
   f.DataSources = sources;
   if (f.ShowDialog() != DialogResult.OK)
   {
      return;
   }
   //get selected dataRow
   DataRow selectedSource = f.SelectedSource;
}
   }
}