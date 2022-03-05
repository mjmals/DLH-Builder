using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public abstract class BuilderCollection<T> : List<T>
    {
        protected virtual string DirectoryName => string.Empty;

        protected virtual BuilderCollectionItemType CollectionType { get; }

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

        internal void Save(string path)
        {
            DirectoryPath = Path.Combine(path, DirectoryName);

            switch(CollectionType)
            {
                case BuilderCollectionItemType.File:
                    SaveFiles();
                    break;
                case BuilderCollectionItemType.Folder:
                    break;
            }

            OnCollectionSaved();
        }

        protected virtual void SaveFiles()
        {
            foreach(T item in this)
            {

            }
        }

        public static T Load<T>(string path)
        {
            return default(T);
        }
    }
}
