using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactSchemaItem
    {
        public string Name { get; set; }

        public IDataType DataType { get; set; }

        public DataArtifactTransformationCollection Transformations
        {
            get
            {
                if(transformations == null)
                {
                    transformations = new DataArtifactTransformationCollection();
                }
                return transformations;
            }
            set => transformations = value;
        }

        private DataArtifactTransformationCollection transformations;

        public static DataArtifactSchemaItem New()
        {
            DataArtifactSchemaItem output = new DataArtifactSchemaItem();
            output.Name = "<New Schema Item>";
            return output;
        }
    }
}
