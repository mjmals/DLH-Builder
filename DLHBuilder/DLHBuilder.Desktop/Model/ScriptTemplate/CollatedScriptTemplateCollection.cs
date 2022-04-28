using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.Model
{
    class CollatedScriptTemplateCollection : ScriptTemplateCollection
    {
        public CollatedScriptTemplateCollection(ScriptTemplateCollection projecttemplates)
        {
            ScriptTemplateCollection templates = new ScriptTemplateCollection();

            templates.AddRange(projecttemplates);
            templates.AddRange(new BuiltInScriptTemplateCollection());

            this.AddRange(templates.OrderBy(x => x.Path() + "." + x.Name));
        }
    }
}
