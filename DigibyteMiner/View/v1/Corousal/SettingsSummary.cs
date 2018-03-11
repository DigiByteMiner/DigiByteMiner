using DigibyteMiner.Core;
using DigibyteMiner.Core.Interfaces;
using DigibyteMiner.Model.Config;
using DigibyteMiner.View.v1.ExtraScreens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DigibyteMiner.View.v1.Corousal
{
    public partial class SettingsSummary : Form
    {
        public Form Creator { get; set; }

        public SettingsSummary()
        {
            InitializeComponent();
        }

        private void SettingsSummary_Load(object sender, EventArgs e)
        {
            UpdateSettingsView();
            Factory.Instance.ViewObject.RegisterForTimer(t_Tick);
            
        }
        void t_Tick()
        {
            try
            {
                UpdateTime();
            }
            catch (Exception )
            {
            }
        }
        public void UpdateTime()
        {
            TimeSpan time = DateTime.Now - Factory.Instance.StartTime;
            //lblRunningTime.Text = time.ToString(@"dd\:hh\:mm");
            string timeStr;
            if (time.Days > 0)
                timeStr = string.Format("{0:00} Day: {0:00} :{1:00}", time.Days, time.TotalHours, time.Minutes);
            else
                timeStr = string.Format("{0:00}:{1:00}", time.TotalHours, time.Minutes);

            lblRunningTime.Text = timeStr;
        }
        public void UpdateSettingsView()
        {
            try
            {
                Config model = Factory.Instance.Model;
                IMiner miner = Factory.Instance.CoreObject.SelectedMiner;
                if (miner != null)
                {
                    lblActiveMiner.Text = miner.Name;
                }
                LoadData();

                UpdateTime();
            }
            catch (Exception e)
            {
            }
        }
        public void SetCheckBoxData(Label chkBox, bool state)
        {
            if (state == true)
            {
                chkBox.Text = "Yes";
            }
            else
            {
                chkBox.Text = "No";
            }
        }
        public void LoadData()
        {
            DB data = Factory.Instance.Model.Data;
            if (data != null)
            {
                SetCheckBoxData(lblMineOnStartup, data.Option.Startup);
                SetCheckBoxData(lblMineonLaunch, data.Option.MineOnStartup);
                SetCheckBoxData(lblShowMinerUi, data.Option.ShowMinerWindows);

            }

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            ((MainForm)Creator).Rotateimage();

        }
    }
}
