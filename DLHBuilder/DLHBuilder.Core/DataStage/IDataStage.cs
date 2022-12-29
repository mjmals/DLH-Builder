using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public interface IDataStage
    {
        Guid ID { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        int Ordinal { get; set; }

        public Guid ConnectionID { get; set; }

        DataStageFolderCollection Folders { get; set; }

        DataArtifactReferenceCollection ArtifactReferences { get; set; }

        ScriptTemplateReferenceCollection ScriptTemplates { get; set; }

        ScriptTemplateReferenceCollection ArtifactDefaultScriptTemplates { get; set; }
    }
}
