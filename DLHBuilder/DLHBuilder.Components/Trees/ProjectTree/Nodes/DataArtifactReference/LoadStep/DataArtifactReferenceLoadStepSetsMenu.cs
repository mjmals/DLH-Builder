using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    internal class DataArtifactReferenceLoadStepSetsMenu : ProjectTreeMenu
    {
        public DataArtifactReferenceLoadStepSetsMenu(DataArtifactReferenceLoadStepSetsNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Load Step Set", AddLoadStepSet));
        }

        DataArtifactReferenceLoadStepSetsNode Node
        {
            get => (DataArtifactReferenceLoadStepSetsNode)Tag;
            set => Tag = value;
        }

        void AddLoadStepSet(object sender, EventArgs e)
        {
            LoadStepSet set = new LoadStepSet();
            set.ID = Guid.NewGuid();
            set.Name = "<New Load Step Set>";
            Node.StepSets.Add(set);
        }
    }
}
