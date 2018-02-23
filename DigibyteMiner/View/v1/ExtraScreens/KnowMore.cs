using OneMiner.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneMiner.View.v1.ExtraScreens
{
    public partial class KnowMore : Form
    {
        ICoin m_Maincoin = null;
        public KnowMore(ICoin coin)
        {
            m_Maincoin = coin;
            InitializeComponent();
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

        private void KnowMore_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            linkLabel3.Text = m_Maincoin.Name;

            switch(m_Maincoin.Algorithm.Name)
            {
                case "Ethhash":
                    linkLabel1.Text = @"All Ethhash based coins are currently supported on both Nvidia and AMD Radeon GPUs. Ethhash based coins generally need 
really good GPU for mining. CPU mining is not supported for this coin.If miner does not find a proper GPU, you might get an error. If you dont have a suitable GPU, try Ubiq, Expanse or Equihash coins like ZClassic.

In Dual mining mode, hashrate of the dual coins is not displayed currently. This will be fixed in future versions";
                    break;
                case "Equihash":
                    linkLabel1.Text = @"All Equihash based coins are currently supported on both Nvidia and AMD Radeon GPUs.Equihash coins need the least power
and hardware among other coins present here. If you still face issues, try with low difficulty coins like ZClassic, Zencash etc";
                    break;

                case "CryptoNote":
                    linkLabel1.Text = @"CryptoNight is supported on CPUs and AMD GPUs. Nvidia(ccminer) has been found to be a bit unstable on cryptonight. Some Antivirus, tags the ccminer.exe which is used for mining in Nvidia, as a virus and hence even OneMiner as a virus. Henceforth it is supported but not selected by default while creating a new miner.
But it can be manually selected from checkbox in the scripts tab. Use it only if you need it.
Also monitoring of hashrate and shares submitted, does not work for NVidia. But do not worry as the miner is submitting shares behind the scenes.";
                    break;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://github.com/arunsatyarth/OneMiner/releases");
            }
            catch (Exception se)
            {
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
