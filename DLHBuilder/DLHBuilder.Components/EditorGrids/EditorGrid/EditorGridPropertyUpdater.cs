using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHBuilder.Components.EditorGrids
{
    class EditorGridPropertyUpdater
    {
        public static void Run(object baseObject, string propertyName, object updateValue)
        {
            PropertyInfo baseProperty = baseObject.GetType().GetProperty(propertyName);

            if(baseProperty.PropertyType.IsEnum)
            {
                baseProperty.SetValue(baseObject, Enum.Parse(baseProperty.PropertyType, (string)updateValue));
                return;
            }

            baseProperty.SetValue(baseObject, updateValue);
        }
    }
}
