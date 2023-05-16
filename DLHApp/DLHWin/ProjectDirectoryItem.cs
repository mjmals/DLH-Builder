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

        public string Parent 
        { 
            get => _parent; 
            set
            {
                string oldPath = _parent;
                _parent = value;
                OnParentChanged(oldPath, value);
            }
        }

        private string _parent { get; set; }

        public string FullPath => Path.Combine(string.IsNullOrEmpty(Parent) ? "" : Parent, Name);

        public ProjectDirectoryItemType Type { get; set; }

        public bool AllowChild = true;

        public override string ToString()
        {
            return FullPath;
        }

        void OnParentChanged(string oldPath, string newPath)
        {
            ParentChanged?.Invoke(this, new ProjectDirectoryItemParentEventArgs(oldPath, newPath));
        }

        public EventHandler<ProjectDirectoryItemParentEventArgs> ParentChanged;
    }

    public class ProjectDirectoryItemParentEventArgs : EventArgs
    {
        public ProjectDirectoryItemParentEventArgs(string oldPath, string newPath)
        {
            OldPath = OldPath;
            NewPath = NewPath;
        }

        public string OldPath { get; set; }

        public string NewPath { get; set; }
    }
}
