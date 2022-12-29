using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    internal class KeyContainerMenu : ProjectTreeMenu
    {
        public KeyContainerMenu(KeyContainerNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Key", AddKey));
        }

        KeyContainerNode Node { get; set; }

        void AddKey(object sender, EventArgs e)
        {
            Key key = new Key();
            key.ID = Guid.NewGuid();
            key.Name = "<New Key>";
            Node.Container.Keys.Add(key);
        }
    }
}
