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

        protected virtual BuilderCollectionItemType CollectionType => BuilderCollectionItemType.File;

        [JsonIgnore]
        public EventHandler CollectionSaved;

        void OnCollectionSaved()
        {
            CollectionSaved?.Invoke(null, null);
        }

        [JsonIgnore]
        public EventHandler CollectionAdded;

        public new void Add(T item)
        {
            base.Add(item);
            CollectionAdded?.Invoke(item, null);
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
        }

        protected virtual void SaveFiles()
        {
            foreach(T item in this)
            {
                Type objecttype = item.GetType();
                string objecttitle = (string)objecttype.GetProperty(FileNameProperty).GetValue(item);
                string subfolder = CollectionType == BuilderCollectionItemType.FolderAndFile ? objecttitle : string.Empty;

                if(!string.IsNullOrEmpty(subfolder) && !Directory.Exists(Path.Combine(DirectoryPath, subfolder)))
                {
                    Directory.CreateDirectory(Path.Combine(DirectoryPath, subfolder));
                }

                string filename = Path.Combine(DirectoryPath, subfolder, string.Format("{0}.{1}.json", objecttitle, objecttype.Name));

                new FileMetadataExtractor(filename).Write(item);
            }
        }

        protected virtual void SaveFolders()
        {
            foreach(T item in this)
            {
                string directory = Path.Combine(DirectoryPath, (string)item.GetType().GetProperty(FileNameProperty).GetValue(item));

                if(!Directory.Exists(directory))
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
            foreach (string file in Directory.GetFiles(path))
            {
                string filename = Path.GetFileNameWithoutExtension(file);
                int startpos = filename.LastIndexOf(".") + 1;

                string filetype = string.Concat(typeof(DataConnectionCollection).Namespace, ".", filename.Substring(startpos));

                Type type = Type.GetType(filetype);

                FileMetadataExtractor extractor = new FileMetadataExtractor(file);
                Add((T)extractor.LoadFile(type));
            }
        }
    }
}
