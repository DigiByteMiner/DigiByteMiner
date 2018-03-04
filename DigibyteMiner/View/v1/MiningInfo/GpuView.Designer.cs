namespace DigibyteMiner.View.v1.MiningInfo
{
    partial class GpuView
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
            this.lblGpuhashrate = new System.Windows.Forms.Label();
            this.pbCardType = new System.Windows.Forms.PictureBox();
            this.lbltemp = new System.Windows.Forms.Label();
            this.lblGpuname = new System.Windows.Forms.Label();
            this.lblFanSpeed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbCardType)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGpuhashrate
            // 
            this.lblGpuhashrate.AutoSize = true;
            this.lblGpuhashrate.BackColor = System.Drawing.Color.Transparent;
            this.lblGpuhashrate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGpuhashrate.ForeColor = System.Drawing.Color.White;
            this.lblGpuhashrate.Location = new System.Drawing.Point(2, 99);
            this.lblGpuhashrate.Name = "lblGpuhashrate";
            this.lblGpuhashrate.Size = new System.Drawing.Size(57, 15);
            this.lblGpuhashrate.TabIndex = 9;
            this.lblGpuhashrate.Text = "Hashrate";
            // 
            // pbCardType
            // 
            this.pbCardType.Image = global::DigibyteMiner.Properties.Resources.amd;
            this.pbCardType.Location = new System.Drawing.Point(4, 4);
            this.pbCardType.Name = "pbCardType";
            this.pbCardType.Size = new System.Drawing.Size(114, 62);
            this.pbCardType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCardType.TabIndex = 6;
            this.pbCardType.TabStop = false;
            // 
            // lbltemp
            // 
            this.lbltemp.AutoSize = true;
            this.lbltemp.BackColor = System.Drawing.Color.Transparent;
            this.lbltemp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltemp.ForeColor = System.Drawing.Color.White;
            this.lbltemp.Location = new System.Drawing.Point(2, 117);
            this.lbltemp.Name = "lbltemp";
            this.lbltemp.Size = new System.Drawing.Size(39, 15);
            this.lbltemp.TabIndex = 10;
            this.lbltemp.Text = "Temp";
            // 
            // lblGpuname
            // 
            this.lblGpuname.AutoSize = true;
            this.lblGpuname.BackColor = System.Drawing.Color.Transparent;
            this.lblGpuname.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGpuname.ForeColor = System.Drawing.Color.White;
            this.lblGpuname.Location = new System.Drawing.Point(1, 69);
            this.lblGpuname.Name = "lblGpuname";
            this.lblGpuname.Size = new System.Drawing.Size(38, 15);
            this.lblGpuname.TabIndex = 11;
            this.lblGpuname.Text = "name";
            // 
            // lblFanSpeed
            // 
            this.lblFanSpeed.AutoSize = true;
            this.lblFanSpeed.BackColor = System.Drawing.Color.Transparent;
            this.lblFanSpeed.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFanSpeed.ForeColor = System.Drawing.Color.White;
            this.lblFanSpeed.Location = new System.Drawing.Point(66, 117);
            this.lblFanSpeed.Name = "lblFanSpeed";
            this.lblFanSpeed.Size = new System.Drawing.Size(26, 15);
            this.lblFanSpeed.TabIndex = 12;
            this.lblFanSpeed.Text = "Fan";
            // 
            // GpuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::DigibyteMiner.Properties.Resources.bg3;
            this.ClientSize = new System.Drawing.Size(158, 136);
            this.Controls.Add(this.lblFanSpeed);
            this.Controls.Add(this.lblGpuname);
            this.Controls.Add(this.lbltemp);
            this.Controls.Add(this.lblGpuhashrate);
            this.Controls.Add(this.pbCardType);
            this.Name = "GpuView";
            this.Text = "GpuData";
            this.Load += new System.EventHandler(this.GpuData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCardType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGpuhashrate;
        private System.Windows.Forms.PictureBox pbCardType;
        private System.Windows.Forms.Label lbltemp;
        private System.Windows.Forms.Label lblGpuname;
        private System.Windows.Forms.Label lblFanSpeed;
    }
}