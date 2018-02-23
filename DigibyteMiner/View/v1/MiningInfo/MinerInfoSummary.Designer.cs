namespace OneMiner.View.v1.MiningInfo
{
    partial class MinerInfoSummary
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
            this.pnlGpus = new System.Windows.Forms.FlowLayoutPanel();
            this.lblGpuInfoStatic = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlGpus
            // 
            this.pnlGpus.BackColor = System.Drawing.Color.White;
            this.pnlGpus.Location = new System.Drawing.Point(12, 0);
            this.pnlGpus.Name = "pnlGpus";
            this.pnlGpus.Size = new System.Drawing.Size(825, 302);
            this.pnlGpus.TabIndex = 19;
            // 
            // lblGpuInfoStatic
            // 
            this.lblGpuInfoStatic.AutoSize = true;
            this.lblGpuInfoStatic.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGpuInfoStatic.ForeColor = System.Drawing.Color.DarkGray;
            this.lblGpuInfoStatic.Location = new System.Drawing.Point(250, 137);
            this.lblGpuInfoStatic.Name = "lblGpuInfoStatic";
            this.lblGpuInfoStatic.Size = new System.Drawing.Size(282, 25);
            this.lblGpuInfoStatic.TabIndex = 20;
            this.lblGpuInfoStatic.Text = "Gpu Info will be populated here";
            // 
            // MinerInfoSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(849, 314);
            this.Controls.Add(this.lblGpuInfoStatic);
            this.Controls.Add(this.pnlGpus);
            this.Name = "MinerInfoSummary";
            this.Text = "MinerInfoSummary";
            this.Load += new System.EventHandler(this.MinerInfoSummary_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlGpus;
        private System.Windows.Forms.Label lblGpuInfoStatic;
    }
}