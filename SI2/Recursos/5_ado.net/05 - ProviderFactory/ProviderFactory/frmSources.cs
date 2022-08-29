using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProviderFactory
{
   public partial class frmSources : Form
   {
      public frmSources()
      {
         InitializeComponent();
      }

      public DataTable DataSources
      {
         get { return dataGridView1.DataSource as DataTable; }
         set { dataGridView1.DataSource = value; }
      }

      public DataRow SelectedSource
      {
         get
         {
            return selectedSource;
         }
         set { selectedSource = value; }
      }
      DataRow selectedSource;
      
      private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
      {
         SelectedSource = ((DataTable)dataGridView1.DataSource).DefaultView[dataGridView1.CurrentCell.RowIndex].Row;
         DialogResult = DialogResult.OK;
      }

   }
}