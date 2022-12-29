using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHBuilder.Components.Model
{
    public class CollatedScriptTemplateCollection : ScriptTemplateCollection
    {
        public CollatedScriptTemplateCollection(ScriptTemplateCollection projecttemplates)
        {
            ScriptTemplateCollection templates = new ScriptTemplateCollection();

            templates.AddRange(projecttemplates);

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Type[] templateCollections = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => x.IsAssignableTo(typeof(BuiltInScriptTemplateCollection))).ToArray();

            foreach (Type collectionType in templateCollections)
            {
                BuiltInScriptTemplateCollection collection = (BuiltInScriptTemplateCollection)Activator.CreateInstance(collectionType);
                templates.AddRange(collection);
            }

            this.AddRange(templates.OrderBy(x => x.Path() + "." + x.Name));
        }
    }
}
