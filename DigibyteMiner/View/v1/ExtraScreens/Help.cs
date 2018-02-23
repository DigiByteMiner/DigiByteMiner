using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OneMiner.View.v1.ExtraScreens
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("https://github.com/arunsatyarth/OneMiner/issues");
            }
            catch (Exception se)
            {
            }
        }

        private void Help_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://arunsatyarth.github.io/OneMiner/");
            }
            catch (Exception se)
            {
            }
        }
    }
}
