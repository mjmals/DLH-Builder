using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DLHBuilder
{
    public interface ILoadStep
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public CodeDefinitionLanguage Language { get; set; }

        [Browsable(false)]
        public string Code { get; set; }

        public int Ordinal { get; set; }
    }
}
