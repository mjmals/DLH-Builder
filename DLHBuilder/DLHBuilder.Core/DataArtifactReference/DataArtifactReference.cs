using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class DataArtifactReference
    {
        public Guid ID { get; set; }

        public Guid DataArtifactID { get; set; }

        public List<string> Path { get; set; }

        public string FullPath
        {
            get
            {
                List<string> output = new List<string>();
                output.AddRange(Path);
                output.Add(ID.ToString());

                return string.Join('.', output);
            }
        }

        [JsonIgnore]
        public string Name
        {
            get
            {
                return ID.ToString();
            }
        }

        [JsonIgnore]
        public DataArtifact ReferencedArtifact { get; set; }

        //[JsonIgnore]
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

        //[JsonIgnore]
        public DataArtifactDependencyCollection Dependencies
        {
            get
            {
                if(dependencies == null)
                {
                    dependencies = new DataArtifactDependencyCollection();
                }
                return dependencies;
            }
            set => dependencies = value;
        }

        private DataArtifactDependencyCollection dependencies { get; set; }

        public ScriptTemplateReferenceCollection ScriptTemplates
        {
            get
            {
                if (scripttemplates == null)
                {
                    scripttemplates = new ScriptTemplateReferenceCollection() { Type = ScriptTemplateReferenceType.Inherited };
                }
                return scripttemplates;
            }
            set => scripttemplates = value;
        }

        private ScriptTemplateReferenceCollection scripttemplates { get; set; }

        private DataArtifactTransformationCollection transformations { get; set; }
    }
}
