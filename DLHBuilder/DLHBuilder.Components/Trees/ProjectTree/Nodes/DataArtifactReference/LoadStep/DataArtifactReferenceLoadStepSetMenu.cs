using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    internal class DataArtifactReferenceLoadStepSetMenu : ProjectTreeMenu
    {
        public DataArtifactReferenceLoadStepSetMenu(DataArtifactReferenceLoadStepSetNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Load Step", AddLoadStep));
        }

        DataArtifactReferenceLoadStepSetNode Node
        {
            get => (DataArtifactReferenceLoadStepSetNode)Tag;
            set => Tag = value;
        }

        void AddLoadStep(object sender, EventArgs e)
        {
            LoadStep step = new LoadStep();
            step.ID = Guid.NewGuid();
            step.Name = "<New Load Step>";
            Node.StepSet.Add(step);
        }
    }
}
