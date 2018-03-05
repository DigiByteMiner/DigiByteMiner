namespace DigibyteMiner.View.v1
{
    partial class MinerInfo
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
            this.btntabScript = new System.Windows.Forms.Button();
            this.btntabLogs = new System.Windows.Forms.Button();
            this.btntabInfo = new System.Windows.Forms.Button();
            this.pnlMinerInfo = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btntabScript
            // 
            this.btntabScript.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btntabScript.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btntabScript.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntabScript.Location = new System.Drawing.Point(91, 12);
            this.btntabScript.Name = "btntabScript";
            this.btntabScript.Size = new System.Drawing.Size(75, 23);
            this.btntabScript.TabIndex = 10;
            this.btntabScript.Text = "Script";
            this.btntabScript.UseVisualStyleBackColor = false;
            this.btntabScript.Click += new System.EventHandler(this.btntabScript_Click);
            // 
            // btntabLogs
            // 
            this.btntabLogs.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btntabLogs.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btntabLogs.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntabLogs.Location = new System.Drawing.Point(162, 12);
            this.btntabLogs.Name = "btntabLogs";
            this.btntabLogs.Size = new System.Drawing.Size(75, 23);
            this.btntabLogs.TabIndex = 9;
            this.btntabLogs.Text = "Logs";
            this.btntabLogs.UseVisualStyleBackColor = false;
            this.btntabLogs.Click += new System.EventHandler(this.btntabLogs_Click);
            // 
            // btntabInfo
            // 
            this.btntabInfo.BackColor = System.Drawing.Color.SteelBlue;
            this.btntabInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btntabInfo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntabInfo.ForeColor = System.Drawing.Color.White;
            this.btntabInfo.Location = new System.Drawing.Point(19, 12);
            this.btntabInfo.Name = "btntabInfo";
            this.btntabInfo.Size = new System.Drawing.Size(75, 23);
            this.btntabInfo.TabIndex = 8;
            this.btntabInfo.Text = "GPU Info";
            this.btntabInfo.UseVisualStyleBackColor = false;
            this.btntabInfo.Click += new System.EventHandler(this.btntabInfo_Click);
            // 
            // pnlMinerInfo
            // 
            this.pnlMinerInfo.AutoScroll = true;
            this.pnlMinerInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnlMinerInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMinerInfo.Location = new System.Drawing.Point(19, 45);
            this.pnlMinerInfo.Name = "pnlMinerInfo";
            this.pnlMinerInfo.Size = new System.Drawing.Size(1021, 293);
            this.pnlMinerInfo.TabIndex = 14;
            // 
            // MinerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::DigibyteMiner.Properties.Resources.bg4;
            this.ClientSize = new System.Drawing.Size(1132, 346);
            this.Controls.Add(this.pnlMinerInfo);
            this.Controls.Add(this.btntabScript);
            this.Controls.Add(this.btntabLogs);
            this.Controls.Add(this.btntabInfo);
            this.Name = "MinerInfo";
            this.Text = "MinerInfo";
            this.Load += new System.EventHandler(this.MinerInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btntabScript;
        private System.Windows.Forms.Button btntabLogs;
        private System.Windows.Forms.Button btntabInfo;
        private System.Windows.Forms.Panel pnlMinerInfo;
    }
}