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
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }
        private void Setting_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = Screen.PrimaryScreen.Bounds.Width;
            numericUpDown2.Maximum = Screen.PrimaryScreen.Bounds.Height;
            /*int findTheMax = 1;
            while (66 + (14 * findTheMax) < Screen.PrimaryScreen.Bounds.Height)
                findTheMax++;*/
            numericUpDown3.Maximum = (Screen.PrimaryScreen.Bounds.Height - 66) / 14;
            numericUpDown1.Value = Global.X;
            numericUpDown2.Value = Global.Y;
            numericUpDown3.Value = Global.MAIN_FORM.panel1.Size.Height / 14;
        }
        private void button1_Click(object sender, EventArgs e)
        {   // save & quit
            Global.X = (int)numericUpDown1.Value;
            Global.Y = (int)numericUpDown2.Value;
            Global.MAIN_FORM.panel1.Size = new Size(Global.MAIN_FORM.panel1.Size.Width, 14 * (Global.COUNT_OF_LINES = (int)numericUpDown3.Value));
            Global.MAIN_FORM.Size = new Size(Global.MAIN_FORM.panel1.Size.Width, 14 * (Global.COUNT_OF_LINES = (int)numericUpDown3.Value) + 66);
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {   // cancel
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = Global.DEFAULT_POSITION.X;
            numericUpDown2.Value = Global.DEFAULT_POSITION.Y;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            numericUpDown3.Value = 15;
        }
    }
}