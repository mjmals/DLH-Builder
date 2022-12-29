using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.EditorGrids
{
    public class EditorGridControlChangeEventArgs : EventArgs
    {
        public EditorGridControlChangeEventArgs(EditorGridPanel panel)
        {
            Panel = panel;
        }

        public EditorGridPanel Panel { get; }
    }
}
