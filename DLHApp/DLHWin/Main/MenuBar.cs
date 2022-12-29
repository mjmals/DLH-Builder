using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Main
{
    internal class MenuBar : MenuStrip
    {
        public MenuBar()
        {
            AddMenuOptions("File");
            AddMenuOptions("File", "New");
            AddMenuOptions("File", "New", "Project");
            AddMenuOptions("File", "Open");

            AddMenuOptions("Edit");
            AddMenuOptions("View");
            
            AddMenuOptions("Terminal");
            AddMenuOptions("Terminal", "Show Terminal");
            AddMenuOptions("Terminal", "Hide Terminal");
        }

        void AddMenuOptions(params string[] paths)
        {
            string parentPath = string.Empty;
            ToolStripMenuItem previousItem = null;

            foreach(string item in paths)
            {
                string itemPath = Path.Combine(parentPath, item);
                ToolStripMenuItem findItem = (ToolStripMenuItem)Items.Find(itemPath, true).FirstOrDefault();

                if (findItem == null)
                {
                    findItem = new ToolStripMenuItem(item);
                    findItem.Name = itemPath;

                    if (previousItem == null)
                        Items.Add(findItem);
                    else
                        previousItem.DropDownItems.Add(findItem);
                }

                previousItem = findItem;
                parentPath = Path.Combine(parentPath, item);
            }
        }

        internal ToolStripMenuItem GetMenuItem(string menuPath)
        {
            return (ToolStripMenuItem)Items.Find(menuPath, true).FirstOrDefault();
        }

        internal void SetMenuItemClick(string menuPath, EventHandler task)
        {
            GetMenuItem(menuPath).Click += task;
        }
    }
}
