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

        [JsonIgnore]
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

        public ScriptTemplateReferenceCollection ScriptTemplates
        {
            get
            {
                if (scripttemplates == null)
                {
                    scripttemplates = new ScriptTemplateReferenceCollection() { Type = ScriptTemplateReferenceType.Selected };
                }
                return scripttemplates;
            }
            set => scripttemplates = value;
        }

        private ScriptTemplateReferenceCollection scripttemplates { get; set; }
    }
}
