using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigibyteMiner.View.v1.Controls
{
    public partial class SliderEx : UserControl
    {
        public SliderEx()
        {
            InitializeComponent();
        }

        private void SliderEx_Load(object sender, EventArgs e)
        {
            //var textbox = new TextBox();
            pbcaret.Location = new Point(pbLine.Location.X, pbLine.Location.Y-pbcaret.Height/2);
            pbcaret.MouseDown += new MouseEventHandler(textbox_MouseDown);
            pbcaret.MouseMove += new MouseEventHandler(textbox_MouseMove);
            pbcaret.MouseUp += new MouseEventHandler(textbox_MouseUp);

            //this.Controls.Add(textbox);
        }
        private Control activeControl;
        private Point previousLocation;

        private void button1_Click(object sender, EventArgs e)
        {
     
        }

        void textbox_MouseDown(object sender, MouseEventArgs e)
        {
            activeControl = sender as Control;
            previousLocation = e.Location;
            Cursor = Cursors.Hand;
        }

        void textbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (activeControl == null || activeControl != sender)
                return;
            /*
            var location = activeControl.Location;
           // location.Offset(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
            int actual_x = pbcaret.Location.X +e.Location.X;
            lblValue.Text = actual_x.ToString();

            if (pbcaret.Location.X < pbLine.Location.X ||
                actual_x > (pbLine.Location.X + pbLine.Width))
                return;

            location.Offset(e.Location.X - previousLocation.X, 0);
            activeControl.Location = location;
             */
            var location = activeControl.Location;
            location.X = pbcaret.Location.X + e.X - previousLocation.X;
            if ((location.X < pbLine.Location.X)
                || ((location.X+pbcaret.Width)> (pbLine.Location.X+pbLine.Width)))
            {
                return;
            }
            pbcaret.Location = location;


        }

        void textbox_MouseUp(object sender, MouseEventArgs e)
        {
            activeControl = null;
            Cursor = Cursors.Default;
        }
    }
}
