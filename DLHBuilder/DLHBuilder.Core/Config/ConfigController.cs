using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DLHBuilder.Config
{
    public static class ConfigController
    {
        static string ConfigPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "DLH Builder", "Config.json");

        public static void SetValue(string item, string value)
        {
            ConfigData config = new ConfigData();

            if(!File.Exists(ConfigPath))
            {
                config.Items = new List<ConfigItem>();
                WriteToConfigFile(config);
            }

            config = LoadFromConfigFile();
            CreateIfNotExists(item, config);
            config.GetItem(item).Value = value;
            WriteToConfigFile(config);
        }

        static void WriteToConfigFile(ConfigData config)
        {
            string directory = Path.GetDirectoryName(ConfigPath);

            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (FileStream stream = new FileStream(ConfigPath, FileMode.OpenOrCreate))
            {
                stream.SetLength(0);

                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string data = JsonConvert.SerializeObject(config, Formatting.Indented);
                    writer.Write(data);
                }
            }
        }

        static void CreateIfNotExists(string item, ConfigData config)
        {
            if (!config.Items.Exists(x => x.Name == item))
            {
                config.Items.Add(new ConfigItem() { Name = item, Value = string.Empty });
            }

            WriteToConfigFile(config);
        }

        static ConfigData LoadFromConfigFile()
        {
            using (FileStream stream = new FileStream(ConfigPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string data = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<ConfigData>(data);
                }
            }
        }

        public static string GetValue(string item)
        {
            ConfigData config = LoadFromConfigFile();
            CreateIfNotExists(item, config);
            return config.GetItem(item).Value;
        }
    }
}
