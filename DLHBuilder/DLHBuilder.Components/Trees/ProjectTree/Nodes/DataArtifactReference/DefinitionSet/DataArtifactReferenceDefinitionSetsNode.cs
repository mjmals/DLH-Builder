using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    internal class DataArtifactReferenceDefinitionSetsNode : ProjectTreeNode
    {
        public DataArtifactReferenceDefinitionSetsNode(DataArtifactReference reference)
        {
            Text = "Definition Sets";
            
            // to be removed
            if (reference.DefinitionSets.Count() == 0)
            {
                CodeDefinitionSet definitionSet = new CodeDefinitionSet();
                definitionSet.ID = Guid.NewGuid();
                definitionSet.Name = "Default";
                reference.DefinitionSets.Add(definitionSet);
            }

            DefinitionSets = reference.DefinitionSets;
            Reference = reference;

            AddDefinitionSets();
        }

        public DataArtifactReference Reference { get; set; }

        public CodeDefinitionSetCollection DefinitionSets
        {
            get => (CodeDefinitionSetCollection)Tag;
            set
            {
                value.CollectionAdded += OnDefinitionSetAdded;
                Tag = value;
            }
        }

        public override ContextMenuStrip ContextMenuStrip => new DataArtifactReferenceDefinitionSetsMenu(this);

        void OnDefinitionSetAdded(object sender, EventArgs e)
        {
            CodeDefinitionSet set = (CodeDefinitionSet)sender;
            Tree.SelectedNode = AddDefinitionSet(set);
        }

        DataArtifactReferenceDefinitionSetNode AddDefinitionSet(CodeDefinitionSet set)
        {
            DataArtifactReferenceDefinitionSetNode output = new DataArtifactReferenceDefinitionSetNode(set);
            Nodes.Add(output);
            return output;
        }

        void AddDefinitionSets()
        {
            foreach(CodeDefinitionSet set in DefinitionSets)
            {
                AddDefinitionSet(set);
            }
        }
    }
}
