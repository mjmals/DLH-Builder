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
        public SqlServerDefinitionEditorPanel(string fileName) : base(fileName)
        {

        }

        public override string[] Extensions => new string[] { ".sql" };

        protected override SyntaxHighlighter Highlighter => new SqlSyntaxHighlighter();
    }
}
