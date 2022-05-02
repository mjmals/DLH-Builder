using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DLHBuilder.DataType.Converters;

namespace DLHBuilder
{
    public class DataTypeConverter
    {
        public DataTypeConverter(Type sourceConnectionType, string sourceType)
        {
            SourceConnectionType = sourceConnectionType;
            SourceType = sourceType;
        }

        Type SourceConnectionType { get; set; }

        string SourceType { get; set; }

        IDataTypeConverter GetConverter()
        {
            var converters = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(s => s.IsAssignableTo(typeof(IDataTypeConverter)) && !s.IsInterface)
                .ToArray();

            foreach(Type converterType in converters)
            {
                IDataTypeConverter converter = (IDataTypeConverter)Activator.CreateInstance(converterType);

                if (converter.SourceTypes.ContainsKey(SourceConnectionType))
                {
                    string[] sourceTypes = converter.SourceTypes[SourceConnectionType];

                    if(sourceTypes.Any(x => SourceType.ToLower().Contains(x)))
                    {
                        return converter;
                    }
                }
            }

            return null;
        }

        public IDataType GetDataType()
        {
            return GetConverter().ConvertSourceType(SourceType);
        }
    }
}
