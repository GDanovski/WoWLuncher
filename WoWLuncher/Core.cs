using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace WoWLuncher
{
    class Core
    {
        public static void LoadTreeViewNodes(TreeView tv)
        {
            Properties.Settings settings = Properties.Settings.Default;

            tv.Nodes.Clear();

            if (settings.ServerNames == null) return;

            for(int i = 0;i< settings.ServerNames.Count; i++)
            {
                TreeNode n = new TreeNode();
                n.Text = settings.ServerNames[i];
                n.Tag = settings.ServerAddres[i];
                tv.Nodes.Add(n);
            }

        }

        public static void DeleteSettings(TreeNode node)
        {
            int index = node.Index;
            Properties.Settings settings = Properties.Settings.Default;
            settings.ServerNames.RemoveAt(index);
            settings.ServerAddres.RemoveAt(index);
            settings.Save();
        }

        public static void AddSettings(TreeNode node)
        {
            Properties.Settings settings = Properties.Settings.Default;

            if (settings.ServerNames == null)
            {
                settings.ServerNames = new System.Collections.Specialized.StringCollection();
                settings.ServerAddres = new System.Collections.Specialized.StringCollection();
            }
            settings.ServerNames.Add(node.Text);
            settings.ServerAddres.Add((string)node.Tag);
            settings.Save();
        }

        public static void EditSettings(TreeNode node)
        {
            int index = node.Index;
            Properties.Settings settings = Properties.Settings.Default;
            settings.ServerNames[index] = node.Text;
            settings.ServerAddres[index] = (string)node.Tag;
            settings.Save();
        }

        public static void StartGame(TreeNode node)
        {
            string Dir = Application.StartupPath;
            string str = "set realmlist " + (string)node.Tag;

            try
            {
                StreamWriter file = new StreamWriter(Dir + "/realmlist.wtf", false);
                file.WriteLine(str);
                file.Close();

                file = new StreamWriter(Dir + "/Data/realmlist.wtf", false);
                file.WriteLine(str);
                file.Close();
                try
                {
                    file = new StreamWriter(Dir + "/Data/enUS/realmlist.wtf", false);
                }
                catch
                {
                    file = new StreamWriter(Dir + "/Data/enGB/realmlist.wtf", false);
                }
                file.WriteLine(str);
                file.Close();
                
                Process.Start(Dir + "/Wow.exe");
            }
            catch
            {
                MessageBox.Show("WoW is not found!");
            }
        }
    }
}
