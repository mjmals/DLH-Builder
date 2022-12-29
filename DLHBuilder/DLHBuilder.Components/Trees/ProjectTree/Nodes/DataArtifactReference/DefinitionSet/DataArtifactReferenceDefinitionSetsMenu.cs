using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    internal class DataArtifactReferenceDefinitionSetsMenu : ProjectTreeMenu
    {
        public DataArtifactReferenceDefinitionSetsMenu(DataArtifactReferenceDefinitionSetsNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Definition Set", AddDefinitionSet));
        }

        public DataArtifactReferenceDefinitionSetsNode Node
        {
            get => (DataArtifactReferenceDefinitionSetsNode)Tag;
            set => Tag = value;
        }
        
        void AddDefinitionSet(object sender, EventArgs e)
        {
            CodeDefinitionSet set = new CodeDefinitionSet();
            set.ID = Guid.NewGuid();
            set.Name = "<New Definition Set>";
            Node.DefinitionSets.Add(set);
        }
    }
}
