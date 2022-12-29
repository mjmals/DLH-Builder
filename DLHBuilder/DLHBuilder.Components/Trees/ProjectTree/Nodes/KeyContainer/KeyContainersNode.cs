using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    internal class KeyContainersNode : ProjectTreeNode
    {
        public KeyContainersNode(KeyContainerCollection keyContainers)
        {
            Text = "Key Containers";
            KeyContainers = keyContainers;
            AddContainers();
        }

        internal KeyContainerCollection KeyContainers
        {
            get => (KeyContainerCollection)Tag;
            set
            {
                value.CollectionAdded += OnKeyContainerAdded;
                Tag = value;
            }
        }

        public override ContextMenuStrip ContextMenuStrip => new KeyContainersMenu(this);

        void OnKeyContainerAdded(object sender, EventArgs e)
        {
            KeyContainerNode node = AddKeyContainer((KeyContainer)sender);
            Nodes.Add(node);
            Tree.SelectedNode = node;
        }

        KeyContainerNode AddKeyContainer(KeyContainer keyContainer)
        {
            return new KeyContainerNode(keyContainer);
        }

        void AddContainers()
        {
            foreach(KeyContainer container in KeyContainers)
            {
                Nodes.Add(AddKeyContainer(container));
            }
        }
    }
}
