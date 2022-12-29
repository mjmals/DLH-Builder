using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    internal class KeyNode : ProjectTreeNode
    {
        public KeyNode(Key key)
        {
            Key = key;
            Text = Key.Name;
        }

        internal Key Key 
        {
            get => (Key)Tag;
            set
            {
                value.PropertyUpdated += OnPropertyUpdated;
                Tag = value;
            }
        }

        public override string CollapsedImage => "Key";

        public override string ExpandedImage => "Key";

        void OnPropertyUpdated(object sender, EventArgs e)
        {
            Text = Key.Name;
        }

        public override void LabelChanged(string text)
        {
            Key.Name = text;
        }
    }
}
