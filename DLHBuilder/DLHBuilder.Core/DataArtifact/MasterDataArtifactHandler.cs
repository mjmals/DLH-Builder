using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    static class MasterDataArtifactHandler
    {
        public static void PostArtifact(DataArtifact artifact)
        {
            MasterDataArtifact master = new MasterDataArtifact();
            master.ID = Guid.NewGuid();
            master.Name = artifact.Name;
            master.Schema = new DataArtifactSchemaItemCollection();

            Artifacts.Add(master);
            artifact.MasterDataArtifactID = master.ID;
        }

        public static void PostSchemaItem(Guid masterartifactid, DataArtifactSchemaItem item)
        {
            MasterDataArtifact artifact = Find(masterartifactid);
            DataArtifactSchemaItem master = new DataArtifactSchemaItem();
            master.ID = Guid.NewGuid();
            master.Name = item.Name;
            master.DataType = item.DataType;

            artifact.Schema.Add(item);
            item.MasterSchemaItemID = master.ID;
        }

        public static DataArtifactCollection Artifacts = new DataArtifactCollection();

        public static MasterDataArtifact Find(Guid id)
        {
            return (MasterDataArtifact)Artifacts.FirstOrDefault(x => x.ID == id);
        }
    }
}
