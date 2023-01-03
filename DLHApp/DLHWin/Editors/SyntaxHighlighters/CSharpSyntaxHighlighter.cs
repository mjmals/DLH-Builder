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

            output.Add("string", Color.Blue);
            output.Add("@", Color.DarkGoldenrod);
            output.Add("\"", Color.OrangeRed);

            return output;
        }
    }
}
