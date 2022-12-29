using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class EnvironmentsMenu : ProjectTreeMenu
    {
        public EnvironmentsMenu(EnvironmentsNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Environment", AddEnvironment));
        }

        EnvironmentsNode Node
        {
            get => (EnvironmentsNode)Tag;
            set => Tag = value;
        }

        void AddEnvironment(object sender, EventArgs e)
        {
            Environment environment = new Environment();
            environment.ID = Guid.NewGuid();
            environment.Name = "<New Environment>";
            Node.Environments.Add(environment);
        }
    }
}
