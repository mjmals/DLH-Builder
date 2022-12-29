using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    internal class KeyContainerNode : ProjectTreeNode
    {
        public KeyContainerNode(KeyContainer keyContainer)
        {
            Text = keyContainer.Name;
            Container = keyContainer;
            AddKeys();
        }

        public KeyContainer Container
        {
            get => (KeyContainer)Tag;
            set
            {
                value.Keys.CollectionAdded += OnKeyAdded;
                value.PropertyUpdated += OnPropertyUpdated;
                Tag = value;
            }
        }

        public override string CollapsedImage => "Key Container";

        public override string ExpandedImage => "Key Container";

        public override ContextMenuStrip ContextMenuStrip => new KeyContainerMenu(this);

        void OnKeyAdded(object sender, EventArgs e)
        {
            KeyNode node = AddKey((Key)sender);
            Nodes.Add(node);
            Tree.SelectedNode = node;
        }

        KeyNode AddKey(Key key)
        {
            return new KeyNode(key);
        }

        void AddKeys()
        {
            foreach(Key key in Container.Keys)
            {
                Nodes.Add(AddKey(key));
            }
        }

        void OnPropertyUpdated(object sender, EventArgs e)
        {
            Text = Container.Name;
        }

        public override void LabelChanged(string text)
        {
            Container.Name = text;
        }
    }
}
