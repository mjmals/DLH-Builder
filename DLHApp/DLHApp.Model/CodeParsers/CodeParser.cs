using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHApp.Model.CodeParsers
{
    public abstract class CodeParser
    {
        public CodeParser(string input)
        {
            Input = input;
        }

        public string Input { get; set; }

        public virtual string[] FileExtensions => new string[0];

        public virtual CodeParserOutput Parse()
        {
            return new CodeParserOutput();
        }

        public static CodeParser GetCodeParser(string fileName)
        {
            Type[] parserTypes = typeof(CodeParser).Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(CodeParser)) && x.IsAbstract == false).ToArray();
            string fileContent = File.ReadAllText(fileName);

            foreach(Type parserType in parserTypes)
            {
                CodeParser parser = (CodeParser)Activator.CreateInstance(parserType, new object[] { fileContent });

                if(parser.FileExtensions.Contains(Path.GetExtension(fileName)))
                {
                    return parser;
                }
            }

            return null;
        }
    }
}
