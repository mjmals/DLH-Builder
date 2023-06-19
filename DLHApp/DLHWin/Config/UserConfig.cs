using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;

namespace DLHWin.Config
{
    internal class UserConfig
    {
        public UserConfigRecentProjectCollection RecentProjects
        {
            get
            {
                if(recentProjects == null)
                {
                    recentProjects = new UserConfigRecentProjectCollection();
                }

                recentProjects.FileAdded += Save;

                return recentProjects;
            }
            set
            {
                recentProjects = value;
                Save();
            }
        }

        [JsonIgnore]
        UserConfigRecentProjectCollection recentProjects { get; set; }

        static string TargetFolder => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config");

        static string TargetFile => Path.Combine(TargetFolder, "user.json");

        public void Save(object sender = null, EventArgs e = null)
        {
            if(!Directory.Exists(TargetFolder))
            {
                Directory.CreateDirectory(TargetFolder);
            }

            using (FileStream fs = new FileStream(TargetFile, FileMode.OpenOrCreate))
            {
                fs.SetLength(0);

                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
                }
            }
        }

        public static UserConfig Load()
        {
            if(!File.Exists(TargetFile))
            {
                UserConfig config = new UserConfig();
                config.RecentProjects = new UserConfigRecentProjectCollection();
                config.Save();
            }

            return JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText(TargetFile));
        }
    }
}
