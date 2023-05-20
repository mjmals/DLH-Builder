using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Editors.SyntaxHighlighters
{
    internal class JsonSyntaxHighlighter : SyntaxHighlighter
    {
        protected override List<Block> Blocks()
        {
            List<Block> output = new List<Block>();

            output.Add(new Block("\"", "\"", Color.OrangeRed));

            return output;
        }
    }
}
