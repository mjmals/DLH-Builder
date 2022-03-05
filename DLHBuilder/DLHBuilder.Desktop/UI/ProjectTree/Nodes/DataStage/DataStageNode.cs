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
        public DataStageNode(IDataStage stage)
        {
            Stage = stage;
            Text = stage.Name;
        }

        protected IDataStage Stage
        {
            get => (IDataStage)Tag;
            set => Tag = value;
        }

        public override string CollapsedImage => "Data Stage";

        public override string ExpandedImage => "Data Stage";

        public override Control[] Editors()
        {
            return new Control[]
            {
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
