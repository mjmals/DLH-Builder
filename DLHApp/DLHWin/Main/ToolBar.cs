using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Styles;

namespace DLHWin.Main
{
    internal class ToolBar : ToolStrip
    {
        public ToolBar()
        {
            ImageList = Images.List;
            AddButton("NewProject", "Project");
            AddButton("OpenProject", "Folder Open");
            AddButton("Save", "Save");
        }

        void AddButton(string name, string image)
        {
            ToolStripItem item = new ToolStripButton();
            item.Name = name;
            item.ImageKey = image;
            Items.Add(item);
        }

        public void SetToolbarItemClick(string name, EventHandler task)
        {
            Items.Find(name, true).FirstOrDefault().Click += task;
        }
    }
}
