using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactCompiler
    {
        public DataArtifactCompiler(DataArtifact artifact, IDataApplication application, IDataStage stage, Project project)
        {
            Artifact = artifact;
            Application = application;
            Stage = stage;
            Project = project;
            DataSources = artifact.DataSources.ToArray();
            SetArtifacts();
        }

        public DataArtifact Artifact { get; set; }

        public IDataSource[] DataSources { get; set; }

        public IDataApplication Application { get; set; }

        public IDataStage Stage { get; set; }

        public ICompiledDataArtifact[] Artifacts { get; set; }

        public Project Project { get; set; }

        void SetArtifacts()
        {
            List<ICompiledDataArtifact> artifacts = new List<ICompiledDataArtifact>();

            foreach (DataStage stage in Application.Stages.OrderBy(x => x.Ordinal))
            {
                foreach (DataArtifactReference reference in stage.ArtifactReferences.Where(x => x.DataArtifactID == Artifact.ID))
                {
                    if (Application is SQLDataApplication)
                    {
                        artifacts.Add(new SQLCompiledDataArtifact(Artifact, (MSSQLDataStage)stage, reference, Project));
                        continue;
                    }

                    if (Application is DataLakeDataApplication)
                    {
                        artifacts.Add(new ADLSCompiledDataArtifact(Artifact, (ADLSDataStage)stage, reference, Project));
                        continue;
                    }
                }
            }

            Artifacts = artifacts.ToArray();
        }
    }
}
