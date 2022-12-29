using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    internal class DataArtifactReferenceDefinitionSetNode : ProjectTreeNode
    {
        public DataArtifactReferenceDefinitionSetNode(CodeDefinitionSet set)
        {
            Text = set.Name;
            DefinitionSet = set;
        }

        CodeDefinitionSet DefinitionSet
        {
            get => (CodeDefinitionSet)Tag;
            set
            {
                value.PropertyUpdated += OnPropertyUpdated;
                Tag = value;
            }
        }

        public override string ExpandedImage => "Definition Set";

        public override string CollapsedImage => "Definition Set";

        void OnPropertyUpdated(object sender, EventArgs e)
        {
            Text = DefinitionSet.Name;
        }

        public override void LabelChanged(string text)
        {
            base.LabelChanged(text);
            DefinitionSet.Name = Text;
        }
    }
}
