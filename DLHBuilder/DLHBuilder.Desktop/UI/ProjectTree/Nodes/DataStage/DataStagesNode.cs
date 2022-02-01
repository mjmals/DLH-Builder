using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataStagesNode : ProjectTreeNode
    {
        public DataStagesNode(DataStageCollection stages)
        {
            Stages = stages;
            Text = "Data Stages";

            ContextMenuStrip = new DataStagesMenu(this);
        }

        DataStageCollection Stages 
        {
            get => (DataStageCollection)Tag;
            set => Tag = value;
        }

        public override string CollapsedImage => "Folder Closed";

        public override string ExpandedImage => "Folder Open";

        public override bool AllowLabelChange => false;
    }
}
