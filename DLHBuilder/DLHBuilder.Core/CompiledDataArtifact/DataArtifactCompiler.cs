using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactCompiler
    {
        public DataArtifactCompiler(DataArtifact artifact, IDataApplication application, IDataStage stage)
        {
            Artifact = artifact;
            Application = application;
            Stage = stage;
            SetArtifacts();
        }

        DataArtifact Artifact { get; set; }

        IDataApplication Application { get; set; }

        IDataStage Stage { get; set; }

        public ICompiledDataArtifact[] Artifacts { get; set; }

        void SetArtifacts()
        {
            List<ICompiledDataArtifact> artifacts = new List<ICompiledDataArtifact>();

            foreach(DataArtifactReference reference in Application.Stages.SelectMany(x => x.ArtifactReferences).Where(x => x.DataArtifactID == Artifact.ID))
            {
                if(Application is SQLDataApplication)
                {
                    artifacts.Add(new SQLCompiledDataArtifact(Artifact, (MSSQLDataStage)Stage, reference));
                    continue;
                }

                if(Application is DataLakeDataApplication)
                {
                    continue;
                }
            }

            Artifacts = artifacts.ToArray();
        }
    }
}
