﻿using System;
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
            output.Add("var", Color.Blue);
            output.Add("@", Color.DarkGoldenrod);
            output.Add("\"", Color.OrangeRed);

            return output;
        }

        protected override List<Block> Blocks()
        {
            List<Block> output = new List<Block>();

            output.Add(new Block("\"", "\"", Color.OrangeRed));


            return output;
        }
    }
}
