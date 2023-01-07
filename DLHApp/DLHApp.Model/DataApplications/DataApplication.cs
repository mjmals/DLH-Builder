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

        public override string OutputExtension => "json";

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

        public override void Save()
        {
            base.Save();

            string[] subDirs = new string[] { "Stages" };

            foreach(string subDir in subDirs)
            {
                string dirName = Path.Combine(Path.GetDirectoryName(OutputPath()), subDir);

                if(!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
            }
        }

        public static DataApplication Load(string path)
        {
            Type[] appTypes = ApplicationTypes;

            foreach(Type appType in appTypes)
            {
                DataApplication app = (DataApplication)Activator.CreateInstance(appType);

                if(path.EndsWith(app.OutputExtension))
                {
                    DataApplication output = (DataApplication)JsonConvert.DeserializeObject(File.ReadAllText(path), appType);
                    output.Name = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(path));
                    return output;
                }
            }

            return null;
        }
    }
}
