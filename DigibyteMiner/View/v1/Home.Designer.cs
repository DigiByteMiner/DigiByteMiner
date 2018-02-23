namespace DigibyteMiner.View.v1
{
    partial class Home
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
            this.lblMinerState = new System.Windows.Forms.Label();
            this.btnStartMining = new System.Windows.Forms.Button();
            this.lblShares = new System.Windows.Forms.Label();
            this.lblTotalHashrate = new System.Windows.Forms.Label();
            this.pbTemplate = new System.Windows.Forms.PictureBox();
            this.lblMinerType = new System.Windows.Forms.Label();
            this.lblMinername = new System.Windows.Forms.Label();
            this.lnlMainCoinPool = new System.Windows.Forms.LinkLabel();
            this.lnDualCoinPool = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbTemplate)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMinerState
            // 
            this.lblMinerState.AutoSize = true;
            this.lblMinerState.BackColor = System.Drawing.Color.White;
            this.lblMinerState.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinerState.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblMinerState.Location = new System.Drawing.Point(21, 154);
            this.lblMinerState.Name = "lblMinerState";
            this.lblMinerState.Size = new System.Drawing.Size(54, 15);
            this.lblMinerState.TabIndex = 33;
            this.lblMinerState.Text = "Stopped";
            // 
            // btnStartMining
            // 
            this.btnStartMining.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartMining.Location = new System.Drawing.Point(22, 178);
            this.btnStartMining.Name = "btnStartMining";
            this.btnStartMining.Size = new System.Drawing.Size(53, 25);
            this.btnStartMining.TabIndex = 32;
            this.btnStartMining.Text = "Start";
            this.btnStartMining.UseVisualStyleBackColor = true;
            // 
            // lblShares
            // 
            this.lblShares.AutoSize = true;
            this.lblShares.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShares.Location = new System.Drawing.Point(106, 188);
            this.lblShares.Name = "lblShares";
            this.lblShares.Size = new System.Drawing.Size(84, 13);
            this.lblShares.TabIndex = 31;
            this.lblShares.Text = "Shares: 0 S, 0 R";
            // 
            // lblTotalHashrate
            // 
            this.lblTotalHashrate.AutoSize = true;
            this.lblTotalHashrate.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalHashrate.Location = new System.Drawing.Point(205, 121);
            this.lblTotalHashrate.Name = "lblTotalHashrate";
            this.lblTotalHashrate.Size = new System.Drawing.Size(91, 25);
            this.lblTotalHashrate.TabIndex = 30;
            this.lblTotalHashrate.Text = "Hashrate";
            // 
            // pbTemplate
            // 
            this.pbTemplate.Image = global::DigibyteMiner.Properties.Resources.digibyte;
            this.pbTemplate.Location = new System.Drawing.Point(24, 40);
            this.pbTemplate.Name = "pbTemplate";
            this.pbTemplate.Size = new System.Drawing.Size(65, 63);
            this.pbTemplate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbTemplate.TabIndex = 28;
            this.pbTemplate.TabStop = false;
            // 
            // lblMinerType
            // 
            this.lblMinerType.AutoSize = true;
            this.lblMinerType.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinerType.Location = new System.Drawing.Point(106, 74);
            this.lblMinerType.Name = "lblMinerType";
            this.lblMinerType.Size = new System.Drawing.Size(68, 13);
            this.lblMinerType.TabIndex = 27;
            this.lblMinerType.Text = "DefaultType";
            // 
            // lblMinername
            // 
            this.lblMinername.AutoSize = true;
            this.lblMinername.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinername.Location = new System.Drawing.Point(105, 40);
            this.lblMinername.Name = "lblMinername";
            this.lblMinername.Size = new System.Drawing.Size(97, 20);
            this.lblMinername.TabIndex = 23;
            this.lblMinername.Text = "Defaultname";
            // 
            // lnlMainCoinPool
            // 
            this.lnlMainCoinPool.AutoSize = true;
            this.lnlMainCoinPool.LinkArea = new System.Windows.Forms.LinkArea(0, 15);
            this.lnlMainCoinPool.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnlMainCoinPool.LinkColor = System.Drawing.Color.RoyalBlue;
            this.lnlMainCoinPool.Location = new System.Drawing.Point(8, 21);
            this.lnlMainCoinPool.Name = "lnlMainCoinPool";
            this.lnlMainCoinPool.Size = new System.Drawing.Size(55, 17);
            this.lnlMainCoinPool.TabIndex = 19;
            this.lnlMainCoinPool.TabStop = true;
            this.lnlMainCoinPool.Text = "Main Pool";
            this.lnlMainCoinPool.UseCompatibleTextRendering = true;
            // 
            // lnDualCoinPool
            // 
            this.lnDualCoinPool.AutoSize = true;
            this.lnDualCoinPool.LinkArea = new System.Windows.Forms.LinkArea(0, 15);
            this.lnDualCoinPool.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnDualCoinPool.LinkColor = System.Drawing.Color.RoyalBlue;
            this.lnDualCoinPool.Location = new System.Drawing.Point(101, 21);
            this.lnDualCoinPool.Name = "lnDualCoinPool";
            this.lnDualCoinPool.Size = new System.Drawing.Size(53, 17);
            this.lnDualCoinPool.TabIndex = 21;
            this.lnDualCoinPool.TabStop = true;
            this.lnDualCoinPool.Text = "Dual Pool";
            this.lnDualCoinPool.UseCompatibleTextRendering = true;
            this.lnDualCoinPool.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lnlMainCoinPool);
            this.groupBox1.Controls.Add(this.lnDualCoinPool);
            this.groupBox1.Location = new System.Drawing.Point(75, 349);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 44);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Visit Pool Account";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(356, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 35;
            this.label1.Text = "Algorithm";
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 465);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMinerState);
            this.Controls.Add(this.btnStartMining);
            this.Controls.Add(this.lblShares);
            this.Controls.Add(this.lblTotalHashrate);
            this.Controls.Add(this.pbTemplate);
            this.Controls.Add(this.lblMinerType);
            this.Controls.Add(this.lblMinername);
            this.Controls.Add(this.groupBox1);
            this.Name = "Home";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbTemplate)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMinerState;
        private System.Windows.Forms.Button btnStartMining;
        private System.Windows.Forms.Label lblShares;
        private System.Windows.Forms.Label lblTotalHashrate;
        private System.Windows.Forms.PictureBox pbTemplate;
        private System.Windows.Forms.Label lblMinerType;
        private System.Windows.Forms.Label lblMinername;
        private System.Windows.Forms.LinkLabel lnlMainCoinPool;
        private System.Windows.Forms.LinkLabel lnDualCoinPool;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
    }
}