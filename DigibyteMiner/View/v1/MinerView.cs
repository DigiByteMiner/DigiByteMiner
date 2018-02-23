using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.View.v1.AddMinerScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OneMiner.View.v1
{
    public partial class MinerView : Form
    {
        public IMiner Miner { get; set; }
        MainForm m_Parent = null;
        public MinerView(IMiner miner, MainForm parent)
        {
            Miner = miner;
            m_Parent = parent;

            InitializeComponent();


            pbTemplate.Image = Miner.MainCoin.Logo;

            lblCoinType.Text = Miner.MainCoin.Name;
            if(miner.DualMining)
                lblCoinType.Text = lblCoinType.Text + " + " + Miner.DualCoin.Name;
            lblMinername.Text = Miner.Name;

        }

        private void MinerView_Load(object sender, EventArgs e)
        {
            this.ContextMenuStrip = optionsMenu;
            pbTemplate.Click += FormFocus_handler_Click;
            lblCoinType.Click += FormFocus_handler_Click;
            lblMinername.Click += FormFocus_handler_Click;
            lblMinerState.Click += FormFocus_handler_Click;
            pnlTemplate.Click += FormFocus_handler_Click;

        }

        void FormFocus_handler_Click(object sender, EventArgs e)
        {
            m_Parent.ChangeMiningView(this);
        }
        public void UpdateState()
        {
            UiStateUtil.UpdateState(Miner,lblMinerState, btnStartMining, optionsMenu);
        }

        public void ActivateView()
        {
            pbSelected.Visible = true;
        }
        public void DeActivateView()
        {
            pbSelected.Visible = false;
            //pbSelected.Image = Properties.Resources.selected;
        }       
        public void SelectView()
        {
            this.BackColor = Color.SteelBlue;

        }
        public void DeSelectView()
        {
            this.BackColor = Color.White;

        }


        private void pnlTemplate_Paint(object sender, PaintEventArgs e)
        {

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

        private void deleteMinerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Factory.Instance.CoreObject.Miners.Count<=1)
            {
                MessageBox.Show("Add another miner to delete this!", "Cannot delete the only Miner");
                return;
            }

            DialogResult retVal=MessageBox.Show("Miners once deleted cannot be recovered. Are you sure?", "Delete Miner", MessageBoxButtons.YesNo);
            if(retVal==DialogResult.Yes && Miner!=null)
            {
                Factory.Instance.CoreObject.RemoveMiner(Miner);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditMinerContainer editMiner = new EditMinerContainer();
            editMiner.LoadInfo(Miner);

            editMiner.ShowDialog();
        }


    }
}
