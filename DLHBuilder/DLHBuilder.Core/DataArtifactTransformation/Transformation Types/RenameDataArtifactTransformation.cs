using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class RenameDataArtifactTransformation : DataArtifactTransformation, IDataArtifactTransformation
    {
        public override string Title => "Rename";

        public string OriginalName { get; set; }

        public override string ToString()
        {
            return string.Format("Rename {0}", OriginalName);
        }
    }
}
