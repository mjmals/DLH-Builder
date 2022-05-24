using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHBuilder.Desktop.Model
{
    class CollatedScriptTemplateCollection : ScriptTemplateCollection
    {
        public CollatedScriptTemplateCollection(ScriptTemplateCollection projecttemplates)
        {
            ScriptTemplateCollection templates = new ScriptTemplateCollection();

            templates.AddRange(projecttemplates);
            
            foreach(Type collectionType in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => x.IsAssignableTo(typeof(BuiltInScriptTemplateCollection))))
            {
                BuiltInScriptTemplateCollection collection = (BuiltInScriptTemplateCollection)Activator.CreateInstance(collectionType);
                templates.AddRange(collection);
            }

            this.AddRange(templates.OrderBy(x => x.Path() + "." + x.Name));
        }
    }
}
