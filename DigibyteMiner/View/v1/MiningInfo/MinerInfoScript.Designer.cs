namespace OneMiner.View.v1.MiningInfo
{
    partial class MinerInfoScript
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
            this.btnTemplate = new System.Windows.Forms.Button();
            this.txtScriptArea = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lnkEdit = new System.Windows.Forms.LinkLabel();
            this.btnDefault = new System.Windows.Forms.Button();
            this.chkTemplate = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTemplate
            // 
            this.btnTemplate.BackColor = System.Drawing.Color.SteelBlue;
            this.btnTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTemplate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTemplate.ForeColor = System.Drawing.Color.White;
            this.btnTemplate.Location = new System.Drawing.Point(-45, 12);
            this.btnTemplate.Name = "btnTemplate";
            this.btnTemplate.Size = new System.Drawing.Size(67, 23);
            this.btnTemplate.TabIndex = 11;
            this.btnTemplate.Text = "Default";
            this.btnTemplate.UseVisualStyleBackColor = false;
            this.btnTemplate.Visible = false;
            // 
            // txtScriptArea
            // 
            this.txtScriptArea.Location = new System.Drawing.Point(12, 53);
            this.txtScriptArea.Multiline = true;
            this.txtScriptArea.Name = "txtScriptArea";
            this.txtScriptArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtScriptArea.Size = new System.Drawing.Size(742, 196);
            this.txtScriptArea.TabIndex = 12;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(763, 80);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(59, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lnkEdit
            // 
            this.lnkEdit.AutoSize = true;
            this.lnkEdit.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkEdit.LinkColor = System.Drawing.Color.SteelBlue;
            this.lnkEdit.Location = new System.Drawing.Point(760, 53);
            this.lnkEdit.Name = "lnkEdit";
            this.lnkEdit.Size = new System.Drawing.Size(25, 13);
            this.lnkEdit.TabIndex = 22;
            this.lnkEdit.TabStop = true;
            this.lnkEdit.Text = "Edit";
            this.lnkEdit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEdit_LinkClicked);
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(763, 125);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(59, 23);
            this.btnDefault.TabIndex = 23;
            this.btnDefault.Text = "Reset";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // chkTemplate
            // 
            this.chkTemplate.AutoSize = true;
            this.chkTemplate.Location = new System.Drawing.Point(329, 12);
            this.chkTemplate.Name = "chkTemplate";
            this.chkTemplate.Size = new System.Drawing.Size(60, 17);
            this.chkTemplate.TabIndex = 24;
            this.chkTemplate.Text = "Default";
            this.chkTemplate.UseVisualStyleBackColor = true;
            this.chkTemplate.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(299, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Selected Miners: ";
            // 
            // MinerInfoScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(927, 344);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkTemplate);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.lnkEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtScriptArea);
            this.Controls.Add(this.btnTemplate);
            this.Name = "MinerInfoScript";
            this.Text = "MinerInfoScript";
            this.Load += new System.EventHandler(this.MinerInfoScript_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTemplate;
        private System.Windows.Forms.TextBox txtScriptArea;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.LinkLabel lnkEdit;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.CheckBox chkTemplate;
        private System.Windows.Forms.Label label1;
    }
}