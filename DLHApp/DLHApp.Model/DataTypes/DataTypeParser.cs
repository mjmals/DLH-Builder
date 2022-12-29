using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class DataTypeParser
    {
        public DataTypeParser(string dataTypeText)
        {
            DataTypeText = dataTypeText;
        }

        string DataTypeText { get; set; }

        public IDataType Parse()
        {
            string dataTypeName = DataTypeText.Substring(0, DataTypeText.IndexOf("("));
            string[] dataTypeParams = DataTypeText.Substring(DataTypeText.IndexOf("(") + 1, DataTypeText.LastIndexOf(")") - (DataTypeText.IndexOf("(") + 1)).Split(",").ToArray();

            Type[] dataTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => x.IsAssignableTo(typeof(IDataType)) && x.IsInterface == false && x.IsAbstract == false).ToArray();

            foreach (Type dataType in dataTypes)
            {
                IDataType type = (IDataType)Activator.CreateInstance(dataType);

                if(type.DisplayNames.Contains(dataTypeName))
                {
                    if (dataTypeParams[0].Length == 0)
                    {
                        return (IDataType)Activator.CreateInstance(dataType);
                    }

                    return (IDataType)Activator.CreateInstance(dataType, dataTypeParams);
                }

            }

            return null;
        }
    }
}
