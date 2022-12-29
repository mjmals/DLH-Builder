using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHBuilder.Components.EditorGrids
{
    public class EditorGridDatasourceDefinition
    {
        public EditorGridDatasourceDefinition(object items, string displayProperty = null)
        {
            Items = items;
            DisplayProperty = displayProperty;
        }

        public object Items { get; set; }

        public string DisplayProperty { get; set; }

        public Dictionary<string, object> Values()
        {
            Dictionary<string, object> output = new Dictionary<string, object>();

            if(Items is Enum)
            {
                foreach(string value in Enum.GetNames(Items.GetType()))
                {
                    output.Add(value, value);
                }

                return output;
            }

            if(Items is Array)
            {
                foreach(var item in (Array)Items)
                {
                    PropertyInfo displayProp = item.GetType().GetProperty(DisplayProperty);
                    output.Add(displayProp.GetValue(item).ToString(), item);
                }
            }

            return output;
        }
    }
}
