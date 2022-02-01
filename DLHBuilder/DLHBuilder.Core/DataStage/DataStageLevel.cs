using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DLHBuilder
{
    public class DataStageLevel
    {
        public string Name { get; set; }

        public DataStageLevelType Type { get; set; }

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
            set
            {
                levels = value;
            }
        }

        DataStageLevelCollection levels { get; set; }
    }
}
