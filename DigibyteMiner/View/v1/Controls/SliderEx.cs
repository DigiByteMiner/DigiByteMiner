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
        int m_start = 0;
        int m_end = 0;
        public int Low { get; set; }
        public int High { get; set; }
        public int Value { get; set; }
        Control m_control = null;//slider

        public SliderEx()
        {
            Low = 0;
            High = 100;
            InitializeComponent();
        }
        void init()
        {
            float actual = Value - Low;
            float range = High - Low;
            float percent = actual / range;

            float distance = m_end - m_start;
            m_control.Location = new Point((int)(pbLine.Location.X+distance*percent), pbLine.Location.Y - m_control.Height / 2);
            lblValue.Text = Value.ToString();
            color(percent);

        }
        void color(float covered_percent)
        {
            if (covered_percent > 0.8)
            {
                m_control.BackColor = Color.Maroon;
            }
            else if (covered_percent > 0.4)
            {
                m_control.BackColor = Color.Gold;
            }
            else
            {
                m_control.BackColor = Color.SeaGreen;
            }
        }
        void calculate()
        {
            float distance = m_end - m_start;
            float covered = m_control.Location.X - m_start;
            float covered_percent = covered / distance;
            //Value = Low + (int)((High - Low) * covered_percent);
            Value = Low + (int)((High - Low-Low) * covered_percent);
            if (Value < Low)
                Value = Low;
            if (Value > High)
                Value = High;
            lblValue.Text = Value.ToString();
            color(covered_percent);
   
        }
        private void SliderEx_Load(object sender, EventArgs e)
        {

            this.Paint += SliderEx_Paint;
            m_control = button1;
            //var textbox = new TextBox();
            m_control.Location = new Point(pbLine.Location.X, pbLine.Location.Y - m_control.Height / 2);

            m_start = pbLine.Location.X;
            m_end = pbLine.Location.X + pbLine.Width - pbcaret.Width;

            m_control.MouseDown += new MouseEventHandler(textbox_MouseDown);
            m_control.MouseMove += new MouseEventHandler(textbox_MouseMove);
            m_control.MouseUp += new MouseEventHandler(textbox_MouseUp);
            init();

            //this.Controls.Add(textbox);
        }

        void SliderEx_Paint(object sender, PaintEventArgs e)
        {
            Pen redPen = new Pen(Color.Red, 1);

            // Draw line using float coordinates
            e.Graphics.DrawLine(redPen, pbLine.Location.X, pbLine.Location.Y, pbLine.Location.X+pbLine.Width, pbLine.Location.Y);
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
            var location = activeControl.Location;
            location.X = activeControl.Location.X + e.X - previousLocation.X;
            if ((location.X < pbLine.Location.X)
                || ((location.X + activeControl.Width) > (pbLine.Location.X + pbLine.Width)))
            {
                return;
            }
            activeControl.Location = location;
            calculate();

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
