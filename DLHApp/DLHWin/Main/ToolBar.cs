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
            AddSeperator();
            AddButton("Build", "Run", "Build");
        }

        void AddButton(string name, string image, string text = null)
        {
            ToolStripItem item = new ToolStripButton();
            item.Name = name;
            item.ImageKey = image;

            if(!string.IsNullOrEmpty(text))
            {
                item.Text = text;
            }

            Items.Add(item);
        }

        void AddSeperator()
        {
            ToolStripSeparator separator = new ToolStripSeparator();
            Items.Add(separator);
        }

        public void SetToolbarItemClick(string name, EventHandler task)
        {
            Items.Find(name, true).FirstOrDefault().Click += task;
        }
    }
}
