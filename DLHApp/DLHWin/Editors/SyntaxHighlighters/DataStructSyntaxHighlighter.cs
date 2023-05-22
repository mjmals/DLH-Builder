using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataTypes;
using System.Reflection;

namespace DLHWin.Editors.SyntaxHighlighters
{
    internal class DataStructSyntaxHighlighter : SyntaxHighlighter
    {
        protected override List<Block> Blocks()
        {
            List<Block> output = new List<Block>();

            output.Add(new Block("\"", "\"", Color.OrangeRed));

            return output;
        }

        protected override Dictionary<string, Color> Tokens()
        {
            Dictionary<string, Color> output = new Dictionary<string, Color>();

            output.Add("StructType", Color.Purple);
            output.Add("StructField", Color.Purple);
            output.Add("StructConfig", Color.Purple);
            output.Add("StructRelationship", Color.Purple);
            output.Add("StructJoin", Color.Purple);
            output.Add("True", Color.Purple);
            output.Add("False", Color.Purple);

            Type[] dataTypes = typeof(IDataType).Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(IDataType)) && x.IsInterface == false && x.IsAbstract == false).ToArray();

            foreach(Type dt in dataTypes)
            {
                IDataType dataType = (IDataType)Activator.CreateInstance(dt);

                foreach(string displayName in dataType.DisplayNames)
                {
                    output.Add(", " + displayName, Color.Purple);
                    output.Add("," + displayName, Color.Purple);
                }
            }

            return output;
        }
    }
}
