using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin
{
    public class ProjectDirectoryItem
    {
        public string Name { get; set; }

        public string? Extension { get; set; }

        public string Parent { get; set; }

        public string FullPath => Path.Combine(string.IsNullOrEmpty(Parent) ? "" : Parent, Name);

        public ProjectDirectoryItemType Type { get; set; }

        public bool AllowChild = true;

        public override string ToString()
        {
            return FullPath;
        }
    }
}
