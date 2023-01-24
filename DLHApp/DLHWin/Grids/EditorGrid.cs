using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Grids
{
    internal abstract class EditorGrid : DataGridView
    {
        public EditorGrid()
        {
            Dock = DockStyle.Fill;
        }
    }
}
