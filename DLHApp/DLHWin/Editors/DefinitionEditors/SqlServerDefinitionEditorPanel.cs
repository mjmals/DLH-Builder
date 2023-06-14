using DLHWin.Editors.SyntaxHighlighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Editors.DefinitionEditors
{
    internal class SqlServerDefinitionEditorPanel : DefinitionEditorPanel
    {
        public SqlServerDefinitionEditorPanel(string fileName, string[] identifiers = null, string identifierLabel = null) : base(fileName, identifiers, identifierLabel)
        {
            GridPanel.Controls.Add(GridPanelControl(fileName, identifiers, identifierLabel));
        }

        public override string[] Extensions => new string[] { ".sql" };

        protected override SyntaxHighlighter Highlighter => new SqlSyntaxHighlighter();

        protected override Panel TopPanel => GridPanel;

        Panel GridPanel = new Panel() { Dock = DockStyle.Fill };

        Control GridPanelControl(string fileName, string[] identifiers, string identifierLabel)
        {
            try
            {
                Grid = new SqlServerDefinitionEditorGrid(File.ReadAllText(fileName), identifiers, identifierLabel);
                return Grid;
            }
            catch(Exception e)
            {
                RichTextBox errorBox = new RichTextBox() { Text = string.Format("Unable to parse SQL statement:\n{0}", e.Message), Dock = DockStyle.Fill, Enabled = false };
                return errorBox;
            }
        }
    }
}
