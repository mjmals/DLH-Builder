using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.CodeParsers
{
    public abstract class CodeParser
    {
        public CodeParser(string input)
        {
            Input = input;
        }

        public string Input { get; set; }

        public virtual CodeParserOutput Parse()
        {
            return new CodeParserOutput();
        }
    }
}
