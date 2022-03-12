﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public abstract class DataSource : IDataSource
    {
        [Browsable(false)]
        public Guid ID { get; set; }

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

        [JsonIgnore]
        public EventHandler PropertyUpdated;

        void OnPropertyUpdated()
        {
            PropertyUpdated?.Invoke(null, null);
        }
    }
}
