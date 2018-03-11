using DigibyteMiner.Coins;
using DigibyteMiner.Core;
using DigibyteMiner.Core.Interfaces;
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
    public partial class ProfitabilitySummary : Form
    {
        DateTime lastTime = DateTime.Now;
        public ProfitabilitySummary()
        {
            InitializeComponent();
        }

        private void ProfitabilitySummary_Load(object sender, EventArgs e)
        {

            Factory.Instance.ViewObject.RegisterForTimer(UpdateView);


        }
        public void ShowWarning(string str)
        {
            if(str=="")
            {
                lblWarning.Visible = true;
            }
        }
        public void UpdateView()
        {
            try
            {
                int TimetoWait = 30;
                TimeSpan time = DateTime.Now - lastTime;
                if (time.TotalSeconds < TimetoWait)
                    return;
                IMiner miner = Factory.Instance.CoreObject.SelectedMiner;
                ShowWarning(miner.HashRate);
                ProfitInfo profits = ((MinerBase)miner).CalculateProfitability();
                if (profits != null)
                {
                    lblDolDaily.Text = profits.revenue;
                    lblDigDaily.Text = profits.estimated_rewards;

                    float dailyDol = float.Parse(profits.revenue.Substring(1));
                    float dailyDig = float.Parse(profits.estimated_rewards);

                    lblDolWeekly.Text = (dailyDol * 7).ToString("F2");
                    lblDolMonth.Text = (dailyDol * 30).ToString("F2");
                    lblDolYear.Text = (dailyDol * 365).ToString("F2");

                    lblDgbWeekly.Text = (dailyDig * 7).ToString("F2");
                    lblDgbMonthly.Text = (dailyDig * 30).ToString("F2");
                    lblDgbYear.Text = (dailyDig * 365).ToString("F2");




                }
                lastTime = DateTime.Now;
            }
            catch (Exception e)
            {
            }
     

        }
    }
}
