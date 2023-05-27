using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Editors;
using DLHApp.Model.DataStructReferences;
using DLHWin.ProjectTree.NodeTypes.LoadSteps;

namespace DLHWin.ProjectTree.NodeTypes.DataStructReferences
{
    internal class DataStructReferenceNode : ProjectTreeNode
    {
        public DataStructReferenceNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {
            Nodes.Add(new ScriptsNode(directoryItem, typeof(DataStructReference)));

            if(!Directory.Exists(Path.Combine(Environment.CurrentDirectory, directoryItem.FullPath, "Load Steps")))
            {
                Nodes.Add(new LoadStepFolderNode(new ProjectDirectoryItem() { Name = "Load Steps", Parent = Path.Combine(directoryItem.FullPath) }));
            }
        }

        protected override string[]? Images => new string[] { "Data Struct" };

        protected override ContextMenuStrip Menu()
        {
            ProjectTreeNodeMenu output = new ProjectTreeNodeMenu();
            output.AddButton("Delete Reference", DeleteReference);

            return output;
        }

        public override EditorCollection Editors()
        {
            string file = Directory.GetFiles(DirectoryItem.FullPath).FirstOrDefault(x => Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(x)) == DirectoryItem.Name);

            if (!string.IsNullOrEmpty(file))
            {
                return new EditorCollection(new DataStructReferenceEditor(file));
            }

            return base.Editors();
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if (directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.FullPath.StartsWith("Data Applications") && directoryItem.FullPath.Contains("Stages"))
            {
                if (Directory.GetFiles(directoryItem.FullPath, "*ref.json").Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        void DeleteReference(object sender, EventArgs e)
        {
            if(MessageBox.Show("This will delete the reference, continue?", "Delete Reference", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Tree.DeleteNode(DirectoryItem);
                Directory.Delete(DirectoryItem.FullPath, true);
            }
        }
    }
}
