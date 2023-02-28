using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.ProjectTree.NodeTypes.LoadSteps
{
    internal class LoadStepFolderNode : ProjectTreeNode
    {
        public LoadStepFolderNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override bool AllowDelete => true;

        protected override ContextMenuStrip Menu()
        {
            ProjectTreeNodeMenu output = (ProjectTreeNodeMenu)base.Menu();

            ToolStripMenuItem addStepBtn = new ToolStripMenuItem();
            addStepBtn.Text = "Add Load Step";
            output.Items.Insert(0, addStepBtn);

            string[] stepTypes = new string[] { ".sql", ".py" };

            foreach(string stepType in stepTypes)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = stepType;
                item.Click += AddLoadStep;
                addStepBtn.DropDownItems.Add(item);
            }

            return output;
        }

        void AddLoadStep(object sender, EventArgs e)
        {
            string fileName = "New Load Step";
            string extension = ".loadstep" + ((ToolStripMenuItem)sender).Text;
            string filePath = Path.Combine(DirectoryItem.FullPath, fileName + extension);
            int iteration = 0;

            while (File.Exists(filePath))
            {
                iteration++;
                fileName = string.Format("{0} ({1})", fileName, iteration);
                filePath = Path.Combine(DirectoryItem.FullPath, fileName + extension);
            }

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                stream.SetLength(0);

                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write("");
                }
            }

            Nodes.Add(new LoadStepNode(new ProjectDirectoryItem() { Extension = extension, Parent = DirectoryItem.FullPath, Name = fileName, Type = ProjectDirectoryItemType.File }));
            Expand();
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.FullPath.Contains(@"\Load Steps"))
            {
                return true;
            }

            return false;
        }
    }
}
