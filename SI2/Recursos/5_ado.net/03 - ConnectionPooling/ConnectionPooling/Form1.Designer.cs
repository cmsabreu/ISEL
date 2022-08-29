namespace ConnectionPooling
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
            this.bt_no_pool = new System.Windows.Forms.Button();
            this.bt_oleDB = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.bt_3_con = new System.Windows.Forms.Button();
            this.csList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // bt_no_pool
            // 
            this.bt_no_pool.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_no_pool.Location = new System.Drawing.Point(24, 25);
            this.bt_no_pool.Name = "bt_no_pool";
            this.bt_no_pool.Size = new System.Drawing.Size(461, 39);
            this.bt_no_pool.TabIndex = 0;
            this.bt_no_pool.Text = "no Pooling";
            this.bt_no_pool.UseVisualStyleBackColor = true;
            this.bt_no_pool.Click += new System.EventHandler(this.bt_no_pool_Click);
            // 
            // bt_oleDB
            // 
            this.bt_oleDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_oleDB.Location = new System.Drawing.Point(24, 70);
            this.bt_oleDB.Name = "bt_oleDB";
            this.bt_oleDB.Size = new System.Drawing.Size(461, 37);
            this.bt_oleDB.TabIndex = 3;
            this.bt_oleDB.Text = "OleDB";
            this.bt_oleDB.UseVisualStyleBackColor = true;
            this.bt_oleDB.Click += new System.EventHandler(this.bt_oleDB_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker3_DoWork);
            // 
            // bt_3_con
            // 
            this.bt_3_con.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_3_con.Location = new System.Drawing.Point(24, 113);
            this.bt_3_con.Name = "bt_3_con";
            this.bt_3_con.Size = new System.Drawing.Size(461, 32);
            this.bt_3_con.TabIndex = 6;
            this.bt_3_con.Text = "3 Connections (0s, 10s, 15s)";
            this.bt_3_con.UseVisualStyleBackColor = true;
            this.bt_3_con.Click += new System.EventHandler(this.bt_3_con_Click);
            // 
            // csList
            // 
            this.csList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.csList.FormattingEnabled = true;
            this.csList.ItemHeight = 20;
            this.csList.Location = new System.Drawing.Point(24, 164);
            this.csList.Name = "csList";
            this.csList.Size = new System.Drawing.Size(461, 64);
            this.csList.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 250);
            this.Controls.Add(this.csList);
            this.Controls.Add(this.bt_3_con);
            this.Controls.Add(this.bt_oleDB);
            this.Controls.Add(this.bt_no_pool);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection Pool Example";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_no_pool;
        private System.Windows.Forms.Button bt_oleDB;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.Button bt_3_con;
        private System.Windows.Forms.ListBox csList;
    }
}

