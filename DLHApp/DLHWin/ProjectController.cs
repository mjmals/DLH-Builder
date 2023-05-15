using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHApp.Model.Projects;
using DLHApp.Templates;

namespace DLHWin
{
    internal class ProjectController : Project
    {
        List<IModelItem> ChangedFiles = new List<IModelItem>();

        void AddChangedFile(ModelItem modelItem)
        {
            ChangedFiles.Add(modelItem);
        }

        internal ProjectDirectory Directory { get; set; }

        public override void Save()
        {
            int changedFileCount = ChangedFiles.Count;
            ChangedFiles.ForEach((x) => x.Save());
            ChangedFiles.Clear();
            MessageBox.Show("Project Saved ({0} files updated)", changedFileCount.ToString());
        }

        public new static ProjectController Load(string path)
        {
            string projectName = new DirectoryInfo(path).Parent.Name;
            string projectRoot = new DirectoryInfo(path).Parent.FullName;

            ProjectController output = new ProjectController()
            {
                Name = projectName,
                BasePath = projectRoot,
                Directory = new ProjectDirectory(projectRoot)
            };

            return output;
        }

        public static void Create(string fileName)
        {
            Environment.CurrentDirectory = fileName;
            Initialize();
            TemplateImporter.Run();
        }
    }
}
