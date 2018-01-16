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
    public partial class AddItem : Form
    {
        public AddItem()
        {
            InitializeComponent();
        }
        public string txt = string.Empty;
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (txt.Length == 0)
            {
                textBox1.ResetText();
                textBox1.ForeColor = SystemColors.WindowText;
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if ((txt = textBox1.Text.Length > 0 ? textBox1.Text : string.Empty).Length == 0)
            {
                textBox1.Text = "Add title...";
                textBox1.ForeColor = SystemColors.InactiveBorder;
            }
        }
        private void button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string GetText()
        {
            textBox1.Text = "Add title...";
            textBox1.ForeColor = SystemColors.InactiveBorder;
            string tmp = txt;
            txt = string.Empty;
            return tmp;
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                button_Click(sender, null);
        }
    }
}