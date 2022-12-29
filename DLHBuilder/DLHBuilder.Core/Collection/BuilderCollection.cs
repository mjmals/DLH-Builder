using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

namespace DLHBuilder
{
    public abstract class BuilderCollection<T> : List<T>
    {
        protected virtual string DirectoryName => string.Empty;

        protected virtual string FileNameProperty => "Name";

        protected virtual string SubfolderProperty => string.Empty;

        protected virtual BuilderCollectionItemType CollectionType => BuilderCollectionItemType.File;

        protected virtual string FileSearchPattern => "*.json";

        string GetFileNamePropertyValue(object item)
        {
            string output = string.Empty;

            Type objectType = item.GetType();
            output = (string)objectType.GetProperty(FileNameProperty).GetValue(item);

            output = output.Replace(@"\", "__").Replace("/", "__").Replace("?", "");

            return output;
        }

        [JsonIgnore]
        public EventHandler CollectionSaved;

        void OnCollectionSaved()
        {
            CollectionSaved?.Invoke(null, null);
        }

        [JsonIgnore]
        public EventHandler CollectionAdded;

        [JsonIgnore]
        public EventHandler CollectionModified;

        [JsonIgnore]
        public EventHandler CollectionRemoved;

        public new void Add(T item)
        {
            base.Add(item);
            CollectionAdded?.Invoke(item, null);
            CollectionModified?.Invoke(item, null);
        }

        public new void Remove(T item)
        {
            base.Remove(item);
            CollectionModified?.Invoke(item, null);
            CollectionRemoved?.Invoke(item, null);
        }

        string DirectoryPath
        {
            get => _directorypath;
            set
            {
                _directorypath = value;

                if(!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }
            }
        }

        string _directorypath { get; set; }

        internal virtual void Save(string path)
        {
            DirectoryPath = Path.Combine(path, DirectoryName);

            switch(CollectionType)
            {
                case BuilderCollectionItemType.File:
                    SaveFiles();
                    break;
                case BuilderCollectionItemType.Folder:
                    SaveFolders();
                    break;
                case BuilderCollectionItemType.FolderAndFile:
                    SaveFiles();
                    break;
            }

            OnCollectionSaved();
            CleanUp(path);
        }

        protected virtual void SaveFiles()
        {
            foreach(T item in this)
            {
                Type objectType = item.GetType();
                string objectTitle = GetFileNamePropertyValue(item);
                string subFolder = SubfolderProperty != string.Empty ? (string)objectType.GetProperty(SubfolderProperty).GetValue(item) : string.Empty;
                string directory = !string.IsNullOrEmpty(subFolder) ? Path.Combine(DirectoryPath, subFolder.Replace(".", @"\")) : DirectoryPath;

                if(!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string filename = Path.Combine(directory, string.Format("{0}.{1}.json", objectTitle, objectType.Name));

                new FileMetadataExtractor(filename).Write(item);
            }
        }

        protected virtual void SaveFolders()
        {
            foreach(T item in this)
            {
                Type objectType = item.GetType();
                string subFolder = SubfolderProperty != string.Empty ? (string)objectType.GetProperty(SubfolderProperty).GetValue(item) : string.Empty;
                string directory = !string.IsNullOrEmpty(subFolder) ? Path.Combine(DirectoryPath, subFolder.Replace(".", @"\")) : DirectoryPath;

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
        }

        internal virtual void Load(string path)
        {
            DirectoryPath = Path.Combine(path, DirectoryName);

            if (CollectionType == BuilderCollectionItemType.Folder)
            {
                foreach(string directory in Directory.GetDirectories(DirectoryPath))
                {
                    var item = Activator.CreateInstance(typeof(T));
                    item.GetType().GetProperty(FileNameProperty).SetValue(item, new DirectoryInfo(directory).Name);
                    Add((T)item);
                }

                return;
            }

            if (CollectionType == BuilderCollectionItemType.FolderAndFile)
            {
                foreach(string directory in Directory.GetDirectories(DirectoryPath))
                {
                    LoadFiles(directory);
                }

                return;
            }

            if (CollectionType == BuilderCollectionItemType.File)
            {
                LoadFiles(DirectoryPath);
            }
        }

        void LoadFiles(string path)
        {
            foreach (string file in Directory.GetFiles(path, FileSearchPattern, SearchOption.AllDirectories))
            {
                string filename = Path.GetFileNameWithoutExtension(file);
                int startpos = filename.LastIndexOf(".") + 1;

                string filetype = string.Concat(typeof(DataConnectionCollection).Namespace, ".", filename.Substring(startpos));

                Type type = Type.GetType(filetype);

                FileMetadataExtractor extractor = new FileMetadataExtractor(file);
                Add((T)extractor.LoadFile(type));
            }
        }

        internal virtual void CleanUp(string path)
        {
            switch(CollectionType)
            {
                case BuilderCollectionItemType.FolderAndFile:
                    CleanUpFiles(path);
                    CleanUpFolders(path);
                    break;
                case BuilderCollectionItemType.Folder:
                    CleanUpFolders(path);
                    break;
                case BuilderCollectionItemType.File:
                    CleanUpFiles(path);
                    break;
            }
        }

        void CleanUpFolders(string path)
        {
            path = Path.Combine(path, DirectoryPath);
            string[] folders = Directory.GetDirectories(path);

            List<string> itemList = this.Select(x => Path.Combine(path, x.GetType().GetProperty(SubfolderProperty).GetValue(x).ToString().Replace(".", @"\"))).ToList();

            foreach(string folder in folders)
            {
                if(!itemList.Contains(folder) && itemList.Where(x => x.StartsWith(folder)).Count() == 0 && Directory.Exists(folder))
                {
                    Directory.Delete(folder, true);
                }
            }
        }

        void CleanUpFiles(string path)
        {
            path = Path.Combine(path, DirectoryPath);
            var files = Directory.GetFiles(path, FileSearchPattern, SearchOption.AllDirectories)
                .Select(file => new { Name = GetFileObjectName(file), Type = GetFileObjectType(file), FileName = file });

            var itemList = this.Select(x => GetFileNamePropertyValue(x));
            var cleanUpFiles = files.Where(file => itemList.Contains(file.Name) == false);

            foreach(var file in cleanUpFiles)
            {
                File.Delete(file.FileName);
            }
        }

        Type GetFileObjectType(string file)
        {
            return this.GetType().Assembly.GetTypes().Where(x => x.Name == Path.GetExtension(Path.GetFileNameWithoutExtension(file)).Substring(1)).FirstOrDefault();
        }

        string GetFileObjectName(string file)
        {
            return Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(file));
        }
    }
}
