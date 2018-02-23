using OneMiner.Core;
using OneMiner.Model.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OneMiner.View.v1.ExtraScreens
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            LoadData();


        }
        public void SetCheckBoxData(CheckBox chkBox, bool state)
        {
            if(state==true)
            {
                chkBox.Checked = true;
                chkBox.Text = "Yes";
            }
            else
            {
                chkBox.Checked = false;
                chkBox.Text = "No";
            }
        }
        public void LoadData()
        {
            DB data = Factory.Instance.Model.Data;
            if (data != null)
            {
                SetCheckBoxData(chkLaunchStartup, data.Option.Startup);
                SetCheckBoxData(chkMineLaunch, data.Option.MineOnStartup);
                SetCheckBoxData(chkShowMinerUI, data.Option.ShowMinerWindows);

            }

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

        private void chkLaunchStartup_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckBoxData(chkLaunchStartup, chkLaunchStartup.Checked);
            DB data = Factory.Instance.Model.Data;
            if (data != null)
            {
                data.Option.Startup = chkLaunchStartup.Checked;
                Factory.Instance.Model.Save();
                Factory.Instance.Model.SetLaunchOnStartup(chkLaunchStartup.Checked);
                Factory.Instance.ViewObject.UpdateSettingsCarousal();
                
            }
        }

        private void chkMineLaunch_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckBoxData(chkMineLaunch, chkMineLaunch.Checked);
            DB data = Factory.Instance.Model.Data;
            if (data != null)
            {
                data.Option.MineOnStartup = chkMineLaunch.Checked;
                Factory.Instance.Model.Save();
                Factory.Instance.ViewObject.UpdateSettingsCarousal();
            }
        }

        private void chkShowMinerUI_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckBoxData(chkShowMinerUI, chkShowMinerUI.Checked);
            DB data = Factory.Instance.Model.Data;
            if (data != null)
            {
                data.Option.ShowMinerWindows = chkShowMinerUI.Checked;
                Factory.Instance.Model.Save();
                Factory.Instance.ViewObject.UpdateSettingsCarousal();
            }
        }
    }
}
