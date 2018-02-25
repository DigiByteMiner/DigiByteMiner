using DigibyteMiner.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DigibyteMiner.View.v1.MiningInfo
{
    public partial class GpuView : Form
    {
        public GpuData GpuData { get; set; }
        MinerInfoSummary m_Parent = null;
        public GpuView(GpuData data, MinerInfoSummary parent)
        {
            GpuData = data;
            m_Parent = parent;
            InitializeComponent();
        }

        private void GpuData_Load(object sender, EventArgs e)
        {
            Size s = new Size();
            s.Width=pbCardType.Width+40;
            s.Height = 100;
            lblGpuname.MaximumSize = s;
        }
        public void UpdateState(bool showRunningData)
        {
            try
            {
                string hashrate = "H: ", shares = "T: ";
                if(GpuData!=null)
                {
                    if (GpuData.Make == CardMake.Nvidia)
                        pbCardType.Image = DigibyteMiner.Properties.Resources.nvidia;
                    else if (GpuData.Make == CardMake.Amd)
                        pbCardType.Image = DigibyteMiner.Properties.Resources.amd;
                    else if (GpuData.Make == CardMake.CPU)
                        pbCardType.Image = DigibyteMiner.Properties.Resources.cpu;
                    else
                        pbCardType.Image = DigibyteMiner.Properties.Resources.gpu;
                    double totalHashrate = double.Parse(GpuData.Hashrate);
                    if (totalHashrate > 1)
                    {
                        hashrate += totalHashrate.ToString("F1") + " MH/s";

                    }
                    else
                    {
                        double conversion = totalHashrate * 1024;
                        hashrate += conversion.ToString() + " KH/s";
                    }
                    if (showRunningData)
                    {
                        lblGpuhashrate.Text = hashrate;
                        lbltemp.Text = shares + GpuData.Temperature;
                        lblFanSpeed.Text = "F: " + GpuData.FanSpeed;
                    }
                    else
                    {
                        lblGpuhashrate.Text = "";
                        lbltemp.Text = "";
                        lblFanSpeed.Text = "";
                    }
                    lblGpuname.Text = GpuData.GPUName;
                }

            }
            catch (Exception e)
            {
            }
            
        }
    }
}
