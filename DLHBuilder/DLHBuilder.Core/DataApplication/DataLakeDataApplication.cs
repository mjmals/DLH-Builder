using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;

namespace DLHBuilder
{
    public class DataLakeDataApplication : DataApplication
    {
        [JsonIgnore]
        [Browsable(false)]
        public DataStageCollection Stages
        {
            get
            {
                if(stages == null)
                {
                    stages = new DataStageCollection();
                }

                return stages;
            }
            set => stages = value;
        }

        private DataStageCollection stages { get; set; }
    }
}
