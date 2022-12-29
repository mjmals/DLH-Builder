using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    internal class KeyContainersMenu : ProjectTreeMenu
    {
        public KeyContainersMenu(KeyContainersNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Key Container", AddKeyContainer));
        }

        KeyContainersNode Node { get; set; }

        void AddKeyContainer(object sender, EventArgs e)
        {
            KeyContainer keyContainer = new KeyContainer();
            keyContainer.ID = Guid.NewGuid();
            keyContainer.Name = "<New Key Container>";
            Node.KeyContainers.Add(keyContainer);
        }
    }
}
