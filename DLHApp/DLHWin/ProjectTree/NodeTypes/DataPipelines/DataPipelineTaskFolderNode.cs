using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataPipelines;

namespace DLHWin.ProjectTree.NodeTypes.DataPipelines
{
    internal class DataPipelineTaskFolderNode : ProjectTreeNode
    {
        public DataPipelineTaskFolderNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override ContextMenuStrip Menu()
        {
            ProjectTreeNodeMenu output = (ProjectTreeNodeMenu)base.Menu();

            output.InsertButton("Add Task", CreateTask, 0);

            return output;
        }

        void CreateTask(object sender, EventArgs e)
        {
            DataPipelineTask task = DataPipelineTask.New();
            task.Name = "New Task";
            task.FolderPath = DirectoryItem.FullPath;
            task.Save();

            Tree.RefreshTree();
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.Name == "Tasks")
            {
                return true;
            }

            return false;
        }
    }
}
