using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    internal class DataArtifactReferenceLoadStepSetNode : ProjectTreeNode
    {
        public DataArtifactReferenceLoadStepSetNode(LoadStepSet stepSet)
        {
            Text = stepSet.Name;
            StepSet = stepSet;
        }

        public LoadStepSet StepSet
        {
            get => (LoadStepSet)Tag;
            set
            {
                value.CollectionAdded += OnLoadStepAdded;
                value.PropertyUpdated += OnPropertyUpdated;
                Tag = value;
            }
        }

        public override ContextMenuStrip ContextMenuStrip => new DataArtifactReferenceLoadStepSetMenu(this);

        void OnLoadStepAdded(object sender, EventArgs e)
        {
            LoadStep step = (LoadStep)sender;
            Tree.SelectedNode = AddLoadStep(step);
        }

        DataArtifactReferenceLoadStepNode AddLoadStep(LoadStep step)
        {
            DataArtifactReferenceLoadStepNode output = new DataArtifactReferenceLoadStepNode(step);
            Nodes.Add(output);
            return output;
        }

        void AddLoadSteps()
        {
            foreach(LoadStep step in StepSet)
            {
                AddLoadStep(step);
            }
        }

        public override void LabelChanged(string text)
        {
            base.LabelChanged(text);
            StepSet.Name = text;
        }

        void OnPropertyUpdated(object sender, EventArgs e)
        {
            Text = StepSet.Name;
        }
    }
}
