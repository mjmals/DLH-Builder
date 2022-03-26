using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class TransformationEditorGrid : EditorGrid
    {
        public TransformationEditorGrid(DataArtifactTransformationCollection transformations, DataSourceCollection sources)
        {
            Transformations = transformations;
            Sources = sources;
        }

        DataArtifactTransformationCollection Transformations { get; set; }

        DataSourceCollection Sources { get; set; }

        protected override EditorGridColumnCollection EditorColumns()
        {
            return new TransformationEditorGridColumnCollection();
        }
    }
}
