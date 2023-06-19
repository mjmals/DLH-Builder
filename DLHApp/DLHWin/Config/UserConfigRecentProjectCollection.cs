using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Config
{
    internal class UserConfigRecentProjectCollection : List<string>
    {
        public new void Add(string value)
        {
            RemoveAll(x => x.ToLower() == value.ToLower());
            Insert(0, value);
            FileAdded?.Invoke(null, null);
        }

        public EventHandler FileAdded { get; set; }
    }
}
