namespace DigibyteMiner.View.v1.MiningInfo
{
    partial class MinerInfoLogs
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.logBrowser = new System.Windows.Forms.WebBrowser();
            this.btnTemplate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.ForeColor = System.Drawing.Color.White;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 4);
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.Crimson;
            this.linkLabel1.Location = new System.Drawing.Point(503, 18);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(212, 17);
            this.linkLabel1.TabIndex = 17;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Open in Browser. Logs are not Real Time";
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.Visible = false;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // logBrowser
            // 
            this.logBrowser.IsWebBrowserContextMenuEnabled = false;
            this.logBrowser.Location = new System.Drawing.Point(15, 42);
            this.logBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.logBrowser.Name = "logBrowser";
            this.logBrowser.ScriptErrorsSuppressed = true;
            this.logBrowser.Size = new System.Drawing.Size(828, 213);
            this.logBrowser.TabIndex = 16;
            // 
            // btnTemplate
            // 
            this.btnTemplate.BackColor = System.Drawing.Color.SteelBlue;
            this.btnTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTemplate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTemplate.ForeColor = System.Drawing.Color.White;
            this.btnTemplate.Location = new System.Drawing.Point(-44, 13);
            this.btnTemplate.Name = "btnTemplate";
            this.btnTemplate.Size = new System.Drawing.Size(67, 23);
            this.btnTemplate.TabIndex = 15;
            this.btnTemplate.Text = "Default";
            this.btnTemplate.UseVisualStyleBackColor = false;
            this.btnTemplate.Visible = false;
            // 
            // MinerInfoLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.logBrowser);
            this.Controls.Add(this.btnTemplate);
            this.Name = "MinerInfoLogs";
            this.Size = new System.Drawing.Size(1034, 346);
            this.Load += new System.EventHandler(this.MinerInfoLogs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.WebBrowser logBrowser;
        private System.Windows.Forms.Button btnTemplate;
    }
}
