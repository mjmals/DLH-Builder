using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;

namespace DLHApp.Model.DataApplications
{
    public abstract class DataApplication : ModelItem, IModelItem
    {
        protected override string OutputPath()
        {
            return Path.Combine("Data Applications", Name, string.Format("{0}.{1}", Name, OutputExtension));
        }

        protected override string OutputExtension => "json";

        [JsonIgnore]
        public virtual string? DisplayName { get; }

        public static Type[] ApplicationTypes
        {
            get
            {
                Type baseType = typeof(DataApplication);
                return baseType.Assembly.GetTypes().Where(x => x.IsAssignableTo(baseType) && x.IsAbstract == false && x.IsInterface == false).ToArray();
            }
        }

        public static DataApplication Load(string path)
        {
            Type[] appTypes = ApplicationTypes;

            foreach(Type appType in appTypes)
            {
                DataApplication app = Activator.CreateInstance<DataApplication>();

                if(path.EndsWith(app.OutputExtension))
                {
                    return (DataApplication)JsonConvert.DeserializeObject(File.ReadAllText(path), appType);
                }
            }

            return null;
        }
    }
}
