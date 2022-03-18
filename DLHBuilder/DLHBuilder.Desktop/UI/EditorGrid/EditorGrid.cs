using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class EditorGrid : DataGridView
    {
        public EditorGrid()
        {
            CellValueChanged += OnCellValueChanged;
        }

        void OnCellValueChanged(object sender, EventArgs e)
        {

        }
    }
}
