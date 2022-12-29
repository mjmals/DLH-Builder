using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class CodeDefinition : Definition, IDefinition
    {
        public string Code { get; set; }
    }
}
