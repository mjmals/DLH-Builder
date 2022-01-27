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
    }
}
