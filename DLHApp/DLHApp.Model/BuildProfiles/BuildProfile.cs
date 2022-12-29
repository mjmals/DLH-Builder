using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHApp.Model.BuildProfiles
{
    public class BuildProfile : ModelItem, IModelItem
    {
        public BuildProfileStageCollection Stages { get; set; }

        public override string BasePath => "Build Profiles";

        [JsonIgnore]
        public BuildProfileUserConfig UserConfig { get; set; }

        public override void Save()
        {
            if(Stages == null)
            {
                Stages = new BuildProfileStageCollection();
            }

            base.Save();

            string userConfigData = JsonConvert.SerializeObject(UserConfig == null ? new BuildProfileUserConfig() : UserConfig, Formatting.Indented);
            string outputPath = OutputPath().Replace(".json", ".local.json");
            CreateFile(outputPath, userConfigData);
        }

        public new static BuildProfile New()
        {
            return new BuildProfile()
            {
                Stages = new BuildProfileStageCollection() { new BuildProfileStage() { Name = "Default", OutputPath = string.Empty, Templates = new string[] { @"Samples\*" } } },
                UserConfig = new BuildProfileUserConfig()
            };
        }

        public static BuildProfile Load(string profileName)
        {
            string profilePath = Path.Combine("Build Profiles", profileName);

            if(!File.Exists(profilePath + ".json"))
            {
                Console.WriteLine("Build Profile not found");
                return null;
            }

            BuildProfile output = JsonConvert.DeserializeObject<BuildProfile>(File.ReadAllText(profilePath + ".json"));
            output.UserConfig = JsonConvert.DeserializeObject<BuildProfileUserConfig>(File.ReadAllText(profilePath + ".local.json"));

            return output;
        }
    }
}
