using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class MSSQLProject : Project
    {
        public MSSQLProject()
        {
            Name = "<New MSSQL Project>";
            Stages.Add(new MSSQLDataStage() { ID = Guid.NewGuid(), Name = "Loading Bay", Schema = "LBY" });
            Stages.Add(new MSSQLDataStage() { ID = Guid.NewGuid(), Name = "Dimension", Schema = "DIM" });
            Stages.Add(new MSSQLDataStage() { ID = Guid.NewGuid(), Name = "Fact", Schema = "FACT" });

            ArtifactGroups.Add(new DataArtifactGroup() { Name = "Source" });
            ArtifactGroups.Add(new DataArtifactGroup() { Name = "Dimension" });
            ArtifactGroups.Add(new DataArtifactGroup() { Name = "Fact" });
        }
    }
}
