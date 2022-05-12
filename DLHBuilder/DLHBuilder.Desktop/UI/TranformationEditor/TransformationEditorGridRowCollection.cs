using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class TransformationEditorGridRowCollection : EditorGridRowCollection
    {
        public TransformationEditorGridRowCollection(DataArtifactTransformationCollection transformations, DataSourceCollection sources)
        {

        }

        DataArtifactTransformationCollection Transformations { get; set; }

        DataSourceCollection Sources { get; set; }

        protected override void AddRows()
        {
            //foreach(DataSource source in Sources)
            //{
            //    if(!Transformations.Exists(x => x.DataSourceID == source.ID))
            //    {
            //        Transformations.Add(new DataArtifactTransformation() { DataSourceID = source.ID });
            //    }

            //    DataArtifactTransformation transformation = Transformations.FirstOrDefault(x => x.DataSourceID == source.ID);


            //}
        }
    }
}
