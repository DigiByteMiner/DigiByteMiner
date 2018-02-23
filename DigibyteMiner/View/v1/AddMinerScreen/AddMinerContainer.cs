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
    public partial class AddMinerContainer : Form, IMinerContainer
    {
        private int m_currentState = 0;
        public AddMiner AddMiner { get; set; }
        public AddDualMiner AddDualMiner { get; set; }

        AddMinerFinish m_finishScreen = null;

        private ICoin m_selected_coin = null;
        private ICoin m_selected_dual_coin = null;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public ICoin SelectedCoin
        {
            get
            {
                return m_selected_coin;
            }
            set
            {
                m_selected_coin = value;
            }
        }
        public ICoin SelectedDualCoin
        {
            get
            {
                return m_selected_dual_coin;
            }
            set
            {
                m_selected_dual_coin = value;
            }
        }

        public string Minername { get; set; }
        public string MainCoinPool { get; set; }
        public string MainCoinWallet { get; set; }
        public string DualCoinPool { get; set; }
        public string DualCoinWallet { get; set; }




        public  bool BAddDualMiner {get;set;}
        public AddMinerContainer()
        {
            AddMiner = new AddMiner(this);
            AddDualMiner = new AddDualMiner(this);
            m_finishScreen = new AddMinerFinish(this);
            InitializeComponent();
            BAddDualMiner = false;
        }
        public void EnableNextButton()
        {
            btnNext.Enabled = true;
        }

        public string GetMinername()
        {
            return AddMiner.Minername;
        }

        public void DisableNextButton()
        {
            btnNext.Enabled = false;
        }
        public void EnablePreviousButton()
        {
            btnPrevious.Enabled = true;
        }
        public void DisablePreviousButton()
        {
            btnPrevious.Enabled = false;
        }
        public void EnableDualMinerButton()
        {
            btnAddDualMiner.Enabled = true;
            btnAddDualMiner.Visible = true;

        }
        public void DisableDualMinerButton()
        {
            btnAddDualMiner.Enabled = false;
        }
        public void HideDualMinerButton()
        {
            btnAddDualMiner.Visible = false;
        }
        public void ReverseNextFinish(bool val)
        {
            if (val)
            {
                btnNext.Visible = false;
                btnFinish.Visible = true;
            }
            else
            {
                btnNext.Visible = true;
                btnFinish.Visible = false;
            }

        }
        public void EnableFinishButton()
        {
            btnFinish.Enabled = true;
        }
        public void DisableFinishButton()
        {
            btnFinish.Enabled = false;
        }

        public void MakeSelectedCoin(ICoin coin)
        {
            m_selected_coin = coin;
        }
        public void MakeSelectedDualCoin(ICoin coin)
        {
            m_selected_dual_coin = coin;
        }
        private void ChangeUIState()
        {
            switch (m_currentState)
            {
                case 0:
                    DisablePreviousButton();
                    //EnableNextButton();
                    HideDualMinerButton();

                    ReverseNextFinish(false);

                    break;
                case 1:
                    EnablePreviousButton();
                    EnableNextButton();
                    if(m_selected_coin.Algorithm.SupportsDualMining)
                        EnableDualMinerButton();
                    ReverseNextFinish(false);
                    break;

                case 2://Dual miner selection screen
                    EnablePreviousButton();
                    EnableNextButton();
                    HideDualMinerButton();
                    ReverseNextFinish(false);
                    break;

                case 3://Dual miner settings screen
                    EnablePreviousButton();
                    EnableNextButton();
                    HideDualMinerButton();
                    ReverseNextFinish(false);
                    break;
                case 4://Finish screen
                    EnablePreviousButton();
                    ReverseNextFinish(true);
                    HideDualMinerButton();
                    break;


            }
        }
        public void NextStage()
        {
            switch (m_currentState)
            {
                case 0:
                    m_currentState++;
                    break;
                case 1:
                    if (BAddDualMiner)
                        m_currentState++;
                    else
                        m_currentState += 3;//to finish. skip dualminerselection and dual miner configuraton
                    break;
                case 2://Dual miner selection screen
                case 3://Dual miner settings screen
                case 4://Finish screen
                    m_currentState++;
                    break;


            }
            ShowStage();
            ChangeUIState();
            //EnablePreviousButton();

        }
        public void PreviousStage()
        {
            switch (m_currentState)
            {
                case 0:
                case 1:
                case 2://Dual miner selection screen
                case 3://Dual miner settings screen
                    m_currentState--;
                    break;
                case 4://Finish screen
                    if (BAddDualMiner)
                        m_currentState--;
                    else
                        m_currentState -= 3;
                    break;


            }
            ShowStage();
            ChangeUIState();

        }
        public void ShowStage()
        {
            Form objForm = null;
            switch (m_currentState)
            {
                case 0:
                    objForm = AddMiner;
                    break;
                case 1:

                    if (m_selected_coin != null)
                    {
                        ICoinConfigurer form = m_selected_coin.SettingsScreen;
                        form.AssignParent(this);
                        objForm = form as Form;
                    }
                    break;
                case 2://Dual miner selection screen
                    AddDualMiner.SelectedCoin = m_selected_coin;
                    AddDualMiner.Init();
                    objForm = AddDualMiner;
                    break;
                case 3://Dual miner settings screen
                    if (m_selected_dual_coin != null)
                    {
                        ICoinConfigurer form = m_selected_dual_coin.SettingsScreen;
                        form.AssignParent(this);
                        objForm = form as Form;
                    }
                    break;
                case 4://Finish screen
                    //m_finishScreen.SelectedCoin = m_selected_coin;
                    //m_finishScreen.SelectedDualCoin = m_selected_dual_coin;
                    m_finishScreen.UpdateUI();
                    objForm = m_finishScreen;
                    break;


            }
            if (objForm != null)
            {
                objForm.TopLevel = false;
                pnlForm.Controls.Clear();
                pnlForm.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //if there is any final things to be performed in this stage do it
            FinalAction();
            NextStage();
        }
        private void FinalAction()
        {
        }

        private void AddMinerContainer_Load(object sender, EventArgs e)
        {
            ShowStage();
            ChangeUIState();
            this.CenterToScreen();

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            PreviousStage();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddDualMiner_Click(object sender, EventArgs e)
        {
            BAddDualMiner = true;
            NextStage();
        }
        private bool Verify()
        {
            return true;
        }
        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (Verify())
            {
                this.Close();
                bool dualMining = false;
                if (m_selected_dual_coin != null)
                    dualMining = true;
                IMiner miner = m_selected_coin.Algorithm.CreateMiner(m_selected_coin, dualMining, m_selected_dual_coin, AddMiner.Minername);

                Factory.Instance.CoreObject.AddMiner(miner, true);

            }
        }
    }
}
