using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.CodeParsers;

namespace DLHWin.Editors.DefinitionEditors
{
    internal class SqlServerDefinitionEditorGrid : DefinitionEditorGrid
    {
        public SqlServerDefinitionEditorGrid(string fileName) : base(fileName)
        {
            Parser = new SqlServerCodeParser(fileName);
        }
    }
}
