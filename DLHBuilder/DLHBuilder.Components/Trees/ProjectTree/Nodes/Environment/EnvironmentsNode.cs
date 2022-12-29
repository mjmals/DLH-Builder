using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class EnvironmentsNode : ProjectTreeNode
    {
        public EnvironmentsNode(EnvironmentCollection environments)
        {
            Text = "Environments";
            Environments = environments;
            AddEnvironments();
        }

        public EnvironmentCollection Environments
        {
            get => (EnvironmentCollection)Tag;
            set
            {
                value.CollectionAdded += OnEnvironmentAdded;
                Tag = value;
            }
        }

        void OnEnvironmentAdded(object sender, EventArgs e)
        {
            EnvironmentNode node = AddEnvironment((IEnvironment)sender);
            Nodes.Add(node);
            Tree.SelectedNode = node;
        }

        EnvironmentNode AddEnvironment(IEnvironment environment)
        {
            return new EnvironmentNode(environment);
        }

        void AddEnvironments()
        {
            foreach(IEnvironment environment in Environments)
            {
                Nodes.Add(AddEnvironment(environment));
            }
        }

        public override ContextMenuStrip ContextMenuStrip => new EnvironmentsMenu(this);
    }
}
