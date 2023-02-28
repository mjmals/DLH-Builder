using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DLHApp.Model.DataStructs;
using DLHApp.Model.DataStages;

namespace DLHApp.Model.DataStructReferences
{
    public class DataStructReference : ModelItem, IModelItem
    {
        public override string BasePath => GetBasePath(string.Empty);

        protected override string GetBasePath(string basePath)
        {
            string output = Path.Combine(string.IsNullOrEmpty(FolderPath) ? string.Empty : FolderPath, string.IsNullOrEmpty(Name) ? string.Empty : Name);

            if(!string.IsNullOrEmpty(output))
            {
                string[] outputPathItems = output.Split(@"\");

                if(outputPathItems.Last() == outputPathItems[outputPathItems.Length - 2])
                {
                    output = string.Join('\\', outputPathItems.Take(outputPathItems.Length - 1));
                }
            }

            if (string.IsNullOrEmpty(output))
            {
                return "Data Applications";
            }

            return output;
        }

        public override string OutputExtension => "ref.json";

        public string SourceDataStruct { get; set; }

        public DataStructFieldReferenceCollection Fields { get; set; }

        public override void Save()
        {
            base.Save();

            string[] subDirs = new string[] { "Definitions", "Load Steps" };

            foreach (string subDir in subDirs)
            {
                string dirName = Path.Combine(Path.GetDirectoryName(OutputPath()), subDir);

                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
            }
        }

        public static DataStructReference New()
        {
            DataStructReference output = new DataStructReference();
            output.SourceDataStruct = string.Empty;
            output.Fields = new DataStructFieldReferenceCollection();

            return output;
        }

        public static DataStructReference Load(string path)
        {
            DataStructReference output = JsonConvert.DeserializeObject<DataStructReference>(File.ReadAllText(path));
            output.Name = Path.GetFileNameWithoutExtension(path).Replace(".ref", "");
            output.FolderPath = Path.GetDirectoryName(path).Replace(Environment.CurrentDirectory + @"\", "");

            return output;
        }

        public override TemplateModelItem GetTemplateItems()
        {
            TemplateModelItem output = base.GetTemplateItems();

            DataStruct ds = DataStruct.Load(SourceDataStruct);
            output.Add("DataStruct", ds);

            DataStage stg = ParentStage();
            output.Add("Stage", stg);

            output.Add("RefPath", Path.GetDirectoryName(Path.GetDirectoryName(this.BasePath)).Replace(stg.SourcePath + @"\", ""));

            Dictionary<string, string> definitions = new Dictionary<string, string>();

            foreach(string defFile in Directory.GetFiles(Path.Combine(FolderPath, "Definitions"), "*.def.*"))
            {
                definitions.Add(Path.GetFileNameWithoutExtension(defFile), File.ReadAllText(defFile));
            }

            output.Add("Definitions", definitions);

            Dictionary<string, Dictionary<string, string>> loadSteps = new Dictionary<string, Dictionary<string, string>>();
            string loadStepDir = Path.Combine(FolderPath, "Load Steps");

            foreach (string stepFile in Directory.GetFiles(loadStepDir, "*.loadstep.*", SearchOption.AllDirectories).OrderBy(x => x))
            {
                string stepGroup = Path.GetDirectoryName(stepFile).Replace(loadStepDir + @"\", "");

                if (!loadSteps.ContainsKey(stepGroup))
                {
                    loadSteps.Add(stepGroup, new Dictionary<string, string>());
                }

                KeyValuePair<string, Dictionary<string, string>> stepKey = loadSteps.FirstOrDefault(x => x.Key == stepGroup);
                Dictionary<string, string> stepGroupEntries = stepKey.Value;
                stepGroupEntries.Add(Path.GetFileNameWithoutExtension(stepFile).Replace(".loadstep", ""), File.ReadAllText(stepFile));
            }

            output.Add("LoadSteps", loadSteps);

            return output;
        }

        public DataStage ParentStage()
        {
            List<string> parentPathItems = BasePath.Split(@"\").ToList();
            int stagePos = parentPathItems.IndexOf("Stages");
            string stageDir = string.Join(@"\", parentPathItems.Take(stagePos + 2));

            string stageFile = Directory.GetFiles(stageDir).FirstOrDefault(x => x.EndsWith("stg.json"));

            if(!string.IsNullOrEmpty(stageFile))
            {
                return DataStage.Load(stageFile);
            }

            return null;
        }
    }
}
