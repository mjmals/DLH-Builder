using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class TransformationEditorGridRow : EditorGridRow
    {
        public TransformationEditorGridRow(DataArtifactTransformation transformation, DataSource source)
        {
            
        }

        DataGridViewTextBoxCell DataSource { get; set; }
    }
}
