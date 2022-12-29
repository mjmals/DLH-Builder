using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class ScriptTemplateReferenceCollection : BuilderCollection<ScriptTemplateReference>
    {
        public ScriptTemplateReferenceType Type { get; set; }
    }
}
