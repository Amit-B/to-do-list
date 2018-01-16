using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace ToDoList
{
    public partial class EditItems : Form
    {
        public EditItems()
        {
            InitializeComponent();
        }
        private List<object[]> nodes = new List<object[]>();
        private DataItem cur = null;
        private int curIndex = -1;
        private void EditItems_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Global.DATA_ITEMS.Count; i++)
            {
                if (Global.DATA_ITEMS[i].status == DataItem.ItemStatus.Active)
                    nodes.Add(new object[] { treeView1.Nodes[0].Nodes.Add(Global.DATA_ITEMS[i].title), Global.DATA_ITEMS[i] });
                else
                    nodes.Add(new object[] { treeView1.Nodes[1].Nodes[Global.DATA_ITEMS[i].status == DataItem.ItemStatus.Canceled ? 0 : 1].Nodes.Add(Global.DATA_ITEMS[i].title), Global.DATA_ITEMS[i] });
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("By pressing \"Yes\", all the items will be permamently deleted.\nAre you sure you want to do that?", "Clear list", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Global.DATA_ITEMS.Clear();
                nodes.Clear();
                while (treeView1.Nodes[0].Nodes.Count > 0)
                    treeView1.Nodes[0].Nodes.RemoveAt(0);
                while (treeView1.Nodes[1].Nodes[0].Nodes.Count > 0)
                    treeView1.Nodes[1].Nodes[0].Nodes.RemoveAt(0);
                while (treeView1.Nodes[1].Nodes[1].Nodes.Count > 0)
                    treeView1.Nodes[1].Nodes[1].Nodes.RemoveAt(0);
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                cur = null;
                for (int i = 0; i < nodes.Count; i++)
                    if (nodes[i][0] as TreeNode == e.Node)
                    {
                        curIndex = i;
                        cur = nodes[i][1] as DataItem;
                    }
                RefreshCurrent();
            }
            catch
            {
            }
        }
        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && cur != null)
            {
                int statusID = Convert.ToInt16(cur.status);
                for (int i = 0; i < setStatusToolStripMenuItem.DropDownItems.Count; i++)
                    setStatusToolStripMenuItem.DropDownItems[i].Enabled = (statusID != 0);
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            }
        }
        private void activeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode moveTo = null;
            switch ((sender as ToolStripMenuItem).Text)
            {
                case "Active": moveTo = treeView1.Nodes[0]; break;
                case "Completed": moveTo = treeView1.Nodes[1].Nodes[0]; break;
                case "Canceled": moveTo = treeView1.Nodes[1].Nodes[1]; break;
            }
            if(moveTo != null)
            {
                (nodes[curIndex][0] as TreeNode).Remove();
                nodes[curIndex][0] = moveTo.Nodes.Add(cur.title);
            }
            cur = null;
            curIndex = -1;
            RefreshCurrent();
        }
        private void renameItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text.Length > 0)
                (nodes[curIndex][0] as TreeNode).Text = cur.title = toolStripTextBox1.Text;
            RefreshCurrent();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (nodes[curIndex][0] as TreeNode).Remove();
            nodes.RemoveAt(curIndex);
            Global.DATA_ITEMS.Remove(cur);
            curIndex = -1;
            cur = null;
            RefreshCurrent();
        }
        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                deleteToolStripMenuItem_Click(sender, null);
        }
        private void RefreshCurrent()
        {
            if (cur == null)
            {
                Label[] l = { label3, label5, label7, label9 };
                for (int i = 0; i < l.Length; i++)
                    l[i].ResetText();
            }
            else
            {
                label3.Text = cur.id.ToString();
                label5.Text = cur.title;
                label7.Text = cur.created.ToShortDateString();
                label9.Text = cur.status.ToString();
            }
        }
    }
}