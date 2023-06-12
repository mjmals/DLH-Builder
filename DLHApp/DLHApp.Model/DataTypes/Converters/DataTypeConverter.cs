using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHApp.Model.DataTypes.Converters
{
    public abstract class DataTypeConverter : IDataTypeConverter
    {
        public abstract string[] SourceTypeNames { get; }

        public abstract Type[] DataTypes { get; }

        public virtual DataTypeConverterType ConverterType { get; }

        public abstract IDataType Import(string dataType);

        public abstract string Export(IDataType dataType);

        public virtual string GetDefaultValue(IDataType dataType)
        {
            return ((DataType)Activator.CreateInstance(DataTypes.FirstOrDefault(x => x.IsInterface == false && x.IsAbstract == false))).DefaultValue.ToString();
        }

        static Type[] ConverterTypes()
        {
            Type baseType = typeof(DataTypeConverter);
            return baseType.Assembly.GetTypes().Where(x => x.IsAssignableTo(baseType) && x.IsInterface == false && x.IsAbstract == false).ToArray();
        }

        public static IDataTypeConverter GetConverter(string dataType)
        {
            Type[] converterTypes = ConverterTypes();

            foreach(Type converterType in converterTypes)
            {
                IDataTypeConverter converter = (IDataTypeConverter)Activator.CreateInstance(converterType);

                if (converter.SourceTypeNames.Where(x => dataType.ToLower().StartsWith(x)).Count() > 0)
                {
                    return converter;
                }
            }

            return null;
        }

        public static IDataTypeConverter GetConverter(IDataType dataType, DataTypeConverterType targetType)
        {
            Type[] converterTypes = ConverterTypes();

            foreach (Type converterType in converterTypes)
            {
                IDataTypeConverter converter = (IDataTypeConverter)Activator.CreateInstance(converterType);

                if (converter.DataTypes.Contains(dataType.GetType()) && converter.ConverterType == targetType)
                {
                    return converter;
                }
            }

            return null;
        }
    }
}
