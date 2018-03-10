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
            this.Paint += SliderEx_Paint;
            Control control = button1;
            //var textbox = new TextBox();
            control.Location = new Point(pbLine.Location.X, pbLine.Location.Y - control.Height / 2);
            control.MouseDown += new MouseEventHandler(textbox_MouseDown);
            control.MouseMove += new MouseEventHandler(textbox_MouseMove);
            control.MouseUp += new MouseEventHandler(textbox_MouseUp);

            //this.Controls.Add(textbox);
        }

        void SliderEx_Paint(object sender, PaintEventArgs e)
        {
            Pen redPen = new Pen(Color.Red, 1);

            // Draw line using float coordinates
            e.Graphics.DrawLine(redPen, pbLine.Location.X, pbLine.Location.Y, pbLine.Location.X+100, pbLine.Location.Y);
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
            location.X = activeControl.Location.X + e.X - previousLocation.X;
            if ((location.X < pbLine.Location.X)
                || ((location.X + activeControl.Width) > (pbLine.Location.X + pbLine.Width)))
            {
                return;
            }
            activeControl.Location = location;


        }

        void textbox_MouseUp(object sender, MouseEventArgs e)
        {
            activeControl = null;
            Cursor = Cursors.Default;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
