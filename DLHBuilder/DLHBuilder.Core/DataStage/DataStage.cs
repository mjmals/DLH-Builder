using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataStage
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DataStageLevelCollection Levels 
        { 
            get
            {
                if(levels == null)
                {
                    levels = new DataStageLevelCollection();
                }

                return levels;
            }
            set => levels = value; 
        }

        private DataStageLevelCollection levels { get; set; }

        public DataStageParameterCollection Parameters
        {
            get
            {
                if(parameters == null)
                {
                    parameters = new DataStageParameterCollection();
                }

                return parameters;
            }
            set
            {
                parameters = value;
            }
        }

        private DataStageParameterCollection parameters { get; set; }

        public DataArtifactCollection Artifacts
        {
            get
            {
                if(artifacts == null)
                {
                    artifacts = new DataArtifactCollection();
                }

                return artifacts;
            }
            set => artifacts = value;
        }

        private DataArtifactCollection artifacts { get; set; }
    }
}
