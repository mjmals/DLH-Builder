using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class ADLSDataStage : DataStage
    {
        public ADLSDataStageLevelCollection Levels 
        { 
            get
            {
                if(levels == null)
                {
                    levels = new ADLSDataStageLevelCollection();
                }

                return levels;
            }
            set => levels = value; 
        }

        private ADLSDataStageLevelCollection levels { get; set; }

        public ADLSDataStageParameterCollection Parameters
        {
            get
            {
                if(parameters == null)
                {
                    parameters = new ADLSDataStageParameterCollection();
                }

                return parameters;
            }
            set
            {
                parameters = value;
            }
        }

        private ADLSDataStageParameterCollection parameters { get; set; }

        public static ADLSDataStage New()
        {
            ADLSDataStage output = new ADLSDataStage();
            output.ID = Guid.NewGuid();
            output.Name = "<New ADLS Stage>";

            return output;
        }
    }
}
