using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class ADLSDataStage : DataStage
    {
        public static ADLSDataStage New()
        {
            ADLSDataStage output = new ADLSDataStage();
            output.ID = Guid.NewGuid();
            output.Name = "<New ADLS Stage>";

            return output;
        }
    }
}
