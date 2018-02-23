using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OneMiner.View.v1.Profitability
{
    public partial class Profitability : Form
    {
        public Profitability()
        {
            InitializeComponent();
        }

        private void Profitability_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
