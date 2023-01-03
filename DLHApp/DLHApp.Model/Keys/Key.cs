using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHApp.Model.Keys
{
    public class Key : ModelItem, IModelItem
    {
        public override string BasePath => GetBasePath("Keys");

        public TemplateReferenceCollection Templates { get; set; }

        public new static Key New()
        {
            Key output = new Key();
            output.Templates = new TemplateReferenceCollection();
            return output;
        }

        public static Key Load(string path)
        {
            Key output = JsonConvert.DeserializeObject<Key>(File.ReadAllText(path));
            output.Name = Path.GetFileNameWithoutExtension(path);
            output.SourcePath = path;
            return output;
        }
    }
}
