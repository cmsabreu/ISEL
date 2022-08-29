namespace ConflitResolutionEx
{
    partial class frmConflit
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btCancel = new System.Windows.Forms.Button();
            this.btAccept = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCurrent = new System.Windows.Forms.TabPage();
            this.tabOriginal = new System.Windows.Forms.TabPage();
            this.tabCurrentDB = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabFinal = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.btAccept);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 324);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(441, 76);
            this.panel1.TabIndex = 0;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(217, 26);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(105, 31);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Cancelar";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btAccept
            // 
            this.btAccept.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btAccept.Location = new System.Drawing.Point(98, 27);
            this.btAccept.Name = "btAccept";
            this.btAccept.Size = new System.Drawing.Size(100, 31);
            this.btAccept.TabIndex = 0;
            this.btAccept.Text = "Aceitar valor final";
            this.btAccept.UseVisualStyleBackColor = true;
            this.btAccept.Click += new System.EventHandler(this.btAccept_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer1.Size = new System.Drawing.Size(441, 324);
            this.splitContainer1.SplitterDistance = 185;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCurrent);
            this.tabControl1.Controls.Add(this.tabOriginal);
            this.tabControl1.Controls.Add(this.tabCurrentDB);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(441, 185);
            this.tabControl1.TabIndex = 0;
            // 
            // tabCurrent
            // 
            this.tabCurrent.Location = new System.Drawing.Point(4, 24);
            this.tabCurrent.Name = "tabCurrent";
            this.tabCurrent.Padding = new System.Windows.Forms.Padding(3);
            this.tabCurrent.Size = new System.Drawing.Size(433, 157);
            this.tabCurrent.TabIndex = 0;
            this.tabCurrent.Text = "Valor corrente";
            this.tabCurrent.UseVisualStyleBackColor = true;
            // 
            // tabOriginal
            // 
            this.tabOriginal.Location = new System.Drawing.Point(4, 24);
            this.tabOriginal.Name = "tabOriginal";
            this.tabOriginal.Padding = new System.Windows.Forms.Padding(3);
            this.tabOriginal.Size = new System.Drawing.Size(433, 157);
            this.tabOriginal.TabIndex = 1;
            this.tabOriginal.Text = "Original DB";
            this.tabOriginal.UseVisualStyleBackColor = true;
            // 
            // tabCurrentDB
            // 
            this.tabCurrentDB.Location = new System.Drawing.Point(4, 24);
            this.tabCurrentDB.Name = "tabCurrentDB";
            this.tabCurrentDB.Padding = new System.Windows.Forms.Padding(3);
            this.tabCurrentDB.Size = new System.Drawing.Size(433, 157);
            this.tabCurrentDB.TabIndex = 2;
            this.tabCurrentDB.Text = "DB Corrente";
            this.tabCurrentDB.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabFinal);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(441, 135);
            this.tabControl2.TabIndex = 0;
            // 
            // tabFinal
            // 
            this.tabFinal.Location = new System.Drawing.Point(4, 24);
            this.tabFinal.Name = "tabFinal";
            this.tabFinal.Padding = new System.Windows.Forms.Padding(3);
            this.tabFinal.Size = new System.Drawing.Size(433, 107);
            this.tabFinal.TabIndex = 1;
            this.tabFinal.Text = "Final";
            this.tabFinal.UseVisualStyleBackColor = true;
            // 
            // frmConflit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 400);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "frmConflit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmConflit";
            this.Load += new System.EventHandler(this.frmConflit_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btAccept;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCurrent;
        private System.Windows.Forms.TabPage tabOriginal;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabFinal;
        private System.Windows.Forms.TabPage tabCurrentDB;

    }
}