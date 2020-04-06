using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoWLuncher
{
    public partial class Form2 : Form
    {
        private TreeNode node = null;
        public Form2()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form2_KeyDown);
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Savebtn.PerformClick();
            }
            else if(e.KeyCode == Keys.Escape)
            {
                this.Hide();
            }
        }
        public void LoadSettings(TreeNode node)
        {
            this.node = node;
            if (node == null)
            {
                NameTB.Text = "";
                realmTB.Text = "";
            }
            else
            {
                NameTB.Text = node.Text;
                realmTB.Text = (string)node.Tag;
            }
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (node == null)
            {
                node = new TreeNode();
                node.Text = NameTB.Text;
                node.Tag = realmTB.Text;
                Core.AddSettings(node);
            }
            else
            {
                node.Text = NameTB.Text;
                node.Tag = realmTB.Text;
                Core.EditSettings(node);
            }
            this.Hide();
        }
    }
}
