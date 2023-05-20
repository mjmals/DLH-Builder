using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Editors.SyntaxHighlighters
{
    internal class SqlSyntaxHighlighter : SyntaxHighlighter
    {
        protected override Dictionary<string, Color> Tokens()
        {
            Dictionary<string, Color> output = new Dictionary<string, Color>();

            output.Add("CREATE", Color.Blue);
            output.Add("UPDATE", Color.Blue);
            output.Add("INSERT", Color.Blue);
            output.Add("DELETE", Color.Blue);
            output.Add("TABLE", Color.Blue);
            output.Add("VIEW", Color.Blue);
            output.Add("PROCEDURE", Color.Blue);
            output.Add("SELECT", Color.Blue);
            output.Add("FROM", Color.Blue);
            output.Add("WHERE", Color.Blue);
            output.Add("GROUP BY", Color.Blue);
            output.Add("ORDER BY", Color.Blue);
            output.Add("HAVING", Color.Blue);

            return output;
        }

        protected override List<Block> Blocks()
        {
            List<Block> output = new List<Block>();

            output.Add(new Block("'", "'", Color.OrangeRed));
            output.Add(new Block("--", "\n", Color.Green));
            output.Add(new Block("/*", "*/", Color.Green));

            return output;
        }
    }
}
