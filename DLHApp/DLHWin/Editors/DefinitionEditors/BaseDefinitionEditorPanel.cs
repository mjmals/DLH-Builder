using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Editors.DefinitionEditors
{
    internal class BaseDefinitionEditorPanel : DefinitionEditorPanel
    {
        public BaseDefinitionEditorPanel(string fileName) : base(fileName)
        {

        }

        public override string[] Extensions => new string[0];
    }
}
