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
            Columns.AddRange(EditorColumns().ToArray());
            Rows.AddRange(EditorRows().ToArray());
        }

        protected virtual EditorGridColumnCollection EditorColumns()
        {
            return new EditorGridColumnCollection();
        }

        protected virtual EditorGridRowCollection EditorRows()
        {
            return new EditorGridRowCollection();
        }

        void OnCellValueChanged(object sender, EventArgs e)
        {

        }
    }
}
