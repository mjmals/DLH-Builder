using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactSchema
    {
        public decimal Version { get; set; }

        public DataArtifactSchemaItemCollection Items
        { 
            get
            {
                if(items == null)
                {
                    items = new DataArtifactSchemaItemCollection();
                }
                return items;
            }
            set => items = value;
        }

        private DataArtifactSchemaItemCollection items { get; set; }

        public override string ToString()
        {
            return string.Format("v{0}", Version);
        }
    }
}
