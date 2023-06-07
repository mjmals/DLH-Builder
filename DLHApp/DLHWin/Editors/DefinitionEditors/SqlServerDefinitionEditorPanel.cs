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
            GridPanel.Controls.Add(Grid = new SqlServerDefinitionEditorGrid(File.ReadAllText(fileName), identifiers, identifierLabel));
        }

        public override string[] Extensions => new string[] { ".sql" };

        protected override SyntaxHighlighter Highlighter => new SqlSyntaxHighlighter();

        protected override Panel TopPanel => GridPanel;

        Panel GridPanel = new Panel() { Dock = DockStyle.Fill };
    }
}
