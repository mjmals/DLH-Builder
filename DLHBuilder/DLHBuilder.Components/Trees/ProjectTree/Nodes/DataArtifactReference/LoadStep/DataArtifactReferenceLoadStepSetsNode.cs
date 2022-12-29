using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    internal class DataArtifactReferenceLoadStepSetsNode : ProjectTreeNode
    {
        public DataArtifactReferenceLoadStepSetsNode(LoadStepSetCollection stepSets)
        {
            Text = "Load Steps";
            StepSets = stepSets;
            AddStepSets();
        }

        public LoadStepSetCollection StepSets
        {
            get => (LoadStepSetCollection)Tag;
            set
            {
                value.CollectionAdded += OnSetAdded;
                Tag = value;
            }
        }

        public override ContextMenuStrip ContextMenuStrip => new DataArtifactReferenceLoadStepSetsMenu(this);

        void OnSetAdded(object sender, EventArgs e)
        {
            LoadStepSet stepSet = (LoadStepSet)sender;
            Tree.SelectedNode = AddSet(stepSet);
        }

        DataArtifactReferenceLoadStepSetNode AddSet(LoadStepSet stepSet)
        {
            DataArtifactReferenceLoadStepSetNode output = new DataArtifactReferenceLoadStepSetNode(stepSet);
            Nodes.Add(output);
            return output;
        }

        void AddStepSets()
        {
            foreach(LoadStepSet stepSet in StepSets.OrderBy(x => x.Ordinal))
            {
                AddSet(stepSet);
            }
        }
    }
}
