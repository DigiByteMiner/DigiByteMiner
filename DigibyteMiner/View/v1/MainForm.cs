using DigibyteMiner.Core;
using DigibyteMiner.Core.Interfaces;
using DigibyteMiner.Model.Config;
using DigibyteMiner.View;
using DigibyteMiner.View.v1;
using DigibyteMiner.View.v1.Corousal;
using DigibyteMiner.View.v1.ExtraScreens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DigibyteMiner
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        List<Form> m_Corousals = new List<Form>();
        int m_CurrentCarousal = 0;
        DateTime m_LastCarousalTurn = DateTime.Now;
        private const int CAROUSAL_WAIT=60;
        public MinerView MinerView { get; set; }//the selected minerview. not the activated one. this is the one which was clicked. 
        public MinerInfo MinerInfo { get; set; }//the selected minerInfo. not the activated one. this is the one which was clicked. We need this object to show the logs etc
        WebBrowserEx m_DownloaderBrowser = new WebBrowserEx();

        public WebBrowserEx DownloadBrowser
        {
            get
            {
                return m_DownloaderBrowser;
            }
        }

        private void btnAddMiner_Click(object sender, EventArgs e)
        {
            //Todo permission from core to add miner
            AddMinerContainer addMiner = new AddMinerContainer();
            addMiner.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            RunCarousal();
            this.FormClosing += MainForm_FormClosing;
            oneMinerNotifyIcon.ContextMenuStrip = taskbarMenu;
            Factory.Instance.ViewObject.RegisterForTimer(UpdateMinerInfo);
            Factory.Instance.ViewObject.RegisterForTimer(t_Tick);
            Factory.Instance.ViewObject.RegisterForTimer(UpDateMinerState);

        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
        SettingsSummary m_SettingsSummary = new SettingsSummary();
        ProfitabilitySummary m_ProfitabilitySummary = new ProfitabilitySummary();
        public void ShowSettingsCarausal()
        {
            m_SettingsSummary.UpdateSettingsView();
            BringToView(m_SettingsSummary);
        }

        public void RunCarousal()
        {
            m_Corousals.Add(m_SettingsSummary);
            m_Corousals.Add(m_ProfitabilitySummary);

            Form next = m_Corousals.ElementAt<Form>(m_CurrentCarousal);
            BringToView(next);


        }

        public void UpdateMinerInfo()
        {
            if(MinerInfo!=null)
            {
                MinerInfo.UpdateUI();
            }
        }
        public void UpDateMinerState()
        {
            try
            {
                if(this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(UpDateMinerState),
                                          new object[] {  });
                }
                else
                {
                    foreach (Control item in pnlMainInfo.Controls)
                    {
                        Home home = item as Home;
                        if (home != null)
                        {
                            home.UpdateState();
                        }
                        //Todo: this seems to be a duplicate call as timer invokes this separately. analyze
                        //MinerInfo.UpdateState();
                    }
                }
                
            }
            catch (Exception e)
            {
                Logger.Instance.LogError("Error while updating minerstate" + e.Message);
            }
         
        }

        public void RemoveFromView(Form previous)
        {
            /*
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(600);
                previous.Opacity -= .1;
                previous.Refresh();

            }
             */
        }
        public void BringToView(Form next)
        {
            next.TopLevel = false;
            pnlCarousal.Controls.Clear();
            pnlCarousal.Controls.Add(next);
            next.Dock = DockStyle.Fill;
            //next.Opacity = .5;//opacity doesnt work for embedded forms
            next.Show();
            /*
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(600);
                next.Opacity += .1;
                next.Refresh();
            }
             */
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 3000)
            {
                showToolStripMenuItem_Click(null, new EventArgs());
            }

            base.WndProc(ref m);
        }
        void t_Tick()
        {
            return;//Enable this after profitability is implemented
            TimeSpan elapsedTime = DateTime.Now - m_LastCarousalTurn;
            if (elapsedTime.Seconds < CAROUSAL_WAIT)
                return;
            m_LastCarousalTurn = DateTime.Now;
            Form previous = m_Corousals.ElementAt<Form>(m_CurrentCarousal);
            m_CurrentCarousal++;
            if (m_CurrentCarousal >= m_Corousals.Count)
                m_CurrentCarousal = 0;

            Form next = m_Corousals.ElementAt<Form>(m_CurrentCarousal);

            RemoveFromView(previous);
            BringToView(next);
        }

        private Panel ClonePanel(Panel p)
        {
            return null;
        }
        public void SelectMiningView(IMiner miner)
        {
            ShowMiningInfo(miner);

        }
        /*
        public void ChangeMiningView(MinerView view)
        {
            foreach (MinerView item in pnlMiner.Controls)
            {
                if (item == view)
                {
                    SelectMiningView(view);
                }
                else
                    item.DeSelectView();
            }
        }
        public void ActivateMiningView(MinerView view)
        {
            foreach (MinerView item in pnlMiner.Controls)
            {
                if (item == view)
                    item.ActivateView();
                else
                    item.DeActivateView();
            }
        }
         *         */
        public void UpdateMinerList()
        {
            IMiner miner = null;
            miner = Factory.Instance.CoreObject.SelectedMiner;

            Home view = new Home(miner,this);
            view.TopLevel = false;
            pnlMainInfo.Controls.Add(view);
            view.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            view.UpdateState();
            view.Show();


            SelectMiningView(Factory.Instance.CoreObject.SelectedMiner);

            ShowSettingsCarausal();
        }


        public void ShowMiningInfo(IMiner miner)
        {
            MinerInfo info = new MinerInfo(miner, this);
            info.TopLevel = false;
            pnlMinerInfo.Controls.Clear();
            pnlMinerInfo.Controls.Add(info);
            info.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            info.Dock = DockStyle.Fill;
            this.MinerInfo = info;
            info.UpdateState();

            info.Show();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About abt = new About();
            abt.ShowDialog();
        }

        private void donateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void pnlMinerInfo_Paint(object sender, PaintEventArgs e)
        {
        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Factory.Instance.CoreObject.CloseApp();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }


        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        public void ShowHardwareMissingError()
        {
            MessageBox.Show("System could not detect any Nvidia or AMD graphics card in your machine! If this is by mistake, please select correct miners from Script tab ",
                "Hardware Missing");
        }

        private void modeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        



    }
}
