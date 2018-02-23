using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneMiner.View.v1.AddMinerScreen
{
    public partial class DefaultMinerMessage : Form
    {
        public DefaultMinerAction MinerAction { get; set; }
        public DefaultMinerMessage()
        {
            InitializeComponent();
        }

        private void DefaultMinerMessage_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MinerAction = DefaultMinerAction.Cancel;
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            MinerAction = DefaultMinerAction.Edit;
            this.Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            MinerAction = DefaultMinerAction.Continue;
            this.Close();
        }
    }
    public enum DefaultMinerAction
    {
        Cancel=0,
        Edit,
        Continue,
        END
    }
}
