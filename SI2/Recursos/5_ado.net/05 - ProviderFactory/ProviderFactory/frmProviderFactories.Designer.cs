namespace ProviderFactory
{
   partial class frmProviderFactories
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
          this.button1 = new System.Windows.Forms.Button();
          this.dataGridView1 = new System.Windows.Forms.DataGridView();
          ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
          this.SuspendLayout();
          // 
          // button1
          // 
          this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.button1.Location = new System.Drawing.Point(3, 13);
          this.button1.Name = "button1";
          this.button1.Size = new System.Drawing.Size(296, 35);
          this.button1.TabIndex = 0;
          this.button1.Text = "Provider Factories";
          this.button1.Click += new System.EventHandler(this.button1_Click);
          // 
          // dataGridView1
          // 
          this.dataGridView1.AllowUserToAddRows = false;
          this.dataGridView1.AllowUserToDeleteRows = false;
          this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
          this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
          dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
          dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
          dataGridViewCellStyle1.FormatProvider = new System.Globalization.CultureInfo("en-US");
          dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
          this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
          this.dataGridView1.Location = new System.Drawing.Point(3, 54);
          this.dataGridView1.Name = "dataGridView1";
          this.dataGridView1.ReadOnly = true;
          this.dataGridView1.Size = new System.Drawing.Size(757, 291);
          this.dataGridView1.TabIndex = 1;
          this.dataGridView1.Text = "dataGridView1";
          this.dataGridView1.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseDoubleClick);
          // 
          // frmProviderFactories
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(761, 347);
          this.Controls.Add(this.dataGridView1);
          this.Controls.Add(this.button1);
          this.Name = "frmProviderFactories";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "frmProviderFactories";
          ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.DataGridView dataGridView1;
   }
}