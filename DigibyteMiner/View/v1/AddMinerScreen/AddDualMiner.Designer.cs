namespace OneMiner.View.v1
{
    partial class AddDualMiner
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
            this.lblSelectedCoin = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbCoinSelect = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lblSelectedCoin
            // 
            this.lblSelectedCoin.AutoSize = true;
            this.lblSelectedCoin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedCoin.Location = new System.Drawing.Point(315, 40);
            this.lblSelectedCoin.Name = "lblSelectedCoin";
            this.lblSelectedCoin.Size = new System.Drawing.Size(96, 15);
            this.lblSelectedCoin.TabIndex = 11;
            this.lblSelectedCoin.Text = "Default Selection";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(157, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Selected Coin";
            // 
            // lbCoinSelect
            // 
            this.lbCoinSelect.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCoinSelect.HideSelection = false;
            this.lbCoinSelect.Location = new System.Drawing.Point(158, 83);
            this.lbCoinSelect.MultiSelect = false;
            this.lbCoinSelect.Name = "lbCoinSelect";
            this.lbCoinSelect.Size = new System.Drawing.Size(271, 205);
            this.lbCoinSelect.TabIndex = 12;
            this.lbCoinSelect.UseCompatibleStateImageBehavior = false;
            this.lbCoinSelect.View = System.Windows.Forms.View.Tile;
            // 
            // AddDualMiner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(707, 381);
            this.Controls.Add(this.lbCoinSelect);
            this.Controls.Add(this.lblSelectedCoin);
            this.Controls.Add(this.label3);
            this.Name = "AddDualMiner";
            this.Text = "AddDualMiner";
            this.Load += new System.EventHandler(this.AddDualMiner_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelectedCoin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lbCoinSelect;
    }
}