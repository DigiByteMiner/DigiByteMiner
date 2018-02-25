namespace DigibyteMiner.View.v1
{
    partial class AddMiner
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtMinername = new System.Windows.Forms.TextBox();
            this.lbAlgoSelect = new System.Windows.Forms.ListBox();
            this.lbCoinSelect = new System.Windows.Forms.ListView();
            this.lblSelectedCoin = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pbSelectedMiner = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedMiner)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(62, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Miner Name";
            // 
            // txtMinername
            // 
            this.txtMinername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinername.Location = new System.Drawing.Point(224, 29);
            this.txtMinername.Name = "txtMinername";
            this.txtMinername.Size = new System.Drawing.Size(121, 25);
            this.txtMinername.TabIndex = 1;
            this.txtMinername.TextChanged += new System.EventHandler(this.txtMinername_TextChanged);
            // 
            // lbAlgoSelect
            // 
            this.lbAlgoSelect.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAlgoSelect.FormattingEnabled = true;
            this.lbAlgoSelect.ItemHeight = 17;
            this.lbAlgoSelect.Location = new System.Drawing.Point(65, 124);
            this.lbAlgoSelect.Name = "lbAlgoSelect";
            this.lbAlgoSelect.Size = new System.Drawing.Size(162, 208);
            this.lbAlgoSelect.TabIndex = 6;
            // 
            // lbCoinSelect
            // 
            this.lbCoinSelect.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCoinSelect.HideSelection = false;
            this.lbCoinSelect.Location = new System.Drawing.Point(262, 124);
            this.lbCoinSelect.MultiSelect = false;
            this.lbCoinSelect.Name = "lbCoinSelect";
            this.lbCoinSelect.Size = new System.Drawing.Size(251, 205);
            this.lbCoinSelect.TabIndex = 10;
            this.lbCoinSelect.UseCompatibleStateImageBehavior = false;
            this.lbCoinSelect.View = System.Windows.Forms.View.Tile;
            // 
            // lblSelectedCoin
            // 
            this.lblSelectedCoin.AutoSize = true;
            this.lblSelectedCoin.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedCoin.Location = new System.Drawing.Point(268, 73);
            this.lblSelectedCoin.Name = "lblSelectedCoin";
            this.lblSelectedCoin.Size = new System.Drawing.Size(125, 20);
            this.lblSelectedCoin.TabIndex = 12;
            this.lblSelectedCoin.Text = "Default Selection";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(62, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Selected Algorithm";
            // 
            // pbSelectedMiner
            // 
            this.pbSelectedMiner.Image = global::DigibyteMiner.Properties.Resources.digibyte;
            this.pbSelectedMiner.Location = new System.Drawing.Point(224, 67);
            this.pbSelectedMiner.Name = "pbSelectedMiner";
            this.pbSelectedMiner.Size = new System.Drawing.Size(42, 32);
            this.pbSelectedMiner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSelectedMiner.TabIndex = 13;
            this.pbSelectedMiner.TabStop = false;
            // 
            // AddMiner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(681, 531);
            this.Controls.Add(this.pbSelectedMiner);
            this.Controls.Add(this.lblSelectedCoin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbCoinSelect);
            this.Controls.Add(this.lbAlgoSelect);
            this.Controls.Add(this.txtMinername);
            this.Controls.Add(this.label1);
            this.Name = "AddMiner";
            this.Text = "AddMiner";
            this.Load += new System.EventHandler(this.AddMiner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedMiner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMinername;
        private System.Windows.Forms.ListBox lbAlgoSelect;
        private System.Windows.Forms.ListView lbCoinSelect;
        private System.Windows.Forms.Label lblSelectedCoin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbSelectedMiner;
    }
}