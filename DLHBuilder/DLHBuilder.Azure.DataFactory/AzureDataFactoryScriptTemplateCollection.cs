using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Azure.DataFactory
{
    public class AzureDataFactoryScriptTemplateCollection : BuiltInScriptTemplateCollection
    {
        protected override string ResourcePrefix => "DLHBuilder.Azure.DataFactory.Templates.";
    }
}
