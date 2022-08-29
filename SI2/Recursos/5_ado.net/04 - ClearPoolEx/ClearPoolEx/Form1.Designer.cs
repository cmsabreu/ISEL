namespace ClearPoolEx
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
            this.btLigar = new System.Windows.Forms.Button();
            this.bt_clearPool = new System.Windows.Forms.Button();
            this.btInit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btLigar
            // 
            this.btLigar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLigar.Location = new System.Drawing.Point(23, 12);
            this.btLigar.Name = "btLigar";
            this.btLigar.Size = new System.Drawing.Size(243, 64);
            this.btLigar.TabIndex = 0;
            this.btLigar.Text = "Ligar";
            this.btLigar.UseVisualStyleBackColor = true;
            this.btLigar.Click += new System.EventHandler(this.btLigar_Click);
            // 
            // bt_clearPool
            // 
            this.bt_clearPool.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_clearPool.Location = new System.Drawing.Point(23, 104);
            this.bt_clearPool.Name = "bt_clearPool";
            this.bt_clearPool.Size = new System.Drawing.Size(243, 55);
            this.bt_clearPool.TabIndex = 1;
            this.bt_clearPool.Text = "ligar (clearPool)";
            this.bt_clearPool.UseVisualStyleBackColor = true;
            this.bt_clearPool.Click += new System.EventHandler(this.bt_clearPool_Click);
            // 
            // btInit
            // 
            this.btInit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btInit.Location = new System.Drawing.Point(23, 194);
            this.btInit.Name = "btInit";
            this.btInit.Size = new System.Drawing.Size(243, 49);
            this.btInit.TabIndex = 2;
            this.btInit.Text = "Iniciar o Pool ( 3 ligações)";
            this.btInit.UseVisualStyleBackColor = true;
            this.btInit.Click += new System.EventHandler(this.btInit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 255);
            this.Controls.Add(this.btInit);
            this.Controls.Add(this.bt_clearPool);
            this.Controls.Add(this.btLigar);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ClearPoolEx";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btLigar;
        private System.Windows.Forms.Button bt_clearPool;
        private System.Windows.Forms.Button btInit;
    }
}

