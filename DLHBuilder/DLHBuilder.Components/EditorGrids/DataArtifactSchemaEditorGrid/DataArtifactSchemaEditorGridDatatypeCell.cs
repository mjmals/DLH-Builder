using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.EditorGrids
{
    class DataArtifactSchemaEditorGridDatatypeCell : EditorGridObjectCell
    {
        public override EditorGridPanel Panel => new EditorGridPropertyInstancePanel(this, typeof(IDataType));
    }
}
