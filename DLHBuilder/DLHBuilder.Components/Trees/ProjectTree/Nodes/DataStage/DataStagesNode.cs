using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataStagesNode : ProjectTreeNode
    {
        public DataStagesNode(DataStageCollection stages, IDataApplication parentApplication)
        {
            Stages = stages;
            ParentApplication = parentApplication;
            Text = "Data Stages";

            ContextMenuStrip = new DataStagesMenu(this);
            AddStages();
        }

        public DataStageCollection Stages
        {
            get => (DataStageCollection)Tag;
            set
            {
                value.CollectionAdded += OnCollectionAdded;
                Tag = value;
            }
        }

        public IDataApplication ParentApplication { get; set; }

        public override string CollapsedImage => "Folder Closed";

        public override string ExpandedImage => "Folder Open";

        public override bool AllowLabelChange => false;

        void OnCollectionAdded(object sender, EventArgs e)
        {
            DataStageNode node = AddStage((IDataStage)sender);
            Tree.SelectedNode = node;
        }

        void AddStages()
        {
            foreach(IDataStage stage in Stages)
            {
                AddStage(stage);
            }
        }

        DataStageNode AddStage(IDataStage stage)
        {
            DataStageNode output = new DataStageNode(stage, ParentApplication);
            Nodes.Add(output);
            return output;
        }
    }
}
