using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin
{
    public class ProjectDirectory : List<ProjectDirectoryItem>
    {
        public ProjectDirectory(string projectRoot)
        {
            Root = projectRoot;
            LoadDirectory();
        }

        string Root { get; set; }

        public void LoadDirectory()
        {
            GetDirectoryItems(Root);
        }

        void GetDirectoryItems(string directory)
        {
            string searchDir = Path.Combine(Root, directory);

            foreach (string subdir in Directory.GetDirectories(searchDir))
            {
                DirectoryInfo dir = new DirectoryInfo(subdir);

                ProjectDirectoryItem dirItem = new ProjectDirectoryItem()
                {
                    Name = dir.Name,
                    Parent = directory.Replace(Root, ""),
                    Type = ProjectDirectoryItemType.Folder
                };

                this.Add(dirItem);
                GetDirectoryItems(dirItem.FullPath);
            }

            foreach(string file in Directory.GetFiles(searchDir))
            {
                FileInfo fileInfo = new FileInfo(file);

                if(fileInfo.Name == "project.json")
                {
                    continue;
                }

                ProjectDirectoryItem dirItem = new ProjectDirectoryItem()
                {
                    Name = Path.GetFileNameWithoutExtension(file),
                    Extension = Path.GetExtension(file),
                    Parent = directory.Replace(Root, ""),
                    Type = ProjectDirectoryItemType.File
                };

                if(dirItem.Name.Contains("."))
                {
                    string[] nameSplit = dirItem.Name.Split(".");
                    dirItem.Name = nameSplit[0];
                    dirItem.Extension = "." + string.Join(".", nameSplit.TakeLast(nameSplit.Count() - 1)) + dirItem.Extension;
                }

                this.Add(dirItem);
            }
        }
    }
}
