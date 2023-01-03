using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;

namespace DLHApp.Model
{
    public abstract class ModelItem : IModelItem
    {
        [JsonIgnore]
        public virtual string? Name { get; set; }

        [JsonIgnore]
        public virtual string BasePath => string.Empty;

        [JsonIgnore]
        public virtual string? SourcePath { get; set; }

        [JsonIgnore]
        public virtual string? FolderPath { get; set; }

        protected virtual string GetBasePath(string basePath)
        {
            // if the calling application working directory contains basePath variable value then
            // set the path to be the root of the working directory
            if (Environment.CurrentDirectory.Contains(basePath))
            {
                return string.Empty;
            }

            return Path.Combine(basePath, string.IsNullOrEmpty(FolderPath) ? string.Empty : FolderPath);
        }

        protected virtual string OutputPath()
        {
            string outputPath = SourcePath;

            if (string.IsNullOrEmpty(outputPath))
            {
                string basePath = string.IsNullOrEmpty(BasePath) ? Environment.CurrentDirectory : BasePath;
                outputPath = Path.Combine(basePath, string.Format("{0}.{1}", Name, OutputExtension));
            }

            return outputPath;
        }

        protected virtual string OutputExtension => "json";

        protected virtual string OutputContent()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public virtual void Save()
        {
            string output = OutputContent();
            CreateFile(OutputPath(), output);
        }

        protected virtual void CreateFile(string outputPath, string output)
        {
            if (!Directory.Exists(Path.GetDirectoryName(outputPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
            }

            using (FileStream fs = new FileStream(outputPath, FileMode.OpenOrCreate))
            {
                fs.SetLength(0);

                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(output);
                }
            }
        }

        public static IModelItem New()
        {
            return null;
        }
    }
}
