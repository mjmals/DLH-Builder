using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Editors.SyntaxHighlighters
{
    internal class PythonSyntaxHighlighter : SyntaxHighlighter
    {
        protected override Dictionary<string, Color> Tokens()
        {
            Dictionary<string, Color> output = new Dictionary<string, Color>();

            output.Add("for ", Color.Purple);
            output.Add(" in ", Color.Purple);
            output.Add("while", Color.Purple);
            output.Add("return", Color.Purple);
            output.Add("from", Color.Purple);
            output.Add("import", Color.Purple);

            return output;
        }

        protected override List<Block> Blocks()
        {
            List<Block> output = new List<Block>();

            //output.Add(new Block("'", "'", Color.OrangeRed));
            output.Add(new Block("\"", "\"", Color.OrangeRed));
            output.Add(new Block("#", "\n", Color.Green));

            return output;
        }
    }
}
