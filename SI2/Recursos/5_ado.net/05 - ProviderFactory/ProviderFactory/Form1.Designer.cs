namespace ProviderFactory
{
    partial class Form1
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
            this.btExec = new System.Windows.Forms.Button();
            this.btFactList = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btExec
            // 
            this.btExec.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExec.Location = new System.Drawing.Point(31, 12);
            this.btExec.Name = "btExec";
            this.btExec.Size = new System.Drawing.Size(228, 41);
            this.btExec.TabIndex = 0;
            this.btExec.Text = "Exec";
            this.btExec.UseVisualStyleBackColor = true;
            this.btExec.Click += new System.EventHandler(this.btExec_Click);
            // 
            // btFactList
            // 
            this.btFactList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btFactList.Location = new System.Drawing.Point(31, 67);
            this.btFactList.Name = "btFactList";
            this.btFactList.Size = new System.Drawing.Size(227, 35);
            this.btFactList.TabIndex = 1;
            this.btFactList.Text = "FactoryList";
            this.btFactList.UseVisualStyleBackColor = true;
            this.btFactList.Click += new System.EventHandler(this.btFactList_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 112);
            this.Controls.Add(this.btFactList);
            this.Controls.Add(this.btExec);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProviderFactory";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btExec;
        private System.Windows.Forms.Button btFactList;
    }
}

