using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Editors.DefinitionEditors
{
    internal abstract class DefinitionEditorPanel : Panel
    {
        public DefinitionEditorPanel(string fileName)
        {
            FileName = fileName;
            SplitPanel.Panel2.Controls.Add(DefinitionTextbox);

            DefinitionTextbox.Text = File.ReadAllText(FileName);
        }

        string FileName { get; set; }

        public abstract string[] Extensions { get; }

        public SplitContainer SplitPanel = new SplitContainer() { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal };

        protected RichTextBox DefinitionTextbox = new RichTextBox() { Dock = DockStyle.Fill };
    }
}
