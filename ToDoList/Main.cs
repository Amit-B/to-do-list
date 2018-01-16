#define UNINSTALL_ON_CLOSE
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace ToDoList
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private const int offset = 25;
        private bool ws = true, movingForm = false;
        private void Main_Load(object sender, EventArgs e)
        {
            label1.Text += " v" + Global.VERSION;
            Global.MAIN_FORM = this;
            Global.DEFAULT_POSITION = new System.Drawing.Point(offset, (Screen.PrimaryScreen.Bounds.Height) - (this.Size.Height) - offset * 2);
            try
            {
                if (Directory.Exists(Global.PROGRAM_DIRECTORY))
                {
                    INI cfg = new INI(Global.FILE_CONFIG);
                    Global.X = Convert.ToInt32(cfg.GetKey("X"));
                    Global.Y = Convert.ToInt32(cfg.GetKey("Y"));
                    this.panel1.Size = new Size(Global.MAIN_FORM.panel1.Size.Width, 14 * (Global.COUNT_OF_LINES = Convert.ToInt32(cfg.GetKey("Lines"))));
                    string[] lines = File.ReadAllLines(Global.FILE_DATA), splited;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        splited = lines[i].Split(Global.DATA_LINE_SEPERATOR);
                        Global.DATA_ITEMS.Add(new DataItem(Convert.ToInt32(splited[0]), DataItem.GetStatusByID(Convert.ToInt32(splited[1])), Convert.ToDateTime(splited[2]), splited[3]));
                    }
                }
                else
                {
                    Directory.CreateDirectory(Global.PROGRAM_DIRECTORY);
                    File.Create(Global.FILE_DATA).Close();
                    File.WriteAllText(Global.FILE_CONFIG,
                        "X=" + (Global.X = Global.DEFAULT_POSITION.X) + "\n" +
                        "Y=" + (Global.Y = Global.DEFAULT_POSITION.Y) + "\n" +
                        "Lines=" + (Global.COUNT_OF_LINES = 15)
                        );
                }
                RefreshLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to initialize program:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
#if !UNINSTALL_ON_CLOSE
            using (StreamWriter sw = new StreamWriter(Global.FILE_DATA, false))
            {
                for (int i = 0; i < Global.DATA_ITEMS.Count; i++)
                    sw.WriteLine(
                        Global.DATA_ITEMS[i].id + Global.DATA_LINE_SEPERATOR +
                        Convert.ToInt32(Global.DATA_ITEMS[i].status) + Global.DATA_LINE_SEPERATOR +
                        Global.DATA_ITEMS[i].created.ToShortDateString() + Global.DATA_LINE_SEPERATOR +
                        Global.DATA_ITEMS[i].title
                        );
                sw.Close();
            }
#endif
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {   // minimize
            ToggleWindowStatus();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {   // about
            ShowDialog(Global.FORM_ABOUT);
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {   // setting
            ShowDialog(Global.FORM_SETTING);
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {   // edit
            contextMenuStrip2.Show(MousePosition.X, MousePosition.Y);
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {   // close
#if UNINSTALL_ON_CLOSE
            Directory.Delete(Global.PROGRAM_DIRECTORY, true);
#endif
            this.Close();
        }
        private void ToggleWindowStatus()
        {
            if (ws = !ws)
                this.Show();
            else
                this.Hide();
            notifyIcon1.Visible = !ws;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleWindowStatus();
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox6_Click(sender, e);
        }
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ToggleWindowStatus();
        }
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            movingForm = true;
        }
        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            movingForm = false;
            INI cfg = new INI(Global.FILE_CONFIG);
            cfg.SetKey("X", Global.X.ToString());
            cfg.SetKey("Y", Global.Y.ToString());
        }
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (movingForm)
            {
                Global.X = MousePosition.X - pictureBox2.Location.X - (pictureBox2.Size.Width/2);
                Global.Y = MousePosition.Y - this.Size.Height + (pictureBox2.Size.Height / 2);
            }
        }
        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {   // add item
            string t = (ShowDialog(Global.FORM_ADDITEM) as AddItem).GetText();
            if (t.Length > 0)
            {
                Global.DATA_ITEMS.Add(new DataItem(Global.DATA_ITEMS.Count, DataItem.ItemStatus.Active, DateTime.Now, t));
                RefreshLabel();
            }
        }
        private void editListToolStripMenuItem_Click(object sender, EventArgs e)
        {   // edit list
            ShowDialog(Global.FORM_EDITITEMS);
            RefreshLabel();
        }
        private Form ShowDialog(int i)
        {
            Global.REQUESTED_FORM = i;
            Form ret = Global.FORM;
            ret.ShowDialog();
            return ret;
        }
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (movingForm)
            {
                Global.X = MousePosition.X - label1.Location.X - (label1.Size.Width / 2);
                Global.Y = MousePosition.Y - (label1.Size.Height / 2);
            }
        }
        private void RefreshLabel()
        {
            string ret = string.Empty;
            for (int i = 0; i < Global.DATA_ITEMS.Count; i++)
                ret += (i == 0 ? "" : "\n") + Global.DATA_ITEMS[i].title;
            if (ret.Length == 0)
                ret = "No items. Click at the pen icon to add.";
            label2.Text = ret;
        }
    }
}