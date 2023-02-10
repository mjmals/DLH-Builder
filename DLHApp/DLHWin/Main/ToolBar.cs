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
            AddButton("Refresh", "Refresh");
            AddSeperator();
            AddButton("Build", "Run", "Build");
            AddSeperator();
            AddLabel("Filter:");
            AddTextBox("FilterText", 400);
            AddButton("ApplyFilter", "Search");
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

        public string GetTextboxValue(string name)
        {
            ToolStripTextBox textBox = (ToolStripTextBox)Items.Find(name, true).FirstOrDefault();
            return textBox.Text;
        }

        void AddLabel(string text)
        {
            ToolStripLabel label = new ToolStripLabel() { Text = text };
            label.Name = string.Format("Label_{0}", text);
            Items.Add(label);
        }

        void AddTextBox(string name, int width)
        {
            ToolStripTextBox textBox = new ToolStripTextBox();
            textBox.Name = name;
            textBox.Width = width;
            textBox.AutoSize = false;
            Items.Add(textBox);
        }
    }
}
