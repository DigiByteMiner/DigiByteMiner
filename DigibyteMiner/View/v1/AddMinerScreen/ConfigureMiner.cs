using OneMiner.Core.Interfaces;
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
    public partial class ConfigureMiner : Form, ICoinConfigurer
    {
        private AddMinerContainer m_parent = null;
        private ICoin m_selected_coin = null;

        public string Pool { get; set; }
        public string Wallet { get; set; }
        public string PoolAccount { get; set; }

        public ConfigureMiner()
        {

            InitializeComponent();
#if DEBUG
            btnFillDefaultAddress.Visible = true;
#endif
        }
        public void AssignParent(object parent)
        {
            m_parent = parent as AddMinerContainer;
        }
        public void AssignCoin(ICoin coin)
        {
            m_selected_coin = coin;
        }

        private void ConfigureMiner_Load(object sender, EventArgs e)
        {



            if (m_selected_coin != null)
            {
                lblCoinName.Text = m_selected_coin.Name;
                txtPool.Text = Pool;
                txtWallet.Text = Wallet;
                txtPoolAccount.Text = PoolAccount;

                cmbPoolList.SelectedIndexChanged += cmbPoolList_SelectedIndexChanged;
                foreach (Pool item in m_selected_coin.GetPools())
                {
                    cmbPoolList.Items.Add(item.Name);

                }

            }
        }
        private Pool GetPool()
        {
            Pool pool = null;
            try
            {
                List<Pool> pools = m_selected_coin.GetPools();
                int index = cmbPoolList.SelectedIndex;
                pool = pools[index];
            }
            catch (Exception e)
            {
                return null;
            }
            return pool;
        }
        void cmbPoolList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Pool pool = GetPool();
                if (pool != null)
                {
                    txtPool.Text = pool.Link;
                    txtPoolAccount.Text = pool.GetAccountLink(txtWallet.Text);
                }
            }
            catch (Exception de)
            {
            }
        }

        private void btnAddDualMiner_Click(object sender, EventArgs e)
        {

        }

        public void CalculatePoolAccount()
        {
            try
            {
                Pool pool = GetPool();
                if (pool != null)
                {
                    txtPoolAccount.Text = pool.GetAccountLink(txtWallet.Text);
                }
            }
            catch (Exception de)
            {
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Pool = txtPool.Text.Trim();
            //CalculatePoolAccount();
            //cmbPoolList.SelectedIndex = -1;
            cmbPoolList.Text = "Select Pool";
        }

        private void txtWallet_TextChanged(object sender, EventArgs e)
        {
            Wallet = txtWallet.Text.Trim();
            CalculatePoolAccount();

        }

        private void txtPoolAccount_TextChanged(object sender, EventArgs e)
        {
            PoolAccount = txtPoolAccount.Text.Trim();

        }

        private void btnFillDefaultAddress_Click(object sender, EventArgs e)
        {
            txtWallet.Text=m_selected_coin.DefaultAddress;
        }
    }
}
