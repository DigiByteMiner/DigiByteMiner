using DigibyteMiner.Core.Interfaces;
using DigibyteMiner.View.v1.AddMinerScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DigibyteMiner.View.v1
{
    public partial class ConfigureMiner : Form, ICoinConfigurer
    {
        private IMinerContainer m_parent = null;
        private ICoin m_selected_coin = null;

        public string Pool { get; set; }
        public string Wallet { get; set; }
        public string Password { get; set; }

        public string PoolAccount { get; set; }
        public Pool m_CurrentPool = null;
        public ConfigureMiner()
        {

            InitializeComponent();
#if DEBUG
            //btnFillDefaultAddress.Visible = true;
#endif
        }
        public void AssignParent(object parent)
        {
            m_parent = parent as AddMinerContainer;
            if(m_parent==null)
                m_parent = parent as EditMinerContainer;
        }
        public void AssignCoin(ICoin coin)
        {
            m_selected_coin = coin;
        }
        private void ShowWalletmessage(Pool pool)
        {
            if (pool == null)
            {
                lblWalletComment.Text = "";
                m_parent.EnableNextButton();
                return;
            }
            if (pool.ValidateAddress(txtWallet.Text) == false)
            {
                lblWalletComment.Text = pool.WrongWallet;
                m_parent.DisableNextButton();
            }
            else
            {
                lblWalletComment.Text = "";
                m_parent.EnableNextButton();

            }

        }
        private void ConfigureMiner_Load(object sender, EventArgs e)
        {



            if (m_selected_coin != null)
            {
                pbSelectedMiner.Image = m_selected_coin.Logo;
                lblCoinName.Text = m_selected_coin.Algorithm.Name+ " > "+ m_selected_coin.Name;
                txtPool.Text = Pool;
                txtWallet.Text = Wallet;
                txtPassword.Text = Password;
                txtPoolAccount.Text = PoolAccount;
                m_CurrentPool = null;
                ShowWalletmessage(m_CurrentPool);

                cmbPoolList.SelectedIndexChanged += cmbPoolList_SelectedIndexChanged;
                int i = 0;
                foreach (Pool item in m_selected_coin.GetPools())
                {
                    if (i == 0)
                    {
                        if (((EditMinerContainer)m_parent).Miner.DefaultMiner)
                        {
                            lblWalletName.Text = item.WalletName;
                            txtWallet.Text = item.WalletAddress;
                            m_CurrentPool = item;
                            ShowWalletmessage(m_CurrentPool);

                        }

           
                    }
                    cmbPoolList.Items.Add(item.Name);
                    i++;
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
                    lblWalletName.Text = pool.WalletName;
                    m_CurrentPool = pool;
                    ShowWalletmessage(m_CurrentPool);

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
            m_CurrentPool = null;
            ShowWalletmessage(m_CurrentPool);

        }

        private void txtWallet_TextChanged(object sender, EventArgs e)
        {
            Wallet = txtWallet.Text.Trim();
            CalculatePoolAccount();
            ShowWalletmessage(m_CurrentPool);

        }

        private void txtPoolAccount_TextChanged(object sender, EventArgs e)
        {
            PoolAccount = txtPoolAccount.Text.Trim();

        }

        private void btnFillDefaultAddress_Click(object sender, EventArgs e)
        {
            txtWallet.Text=m_selected_coin.DefaultAddress;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            Password = txtPassword.Text;//trim not needed for password

        }
    }
}
