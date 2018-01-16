using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace ToDoList
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }
        private int startY = 0, rgba = 0;
        private bool rgbaDirect = false;
        private void About_Load(object sender, EventArgs e)
        {
            label1.Text =
@"To Do List
v" + Global.VERSION +
@"


Another app used to remind you

what you need to do... :-)



Developed using C# by

Amit `Amit_B` Barami



Icon and image sources:

www.iconarchive.com";
            label1.Location = new Point(label1.Location.X, startY += Size.Height + (3 * label1.Text.Split('\n').Length));
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label1.Location.Y == (startY * -1)) label1.Location = new Point(label1.Location.X, startY);
            else label1.Location = new Point(label1.Location.X, label1.Location.Y - 1);
            rgba += rgbaDirect ? (-1) : (1);
            if (rgba == (rgbaDirect ? 0 : 255))
                rgbaDirect = !rgbaDirect;
            label1.ForeColor = Color.FromArgb(255, 255 - rgba, 255 - rgba, 255 - rgba);
            this.BackColor = Color.FromArgb(255, rgba, rgba, rgba);
        }
        private void About_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Close();
        }
        private void About_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}