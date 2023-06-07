using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Editors.SyntaxHighlighters;
using DLHApp.Model.CodeParsers;

namespace DLHWin.Editors.DefinitionEditors
{
    internal abstract class DefinitionEditorPanel : Panel
    {
        public DefinitionEditorPanel(string fileName, string[] identifiers = null, string identifierLabel = null)
        {
            FileName = fileName;
            Identifiers = identifiers;
            IdentifierLabel = identifierLabel;

            Dock = DockStyle.Fill;
            DefinitionTextbox.Text = File.ReadAllText(FileName);
            SetControls();

            if (Highlighter != null)
            {
                Highlighter.Highlight(DefinitionTextbox);
            }
        }

        string FileName { get; set; }

        string[] Identifiers { get; set; }

        string IdentifierLabel { get; set; }

        public abstract string[] Extensions { get; }

        protected RichTextBox DefinitionTextbox = new RichTextBox() { Dock = DockStyle.Fill };

        protected virtual Panel TopPanel => null;

        protected virtual SyntaxHighlighter Highlighter => null;

        protected virtual DefinitionEditorGrid Grid
        {
            get => _grid;
            set
            {
                _grid = value;
                _grid.DefinitionCellSelected += GridDefinitionCellSelected;
            }
        }

        DefinitionEditorGrid _grid { get; set; }

        void SetControls()
        {
            Controls.Clear();

            if (TopPanel == null)
            {
                Controls.Add(DefinitionTextbox);
                return;
            }

            SplitContainer splitPanel = new SplitContainer() { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal };
            splitPanel.Panel1.Controls.Add(TopPanel);
            splitPanel.Panel2.Controls.Add(DefinitionTextbox);
            Controls.Add(splitPanel);
        }

        protected virtual void GridDefinitionCellSelected(object sender, DefinitionEditorGrid.DefinitionCellSelectedEventArgs e)
        {
            DefinitionTextbox.SelectAll();
            DefinitionTextbox.SelectionBackColor = Color.White;
            DefinitionTextbox.SelectionStart = 0;
            DefinitionTextbox.SelectionLength = 0;

            int startPos = e.ParserLine.StartPosition;
            int checkPos = 0;
            int lengthToRemove = 0;

            while (checkPos < startPos)
            {
                string checkString = DefinitionTextbox.Text.Substring(checkPos, 1);

                switch (checkString)
                {
                    case "\n":
                        lengthToRemove += 1;
                        break;
                    case "\r":
                        lengthToRemove += 1;
                        break;
                }

                checkPos++;
            }

            startPos -= lengthToRemove;

            int selectionLength = e.ParserLine.ExpressionLength;
            selectionLength = e.ParserLine.Expression.Replace("\n", "").Length;

            DefinitionTextbox.Select(startPos, selectionLength);
            DefinitionTextbox.SelectionBackColor = Color.LightGray;
        }
    }
}
