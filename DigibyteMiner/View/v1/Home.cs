using DigibyteMiner.Core;
using DigibyteMiner.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigibyteMiner.View.v1
{
    public partial class Home : Form
    {

        public IMiner Miner { get; set; }
        MainForm m_Parent = null;
        public Home(IMiner miner, MainForm parent)
        {
            Miner = miner;
            m_Parent = parent;

            InitializeComponent();


            /*
            lblCoinType.Text = Miner.MainCoin.Name;
            lblMinername.Text = Miner.Name;
            */
        }
        private void Home_Load(object sender, EventArgs e)
        {
            /*
            this.ContextMenuStrip = optionsMenu;
            pbTemplate.Click += FormFocus_handler_Click;
            lblCoinType.Click += FormFocus_handler_Click;
            lblMinername.Click += FormFocus_handler_Click;
            lblMinerState.Click += FormFocus_handler_Click;
            pnlTemplate.Click += FormFocus_handler_Click;
            */
        }
    

        void FormFocus_handler_Click(object sender, EventArgs e)
        {
            //m_Parent.ChangeMiningView(this);
        }
        public void UpdateState()
        {
            //UiStateUtil.UpdateState(Miner,lblMinerState, btnStartMining, optionsMenu);
        }




        private void btnStartMining_Click(object sender, EventArgs e)
        {
            UiStateUtil.MiningStartAction(Miner);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MinerView view = sender as MinerView;
            var menuItem = sender as ToolStripMenuItem;
            var contextMenu = menuItem.GetCurrentParent() as ContextMenuStrip;
            MinerView view = contextMenu.SourceControl as MinerView;
            if (view != null)
            {
                Factory.Instance.CoreObject.SelectMiner(view.Miner);
            }


        }

        private void startMiningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnStartMining_Click(sender, e);
        }



    }
}
