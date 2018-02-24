namespace DigibyteMiner.View.v1
{
    partial class Profiles
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
            this.pnlMiner = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlMinerInfo = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlMiner
            // 
            this.pnlMiner.AutoScroll = true;
            this.pnlMiner.BackColor = System.Drawing.Color.Transparent;
            this.pnlMiner.Location = new System.Drawing.Point(13, 11);
            this.pnlMiner.Name = "pnlMiner";
            this.pnlMiner.Size = new System.Drawing.Size(417, 428);
            this.pnlMiner.TabIndex = 5;
            // 
            // pnlMinerInfo
            // 
            this.pnlMinerInfo.BackColor = System.Drawing.Color.White;
            this.pnlMinerInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMinerInfo.Location = new System.Drawing.Point(452, 12);
            this.pnlMinerInfo.Name = "pnlMinerInfo";
            this.pnlMinerInfo.Size = new System.Drawing.Size(527, 427);
            this.pnlMinerInfo.TabIndex = 4;
            // 
            // Profiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1001, 463);
            this.Controls.Add(this.pnlMiner);
            this.Controls.Add(this.pnlMinerInfo);
            this.Name = "Profiles";
            this.Text = "Profiles";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlMiner;
        private System.Windows.Forms.Panel pnlMinerInfo;
    }
}