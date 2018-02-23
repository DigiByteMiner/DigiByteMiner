using OneMiner.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OneMiner.View.v1.MiningInfo
{
    public partial class MinerInfoLogs : Form, IMinerInfoTab
    {
        public IMiner Miner { get; set; }
        MinerInfo m_Parent = null;
        Hashtable m_ButtonToMiner = new Hashtable();
        Button m_currentButton = null;
        List<Button> m_tabButtons = new List<Button>();

        public MinerInfoLogs(IMiner miner, MinerInfo parent)
        {
            Miner = miner;
            m_Parent = parent;
            InitializeComponent();

        }

        private void MinerInfoLogs_Load(object sender, EventArgs e)
        {
            List<IMinerProgram> programs = Miner.MinerPrograms;
            Button leftbutton = btnTemplate;
            int i = 0;
            foreach (IMinerProgram item in programs)
            {
                Button btn = new Button();
                btn.Top = 10;
                btn.Name = "button" + i.ToString();
                btn.Click += btn_Click;
                btn.Text = item.Type;
                btn.Left = leftbutton.Left + btnTemplate.Width;
                btn.FlatStyle = FlatStyle.Popup;
                btn.BackColor = Color.LightSteelBlue;
                btn.ForeColor = Color.Black;
                if (m_currentButton == null)
                    m_currentButton = btn;

                leftbutton = btn;
                this.Controls.Add(btn);
                m_tabButtons.Add(btn);
                i++;
                m_ButtonToMiner.Add(btn.Name, item);
            }
            m_currentButton.BackColor = Color.SteelBlue;
            m_currentButton.ForeColor = Color.White;
            UpdateUI();
        }
        public void SelectView(Button btn)
        {
            foreach (Button item in m_tabButtons)
            {
                if (btn == item)
                {
                    item.BackColor = Color.SteelBlue;
                    item.ForeColor = Color.White;
                }
                else
                {
                    item.BackColor = Color.LightSteelBlue;
                    item.ForeColor = Color.Black;
                }
            }
        }
        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SelectView(btn);
            m_currentButton = btn;
            UpdateUI();

        }



        public void UpdateUI()
        {
            try
            {
                if (m_currentButton != null)
                {
                    IMinerProgram prog = m_ButtonToMiner[m_currentButton.Name] as IMinerProgram;
                    string script = prog.OutputReader.NextLog;
                    logBrowser.DocumentText = script;

                }
            }
            catch (Exception e)
            {
            }
       
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (m_currentButton != null)
                {
                    IMinerProgram prog = m_ButtonToMiner[m_currentButton.Name] as IMinerProgram;
                    Process.Start(prog.STATS_LINK_HTML);

                }
            }
            catch (Exception se)
            {
            }
        }
    }
}
