using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class TransformationEditor : Editor
    {
        public TransformationEditor(DataArtifactTransformationCollection transformations, DataSourceCollection sources)
        {
            Text = "Transformations";
            Transformations = transformations;
            Sources = sources;
            SetControls();
        }

        DataArtifactTransformationCollection Transformations { get; set; }

        DataSourceCollection Sources { get; set; }

        protected override Control[] EditorControls()
        {
            return new Control[] { new TransformationEditorGrid(Transformations, Sources) };
        }
    }
}
