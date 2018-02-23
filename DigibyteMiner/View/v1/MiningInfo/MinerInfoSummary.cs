using OneMiner.Core;
using OneMiner.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OneMiner.View.v1.MiningInfo
{
    public partial class MinerInfoSummary : Form,IMinerInfoTab
    {
        public IMiner Miner { get; set; }
        MinerInfo m_Parent = null;
        public MinerInfoSummary(IMiner miner, MinerInfo parent)
        {
            Miner = miner;
            m_Parent = parent;
            InitializeComponent();
        }

        private void MinerInfoSummary_Load(object sender, EventArgs e)
        {

        }
        public void UpdateUIStatic()
        {
            try
            {
                List<GpuData> gpus = Miner.GetGpuList();
                pnlGpus.Controls.Clear();
                lblGpuInfoStatic.Visible = true;

                foreach (GpuData gpuData in gpus)
                {
                    GpuView gpu = new GpuView(gpuData, this);
                    gpu.TopLevel = false;
                    pnlGpus.Controls.Add(gpu);
                    gpu.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    gpu.UpdateState(false);
                    gpu.Show();
                }
            }
            catch (Exception e)
            {
            }
        }
        public void UpdateUI()
        {
            try 
	        {
                if(Miner.MinerState==MinerProgramState.Running)
                {
                    List<IMinerProgram> miners = Miner.ActualMinerPrograms;
                    while (pnlGpus.Controls.Count > 0)
                    {
                        Control oKill = pnlGpus.Controls[0];
                        pnlGpus.Controls.RemoveAt(0);
                        if (oKill != null)
                            oKill.Dispose();
                    }  
                    lblGpuInfoStatic.Visible = false;

                    foreach (IMinerProgram item in miners)
                    {
                        MinerDataResult result = item.OutputReader.MinerResult;
                        if (result == null)
                            continue;
                        foreach (GpuData gpuData in result.GPUs)
                        {
                            GpuView gpu = new GpuView(gpuData, this);
                            gpu.TopLevel = false;
                            pnlGpus.Controls.Add(gpu);
                            gpu.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                            gpu.UpdateState(true);
                            gpu.Show();
                        }
                    }
                }
                else
                {
                    UpdateUIStatic();
                }
   
	        }
	        catch (Exception e)
	        {
                Logger.Instance.LogError(e.Message);
	        }
        }
    }
}
