using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Editors.SyntaxHighlighters
{
    internal class CSharpSyntaxHighlighter : SyntaxHighlighter
    {
        protected override Dictionary<string, Color> Tokens()
        {
            Dictionary<string, Color> output = new Dictionary<string, Color>();

            output.Add("using", Color.Blue);
            output.Add("string", Color.Blue);
            output.Add("bool", Color.Blue);
            output.Add("if", Color.Blue);
            output.Add("foreach", Color.Blue);
            output.Add("for", Color.Blue);
            output.Add("return", Color.Blue);
            output.Add("break", Color.Blue);
            output.Add("continue", Color.Blue);
            output.Add("else", Color.Blue);
            output.Add("var", Color.Blue);
            output.Add("@", Color.DarkGoldenrod);
            output.Add("\"", Color.OrangeRed);
            output.Add("List", Color.Green);

            return output;
        }

        protected override List<Block> Blocks()
        {
            List<Block> output = new List<Block>();

            output.Add(new Block("\"", "\"", Color.OrangeRed));
            output.Add(new Block(@"//", "\n", Color.Green));


            return output;
        }
    }
}
