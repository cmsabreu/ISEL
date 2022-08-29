using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace ConflitResolutionEx
{
    public partial class frmConflit : Form
    {
        private DataRow m_currentDataRow = null;
        private DataRow m_finalDbDataRow = null;
        public void PopulateTab(TabPage tab, DataRow dataRow, DataRowVersion dataRowVersion, bool readOnly)
        {
            const int verticalSpacing = 30;
            const int labelWidth = 50;
            const int horizontalSpacing = 10;
            const int buttonWidth = 100;
            const int buttonHeight = 20;
            for (int col = 0; col < dataRow.ItemArray.Length; col++)
            {
                object val = dataRow[col, dataRowVersion];
                Label label = new Label();
                tab.Controls.Add(label);
                label.Text = dataRow.Table.Columns[col].ColumnName;
                label.Top = (col + 1) * verticalSpacing;
                label.Left = horizontalSpacing;
                label.Width = labelWidth;
                label.Visible = true;
                TextBox textBox = new TextBox();
                tab.Controls.Add(textBox);
                textBox.Text = val.ToString();
                textBox.Top = (col + 1) * verticalSpacing;
                textBox.Left = (horizontalSpacing * 2) + labelWidth;
                textBox.Width = tab.Width - textBox.Left - buttonWidth - (horizontalSpacing * 2);
                textBox.Name = tab.Name + label.Text;
                textBox.ReadOnly = readOnly;
                textBox.Visible = true;
                textBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                if (tab.Name == "tabFinal") continue;
                Button btn = new Button();
                tab.Controls.Add(btn);
                btn.Text = "Copy to Final";
                btn.Left = textBox.Left + textBox.Width + horizontalSpacing;
                btn.Top = (col + 1) * verticalSpacing;
                btn.Height = buttonHeight;
                btn.Width = buttonWidth;
                btn.Visible = true;
                btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                btn.Click += new EventHandler(CopyToFinal);
                ArrayList propertyBag = new ArrayList();
                propertyBag.Add(dataRow.Table.Columns[col]);
                propertyBag.Add(textBox);
                btn.Tag = propertyBag;
            }
        }

        private void CopyToFinal(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            ArrayList propertyBag = (ArrayList)btn.Tag;
            DataColumn dc = (DataColumn)propertyBag[0];
            TextBox textBox = (TextBox)propertyBag[1];
            tabFinal.Controls[tabFinal.Name + dc.ColumnName].Text = textBox.Text;
        }
        public frmConflit(DataRow currentRow, DataRow currentdbRow)
        {
            InitializeComponent();
            CurrentDataRow = currentRow;
            FinalDbDataRow = currentdbRow;
        }
        public DataRow CurrentDataRow
        {
            get { return m_currentDataRow; }
            set { m_currentDataRow = value; }
        }

        public DataRow FinalDbDataRow
        {
            get { return m_finalDbDataRow; }
            set { m_finalDbDataRow = value; }
        }

        private void btAccept_Click(object sender, EventArgs e)
        {
            foreach (DataColumn dc in FinalDbDataRow.Table.Columns)
            {
                FinalDbDataRow[dc] =
                   tabFinal.Controls[tabFinal.Name + dc.ColumnName].Text;
            }            
        }

        private void frmConflit_Load(object sender, EventArgs e)
        {
            PopulateTab(tabCurrent, CurrentDataRow,
            DataRowVersion.Current, true);
            PopulateTab(tabOriginal, CurrentDataRow,
               DataRowVersion.Original, true);
            PopulateTab(tabCurrentDB, FinalDbDataRow,
               DataRowVersion.Original, true);
            PopulateTab(tabFinal, FinalDbDataRow,
               DataRowVersion.Current, false);

            
        }

        
    }
}