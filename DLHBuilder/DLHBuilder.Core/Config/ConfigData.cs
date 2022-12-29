using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Config
{
    internal class ConfigData
    {
        public List<ConfigItem> Items { get; set; }

        public ConfigItem GetItem(string name)
        {
            return Items.FirstOrDefault(x => x.Name == name);
        }
    }
}
