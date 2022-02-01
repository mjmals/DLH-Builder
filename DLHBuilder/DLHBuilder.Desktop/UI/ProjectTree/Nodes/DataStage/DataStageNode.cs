using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataStageNode : ProjectTreeNode
    {
        public DataStageNode(DataStage stage)
        {
            Stage = stage;
            Text = stage.Name;
        }

        DataStage Stage
        {
            get => (DataStage)Tag;
            set => Tag = value;
        }

        public override string CollapsedImage => "Folder Closed";

        public override string ExpandedImage => "Folder Open";

        public override Control[] Editors()
        {
            return new Control[]
            {
                new DataStageTree(Stage),
                new PropertyEditor(Stage)
            };
        }
    }
}
