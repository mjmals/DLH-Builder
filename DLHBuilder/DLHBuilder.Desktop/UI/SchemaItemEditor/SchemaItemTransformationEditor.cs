using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class SchemaItemTransformationEditor : Editor
    {
        public SchemaItemTransformationEditor(DataArtifactTransformationCollection transformations, DataStageCollection stages)
        {
            Text = "Transformations";
            Grid = new SchemaItemTransformationEditorGrid(transformations, stages);
            SetControls();
        }

        SchemaItemTransformationEditorGrid Grid { get; set; }

        protected override Control[] EditorControls()
        {
            return new Control[]
            {
                Grid
            };
        }
    }
}
