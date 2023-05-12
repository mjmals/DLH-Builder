using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Styles;
using DLHApp.Model.DataStructImporters;
using DLHApp.Model.DataStructs;

namespace DLHWin.Editors.Dialogs
{
    internal class DataStructImporterDialog : Form
    {
        public DataStructImporterDialog(string targetPath = null)
        {
            Text = "Import DataStruct from Connection";
            Height = 800;
            Width = 1500;

            TargetPath = targetPath;

            Controls.Add(ImportPanel);
            ImportPanel.Controls.Add(DataStructPanel());

            Controls.Add(ToolBar);
            ToolBar.Items.Add(ConnectionLabel);
            ToolBar.Items.Add(ConnectionBox);
            ToolBar.Items.Add(ImportButton);
            
            ConnectionBox.SelectedIndexChanged += GetDataStructs;
            ImportButton.Click += ImportStructs;

            StructTree.AfterSelect += StructTreeSelection;
            StructTree.AfterCheck += StructTreeCheck;
            FieldTree.AfterCheck += FieldTreeCheck;

            GetConnections();
        }

        string TargetPath { get; set; }

        DataStructImporter StructImporter { get; set; }

        Dictionary<string, Dictionary<string, string>> SelectedStructs = new Dictionary<string, Dictionary<string, string>>();

        ToolStrip ToolBar = new ToolStrip();

        ToolStripLabel ConnectionLabel = new ToolStripLabel() { Text = "Select Connection:" };

        ToolStripComboBox ConnectionBox = new ToolStripComboBox() { AutoSize = false, Width = 400 };

        ToolStripButton ImportButton = new ToolStripButton() { AutoSize = false, Width = 200, Text = "Import Selected (0)", Enabled = false };

        Panel ImportPanel = new Panel() { Dock = DockStyle.Fill };

        TreeView StructTree = new TreeView() { Dock = DockStyle.Fill, CheckBoxes = true, ImageList = Images.List };

        TreeView FieldTree = new TreeView() { Dock = DockStyle.Fill, CheckBoxes = true, ImageList = Images.List };

        Panel DataStructPanel()
        {
            Panel output = new Panel();
            output.Dock = DockStyle.Fill;

            SplitContainer treePanel = new SplitContainer();
            treePanel.Dock = DockStyle.Fill;
            treePanel.Panel1.Show();
            treePanel.Panel1.Controls.Add(StructTree);
            treePanel.Panel2.Controls.Add(FieldTree);
            treePanel.Panel2.Show();
            output.Controls.Add(treePanel);

            return output;
        }

        void GetConnections()
        {
            ConnectionBox.Items.Clear();
            ConnectionItems.Clear();

            foreach(string connectionFile in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Connections"), "*.json", SearchOption.AllDirectories))
            {
                string connectionName = Path.GetDirectoryName(connectionFile).Replace(Environment.CurrentDirectory + @"\", "") + @"\" + Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(connectionFile));
                ConnectionBox.Items.Add(connectionName);
                ConnectionItems.Add(connectionName, connectionFile);
            }
        }

        Dictionary<string, string> ConnectionItems = new Dictionary<string, string>();

        void GetDataStructs(object sender, EventArgs e)
        {
            string connectionFile = ConnectionItems.GetValueOrDefault(ConnectionBox.Text);

            Dictionary<string, Dictionary<string, string>> sourceStructs = new Dictionary<string, Dictionary<string, string>>();

            if (connectionFile.EndsWith(".sqlcon.json"))
            {
                DataStructImporter importer = new SqlServerDataStructImporter(connectionFile);
                sourceStructs = importer.SourceStructures;
                StructImporter = importer;
            }

            if(sourceStructs.Count > 0)
            {
                StructTree.Tag = sourceStructs;

                foreach(var sourceStruct in sourceStructs)
                {
                    StructTree.Nodes.Add(new TreeNode() { Text = sourceStruct.Key, Tag = sourceStruct.Key, ImageKey = "Data Struct", SelectedImageKey = "Data Struct" });
                }
            }
        }

        void StructTreeSelection(object sender, TreeViewEventArgs e)
        {
            FieldTree.Nodes.Clear();

            string structName = e.Node.Tag.ToString();
            Dictionary<string, Dictionary<string, string>> sourceStructs = (Dictionary<string, Dictionary<string, string>>)StructTree.Tag;
            Dictionary<string, string> structFields = sourceStructs.GetValueOrDefault(structName);

            FieldTree.Tag = structName;

            foreach(var field in structFields)
            {
                bool selected = false;

                if (SelectedStructs.ContainsKey(structName))
                {
                    Dictionary<string, string> fieldSelection = SelectedStructs.GetValueOrDefault(structName);

                    if (fieldSelection.ContainsKey(field.Key))
                    {
                        selected = true;
                    }
                }

                TreeNode fieldNode = new TreeNode() { Text = field.Key, Tag = field.Key, ImageKey = "Field", SelectedImageKey = "Field", Checked = selected };
                FieldTree.Nodes.Add(fieldNode);
            }
        }

        void StructTreeCheck(object sender, TreeViewEventArgs e)
        {
            Dictionary<string, Dictionary<string, string>> sourceStructs = (Dictionary<string, Dictionary<string, string>>)StructTree.Tag;
            string structName = e.Node.Tag.ToString();

            if (e.Node.Checked)
            {
                if (!SelectedStructs.ContainsKey(structName))
                {
                    SelectedStructs.Add(structName, sourceStructs.GetValueOrDefault(structName));
                }
            }

            if(!e.Node.Checked)
            {
                if (SelectedStructs.ContainsKey(structName))
                {
                    SelectedStructs.Remove(structName);
                }
            }

            foreach (TreeNode fieldNode in FieldTree.Nodes)
            {
                fieldNode.Checked = e.Node.Checked;
            }

            SetImportButton();
        }

        void FieldTreeCheck(object sender, TreeViewEventArgs e)
        {
            string structName = FieldTree.Tag.ToString();
            string fieldName = e.Node.Tag.ToString();
            Dictionary<string, Dictionary<string, string>> sourceStructs = (Dictionary<string, Dictionary<string, string>>)StructTree.Tag;

            if (e.Node.Checked)
            {
                if(SelectedStructs.ContainsKey(structName))
                {
                    Dictionary<string, string> structFields = SelectedStructs[structName];

                    if(!structFields.ContainsKey(fieldName))
                    {
                        structFields.Add(fieldName, sourceStructs.GetValueOrDefault(structName).GetValueOrDefault(fieldName));
                    }
                }
            }

            if (!e.Node.Checked)
            {
                if (SelectedStructs.ContainsKey(structName))
                {
                    Dictionary<string, string> structFields = SelectedStructs[structName];

                    if (structFields.ContainsKey(fieldName))
                    {
                        structFields.Remove(fieldName);
                    }
                }
            }
        }

        void SetImportButton()
        {
            ImportButton.Text = string.Format("Import Selected ({0})", SelectedStructs.Count);

            switch(SelectedStructs.Count)
            {
                case 0:
                    ImportButton.Enabled = false;
                    break;
                default:
                    ImportButton.Enabled = true;
                    break;
            }
        }

        void ImportStructs(object sender, EventArgs e)
        {
            StructImporter.SourceStructures = SelectedStructs;
            DataStruct[] dataStructs = StructImporter.GetDataStructs();

            foreach(DataStruct ds in dataStructs)
            {
                ds.Name = StructNameFormatter(ds.SourceItemName);
                ds.FolderPath = Path.Combine(Environment.CurrentDirectory, TargetPath);
                ds.Save();
            }

            Close();
        }

        string StructNameFormatter(string name)
        {
            string connectionFile = ConnectionItems[ConnectionBox.Text];
            string extension = Path.GetExtension(Path.GetFileNameWithoutExtension(connectionFile));

            switch (extension)
            {
                case ".sqlcon":
                    return name.Split(".").Last();
                default:
                    return name;
            }
        }
    }
}
