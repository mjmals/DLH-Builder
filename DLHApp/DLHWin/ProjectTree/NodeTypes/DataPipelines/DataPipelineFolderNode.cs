using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataPipelines;

namespace DLHWin.ProjectTree.NodeTypes.DataPipelines
{
    internal class DataPipelineFolderNode : FolderNode
    {
        public DataPipelineFolderNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override ContextMenuStrip Menu()
        {
            ProjectTreeNodeMenu output = (ProjectTreeNodeMenu)base.Menu();

            output.InsertButton("Add Data Pipeline", AddPipeline, 0);

            return output;
        }

        void AddPipeline(object sender, EventArgs e)
        {
            string parentPath = DirectoryItem.FullPath.Replace(@"Data Pipelines\", "");

            DataPipeline pipeline = DataPipeline.New();
            pipeline.Name = "New Pipeline";
            pipeline.FolderPath = parentPath;
            pipeline.Save();

            Tree.RefreshTree();
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.FullPath.StartsWith(@"Data Pipelines") && !directoryItem.FullPath.Contains(@"\Tasks"))
            {
                if(Directory.GetFiles(directoryItem.FullPath).Where(x => x.EndsWith(".dpl.json")).Count() > 0)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
