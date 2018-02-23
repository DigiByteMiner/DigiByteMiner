using OneMiner.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OneMiner.View.v1.MiningInfo
{
    public partial class MinerInfoScript : Form,IMinerInfoTab
    {
        public IMiner Miner { get; set; }
        MinerInfo m_Parent = null;
        Hashtable m_ButtonToMiner = new Hashtable();
        Hashtable m_CheckBoxToMiner = new Hashtable();
        Button m_currentButton = null;
        List<Button> m_tabButtons = new List<Button>();


        public MinerInfoScript(IMiner miner, MinerInfo parent)
        {
            Miner = miner;
            m_Parent = parent;
            InitializeComponent();
        }

        private void MinerInfoScript_Load(object sender, EventArgs e)
        {
            List<IMinerProgram> programs = Miner.MinerPrograms;
            Button leftbutton = btnTemplate;
            int i=0;
            foreach (IMinerProgram item in programs)
            {
                Button btn = new Button();
                btn.Top = 10;
                btn.Name = "button"+i.ToString();
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
            AddCheckboxes();

        }
        
        public void AddCheckboxes()
        {
            CheckBox leftCheckBox = chkTemplate;
            int i = 0;
            foreach (IMinerProgram item in Miner.MinerPrograms)
            {
                CheckBox chkBox = new CheckBox();
                chkBox.Top = 10;
                chkBox.Name = "chkBox" + i.ToString();
                chkBox.Click += chkBox_Click;
                chkBox.AutoSize = true;
                chkBox.Text = item.Type;
                chkBox.Left = leftCheckBox.Left + leftCheckBox.Width;
                chkBox.Checked = item.Enabled;
                //chkBox.FlatStyle = FlatStyle.Popup;
                //chkBox.BackColor = Color.LightSteelBlue;
                //chkBox.ForeColor = Color.Black;

                leftCheckBox = chkBox;
                this.Controls.Add(chkBox);
                i++;
                m_CheckBoxToMiner.Add(chkBox.Name, item);
            }
        }

        void chkBox_Click(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;
                if (chk != null)
                {
                    IMinerProgram prog = m_CheckBoxToMiner[chk.Name] as IMinerProgram;
                    if(prog!=null)
                    {
                        Miner.ChangeGPUType(prog);
                    }
                }
            }
            catch (Exception ee)
            {
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SelectView(btn);
            m_currentButton = btn;
            EditingInProgress = false;//if this was not set then ui would show previous tab script
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
        public void UpdateUI()
        {
            if (EditingInProgress)
                return;
            if (m_currentButton != null)
            {
                IMinerProgram prog = m_ButtonToMiner[m_currentButton.Name] as IMinerProgram;
                string script = prog.Script;
                txtScriptArea.Text = script;
                DisableEdit();

            }
        }
        public bool EditingInProgress { get; set; }
        private void DisableEdit()
        {
            EditingInProgress = false;
            txtScriptArea.ReadOnly = true;
            lnkEdit.Enabled = true;
            btnSave.Enabled = false;
        }
        private void Enabledit()
        {
            EditingInProgress = true;
            txtScriptArea.ReadOnly = false;
            lnkEdit.Enabled = false;
            btnSave.Enabled = true;
        }
        private void lnkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Enabledit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DisableEdit();
            if (m_currentButton != null)
            {
                IMinerProgram prog = m_ButtonToMiner[m_currentButton.Name] as IMinerProgram;
                prog.ModifyScript(txtScriptArea.Text);

            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            if (m_currentButton != null)
            {
                IMinerProgram prog = m_ButtonToMiner[m_currentButton.Name] as IMinerProgram;
                string script = prog.GenerateScript();
                txtScriptArea.Text = script;
                DisableEdit();

            }
        }


    }
}
