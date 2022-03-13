using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public abstract class DataApplication : IDataApplication
    {
        [JsonIgnore]
        public EventHandler PropertyUpdated;

        void OnPropertyUpdated()
        {
            PropertyUpdated?.Invoke(null, null);
        }

        public string Name 
        {
            get => name;
            set
            {
                name = value;
                OnPropertyUpdated();
            }
        }

        private string name { get; set; }

        [Browsable(false)]
        public int Ordinal { get; set; }

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
