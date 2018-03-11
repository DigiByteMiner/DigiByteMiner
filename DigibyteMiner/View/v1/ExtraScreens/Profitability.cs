using DigibyteMiner.View.v1.Corousal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigibyteMiner.View.v1.ExtraScreens
{
    public partial class Profitability : Form
    {
        public Profitability()
        {
            InitializeComponent();
        }

        private void Profitability_Load(object sender, EventArgs e)
        {
            ProfitabilitySummary sum = new ProfitabilitySummary();
            sum.TopLevel = false;
            this.Controls.Add(sum);
            sum.Show();
        }
    }
}
