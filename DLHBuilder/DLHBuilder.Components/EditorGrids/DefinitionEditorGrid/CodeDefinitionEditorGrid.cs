using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.EditorGrids
{
    public class CodeDefinitionEditorGrid : EditorGrid
    {
        public CodeDefinitionEditorGrid(CodeDefinition[] baseObjects) : base(baseObjects)
        {
            Text = "Definitions";
        }

        public override EditorGridColumnDefinition[] ColumnDefinitions
        {
            get
            {
                return new EditorGridColumnDefinition[]
                {
                    new EditorGridColumnDefinition("Definition Set", "DefinitionSetName", typeof(EditorGridObjectCell)),
                    new EditorGridColumnDefinition("Code", "Code", typeof(EditorGridTextEditorCell))
                };
            }
        }
    }
}
