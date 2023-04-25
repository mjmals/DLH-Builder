using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructReferences;
using DLHApp.Model.DataStructs;

namespace DLHWin.Editors.Dialogs
{
    internal class DataStructReferenceLinkDialog : Form
    {
        public DataStructReferenceLinkDialog(string destPath)
        {
            Text = "Link Data Struct";
            Height = 550;
            Width = 800;

            DestPath = destPath;

            Controls.Add(StructTree);
            Controls.Add(Toolbar);

            Toolbar.Items.Add(LinkButton);
            LinkButton.Click += LinkDataStruct;
            StructTree.AfterSelect += NodeSelectionChanged;

            LoadDataStructs();
        }

        string DestPath { get; set; }

        public string DestFile { get; set; }

        ToolStrip Toolbar = new ToolStrip();

        ToolStripButton LinkButton = new ToolStripButton() { Text = "Link Data Struct", Enabled = false };

        TreeView StructTree = new TreeView() { Dock = DockStyle.Fill, ImageList = Styles.Images.List };

        void CreateNodes(string structFile)
        {
            string[] folderLevels = Path.GetDirectoryName(structFile).Split(@"\");
            string folderParent = string.Empty;

            foreach(string folder in folderLevels)
            {
                string folderPath = Path.Combine(folderParent, folder);

                if (StructTree.Nodes.Find(folderPath, true).Count() == 0)
                {
                    TreeNode node = new TreeNode();
                    node.Text = folder;
                    node.Name = folderPath;
                    node.ImageKey = "Folder Closed";
                    node.SelectedImageKey = "Folder Closed";

                    if(string.IsNullOrEmpty(folderParent))
                    {
                        StructTree.Nodes.Add(node);
                    }
                    else
                    {
                        TreeNode parentNode = StructTree.Nodes.Find(folderParent, true).FirstOrDefault();
                        parentNode.Nodes.Add(node);
                    }
                }

                folderParent = folderPath;
            }

            TreeNode structNode = new TreeNode();
            structNode.Name = structFile;
            structNode.Text = Path.GetFileNameWithoutExtension(structFile);
            structNode.ImageKey = "Data STruct";
            structNode.SelectedImageKey = "Data Struct";

            TreeNode structParentNode = StructTree.Nodes.Find(folderParent, true).FirstOrDefault();
            structParentNode.Nodes.Add(structNode);
        }

        void LoadDataStructs()
        {
            foreach(string structFile in Directory.GetFiles("Data Structures", "*.datastruct", SearchOption.AllDirectories))
            {
                CreateNodes(structFile);
            }
        }

        void NodeSelectionChanged(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Name.EndsWith(".datastruct"))
            {
                LinkButton.Enabled = true;
                return;
            }

            LinkButton.Enabled = false;
        }

        void LinkDataStruct(object sender, EventArgs e)
        {
            DataStruct ds = DataStruct.Load(StructTree.SelectedNode.Name);

            DataStructReference dsr = DataStructReference.New();
            dsr.Name = StructTree.SelectedNode.Text;
            dsr.SourceDataStruct = StructTree.SelectedNode.Name.Replace(".datastruct", "");
            dsr.FolderPath = DestPath;
            
            foreach(DataStructField field in ds.Fields)
            {
                dsr.Fields.Add(new DataStructFieldReference() { SourceField = field.Name, OutputName = field.Name });
            }

            DestFile = Path.Combine(DestPath, dsr.Name + "." + dsr.OutputExtension);
            dsr.Save();

            DialogResult = DialogResult.OK;
        }
    }
}
