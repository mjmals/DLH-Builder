using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public interface IDataConnection
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public ScriptTemplateReferenceCollection ScriptTemplates { get; set; }
    }
}
