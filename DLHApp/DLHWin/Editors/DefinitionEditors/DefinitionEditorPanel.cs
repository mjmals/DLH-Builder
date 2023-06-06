using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Editors.SyntaxHighlighters;

namespace DLHWin.Editors.DefinitionEditors
{
    internal abstract class DefinitionEditorPanel : Panel
    {
        public DefinitionEditorPanel(string fileName)
        {
            FileName = fileName;
            Dock = DockStyle.Fill;
            DefinitionTextbox.Text = File.ReadAllText(FileName);
            SetControls();

            if(Highlighter != null)
            {
                Highlighter.Highlight(DefinitionTextbox);
            }
        }

        string FileName { get; set; }

        public abstract string[] Extensions { get; }

        protected RichTextBox DefinitionTextbox = new RichTextBox() { Dock = DockStyle.Fill };

        protected virtual Panel TopPanel => null;

        protected virtual SyntaxHighlighter Highlighter => null;

        void SetControls()
        {
            Controls.Clear();

            if(TopPanel == null)
            {
                Controls.Add(DefinitionTextbox);
                return;
            }

            SplitContainer splitPanel = new SplitContainer() { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal };
            splitPanel.Panel1.Controls.Add(TopPanel);
            splitPanel.Panel2.Controls.Add(DefinitionTextbox);
            Controls.Add(splitPanel);
        }
    }
}
