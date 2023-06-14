using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace DLHApp.Model.CodeParsers
{
    public class SqlServerCodeParser : CodeParser
    {
        public SqlServerCodeParser(string input) : base(input)
        {

        }

        public override string[] FileExtensions => new string[] { ".sql" };

        IList<TSqlBatch> GetSqlBatches()
        {
            TSql150Parser parser = new TSql150Parser(false);
            IList<ParseError> parseErrors;
            TSqlFragment result = parser.Parse(new StringReader(Input), out parseErrors);
            TSqlScript sqlScript = (TSqlScript)result;
            IList<TSqlBatch> sqlBatches = sqlScript.Batches;
            return sqlBatches;
        }

        IList<TSqlStatement> GetPrimarySqlBatchStatements()
        {
            return GetSqlBatches()[0].Statements;
        }

        CodeParserOutput ParseSqlColumns()
        {
            CodeParserOutput output = new CodeParserOutput();
            IList<TSqlStatement> statements = GetPrimarySqlBatchStatements();

            foreach(SelectStatement statement in statements)
            {
                IList<SelectElement> elements = ((QuerySpecification)statement.QueryExpression).SelectElements;

                foreach(SelectScalarExpression element in elements)
                {
                    string identifier = element.ColumnName == null ? string.Empty : element.ColumnName.Value;
                    string expression = string.Empty;

                    for(int i = element.FirstTokenIndex; i <= element.LastTokenIndex; i++)
                    {
                        expression += element.ScriptTokenStream[i].Text;
                    }

                    int startPos = Input.IndexOf(expression);
                    expression = expression.Replace(" AS " + identifier, "");
                    int endPos = startPos + expression.Length;

                    CodeParserOutputLine parserLine = new CodeParserOutputLine()
                    {
                        Identifier = identifier,
                        Expression = expression,
                        StartPosition = startPos,
                        EndPosition = endPos
                    };

                    output.Add(parserLine);
                }
            }

            return output;
        }

        public override CodeParserOutput Parse()
        {
            return ParseSqlColumns();
        }
    }
}
