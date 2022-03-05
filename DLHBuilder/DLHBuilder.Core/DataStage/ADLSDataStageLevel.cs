using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DLHBuilder
{
    public class ADLSDataStageLevel
    {
        public string Name { get; set; }

        public ADLSDataStageLevelType Type { get; set; }

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
            set
            {
                levels = value;
            }
        }

        ADLSDataStageLevelCollection levels { get; set; }
    }
}
