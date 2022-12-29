using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;

namespace DLHBuilder
{
    public abstract class DataStage : IDataStage
    {
        [Browsable(false)]
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Browsable(false)]
        public int Ordinal { get; set; }

        public Guid ConnectionID { get; set; }

        //[JsonIgnore]
        public DataStageFolderCollection Folders
        {
            get
            {
                if(folders == null)
                {
                    folders = new DataStageFolderCollection();
                }
                return folders;
            }
            set => folders = value;
        }

        private DataStageFolderCollection folders { get; set; }

        [JsonIgnore]
        public DataArtifactReferenceCollection ArtifactReferences
        {
            get
            {
                if(artifactReferences == null)
                {
                    artifactReferences = new DataArtifactReferenceCollection();
                }
                return artifactReferences;
            }
            set => artifactReferences = value;
        }

        private DataArtifactReferenceCollection artifactReferences { get; set; }

        public ScriptTemplateReferenceCollection ScriptTemplates
        {
            get
            {
                if (scriptTemplates == null)
                {
                    scriptTemplates = new ScriptTemplateReferenceCollection() { Type = ScriptTemplateReferenceType.Selected };
                }
                return scriptTemplates;
            }
            set => scriptTemplates = value;
        }


        private ScriptTemplateReferenceCollection scriptTemplates { get; set; }


        public ScriptTemplateReferenceCollection ArtifactDefaultScriptTemplates
        {
            get
            {
                if (artifactDefaultScriptTemplates == null)
                {
                    artifactDefaultScriptTemplates = new ScriptTemplateReferenceCollection() { Type = ScriptTemplateReferenceType.Selected };
                }
                return artifactDefaultScriptTemplates;
            }
            set => artifactDefaultScriptTemplates = value;
        }

        private ScriptTemplateReferenceCollection artifactDefaultScriptTemplates { get; set; }
    }
}
