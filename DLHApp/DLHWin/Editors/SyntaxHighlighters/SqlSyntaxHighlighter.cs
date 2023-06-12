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

            Color blueHighlight = Color.Blue;
            output.Add("CREATE", blueHighlight);
            output.Add("DROP", blueHighlight);
            output.Add("IF", blueHighlight);
            output.Add("GO", blueHighlight);
            output.Add("UPDATE ", blueHighlight);
            output.Add("INSERT", blueHighlight);
            output.Add("INTO", blueHighlight);
            output.Add("DELETE", blueHighlight);
            output.Add(" TABLE ", blueHighlight);
            output.Add("VIEW", blueHighlight);
            output.Add("PROCEDURE", blueHighlight);
            output.Add("EXEC ", blueHighlight);
            output.Add("SELECT", blueHighlight);
            output.Add("FROM", blueHighlight);
            output.Add("WHERE", blueHighlight);
            output.Add("GROUP BY", blueHighlight);
            output.Add("ORDER BY", blueHighlight);
            output.Add("HAVING", blueHighlight);
            output.Add("IDENTITY", blueHighlight);
            output.Add("DECLARE", blueHighlight);
            output.Add("AS", blueHighlight);
            output.Add("BEGIN", blueHighlight);
            output.Add("SET", blueHighlight);
            output.Add("END", blueHighlight);
            output.Add("CASE", blueHighlight);
            output.Add("WHEN", blueHighlight);
            output.Add("THEN", blueHighlight);
            output.Add("TRY", blueHighlight);
            output.Add("CATCH", blueHighlight);
            output.Add("NOCOUNT", blueHighlight);
            output.Add("ON", blueHighlight);
            output.Add("OFF", blueHighlight);

            Color grayHighlight = Color.Gray;
            output.Add("EXISTS", grayHighlight);
            output.Add("NOT", grayHighlight);
            output.Add("NULL", grayHighlight);

            Color pinkHighlight = Color.DeepPink;
            output.Add("@@ROWCOUNT", pinkHighlight);
            output.Add("COALESCE", pinkHighlight);
            output.Add("ISNULL", pinkHighlight);
            output.Add("ROW_NUMBER", pinkHighlight);
            output.Add("RANK", pinkHighlight);
            output.Add("DENSE_RANK", pinkHighlight);
            output.Add("CONVERT", pinkHighlight);
            output.Add("OBJECT_ID", pinkHighlight);
            output.Add("OBJECT_NAME", pinkHighlight);


            foreach (string dataType in DataTypes())
            {
                output.Add(" " + dataType, blueHighlight);
            }

            return output;
        }

        List<string> DataTypes()
        {
            return new List<string>()
            {
                "int",
                "integer",
                "bigint",
                "smallint",
                "tinyint",
                "float",
                "bit",
                "varchar",
                "nvarchar",
                "char",
                "nchar",
                "text",
                "ntext",
                "datetime",
                "date",
                "time",
                "money",
                "numeric",
                "decimal",
                "uniqueidentifier",
                "timestamp"
            };
        }

        protected override List<Block> Blocks()
        {
            List<Block> output = new List<Block>();

            output.Add(new Block("'", "'", Color.OrangeRed));
            output.Add(new Block(@"--", "\n", Color.Green));
            output.Add(new Block("/*", "*/", Color.Green));

            return output;
        }
    }
}
