using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WoWLuncher
{
    public partial class Form1 : Form
    {
        Form2 form2 = new Form2();
        public Form1()
        {
            InitializeComponent();
            treeView1.NodeMouseDoubleClick += Node_MouseDoubleClick;
            ImageList il = new ImageList();
            il.Images.Add(Properties.Resources.official_wow_icon_by_benashvili_d7sd1ab);
            treeView1.ImageList = il;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            Core.LoadTreeViewNodes(treeView1);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                StartBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                deleteBtn.PerformClick();
            }
            else if(e.Modifiers == Keys.Control)
            {
                if(e.KeyCode  == Keys.E)
                {
                    editBtn.PerformClick();
                }
                else if(e.KeyCode == Keys.A)
                {
                    addBtn.PerformClick();
                }
            }
            e.Handled = false;
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            form2.LoadSettings(null);
            form2.ShowDialog();
            Core.LoadTreeViewNodes(treeView1);
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if(treeView1.SelectedNode == null)
            {
                MessageBox.Show("There is no selected server!");
                return;
            }
            form2.LoadSettings(treeView1.SelectedNode);
            form2.ShowDialog();
            Core.LoadTreeViewNodes(treeView1);
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("There is no selected server!");
                return;
            }
            Core.DeleteSettings(treeView1.SelectedNode);
            Core.LoadTreeViewNodes(treeView1);
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("There is no selected server!");
                return;
            }
            Core.StartGame(treeView1.SelectedNode);
        }
        private void Node_MouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Core.StartGame(treeView1.SelectedNode);
        }
    }
}
