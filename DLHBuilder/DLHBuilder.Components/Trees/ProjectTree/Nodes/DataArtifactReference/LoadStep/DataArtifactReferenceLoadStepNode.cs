using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    internal class DataArtifactReferenceLoadStepNode : ProjectTreeNode
    {
        public DataArtifactReferenceLoadStepNode(LoadStep step)
        {
            Text = step.Name;
            Step = step;
        }

        public LoadStep Step
        {
            get => (LoadStep)Tag;
            set
            {
                value.PropertyUpdated += OnPropertyUpdated;
                Tag = value;
            }
        }

        public override void LabelChanged(string text)
        {
            base.LabelChanged(text);
            Step.Name = text;
        }

        void OnPropertyUpdated(object sender, EventArgs e)
        {
            Text = Step.Name;
        }
    }
}
