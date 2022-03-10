using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DLHBuilder
{
    public class DataApplicationCollection : BuilderCollection<IDataApplication>
    {
        protected override string DirectoryName => "Data Applications";

        protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.FolderAndFile;

        internal override void Save(string path)
        {
            base.Save(path);

            foreach(IDataApplication application in this)
            {
                application.Stages.Save(Path.Combine(path, DirectoryName, application.Name));
            }
        }

        internal override void Load(string path)
        {
            base.Load(path);

            foreach (IDataApplication application in this)
            {
                application.Stages.Load(Path.Combine(path, DirectoryName, application.Name));
            }
        }
    }
}