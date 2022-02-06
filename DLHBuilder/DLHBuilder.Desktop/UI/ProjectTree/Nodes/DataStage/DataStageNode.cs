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

            Nodes.Add(new DataArtifactsNode());
        }

        DataStage Stage
        {
            get => (DataStage)Tag;
            set => Tag = value;
        }

        public override string CollapsedImage => "Data Stage";

        public override string ExpandedImage => "Data Stage";

        public override Control[] Editors()
        {
            return new Control[]
            {
                new DataStageTree(Stage),
                new PropertyEditor(Stage)
            };
        }

        public override void LabelChanged(string text)
        {
            Stage.Name = text;
            base.LabelChanged(text);
        }
    }
}
