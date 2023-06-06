using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.CodeParsers
{
    public class CodeParserOutputLine
    {
        public string Identifier { get; set; }

        public string Expression { get; set; }

        public int StartPosition { get; set; }

        public int EndPosition { get; set; }

        public int ExpressionLength => Expression.Length;

        public override string ToString()
        {
            return Identifier;
        }
    }
}
