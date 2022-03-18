using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class EditorGridColumnCollection: List<DataGridViewColumn>
    {
        public DataGridViewColumn Add(string header)
        {
            DataGridViewTextBoxColumn output = new DataGridViewTextBoxColumn() { HeaderText = header };
            base.Add(output);
            return output;
        }

        public DataGridViewColumn Add(string header, object[] values)
        {
            DataGridViewComboBoxColumn output = new DataGridViewComboBoxColumn() { HeaderText = header };
            output.Items.AddRange(values);
            base.Add(output);
            return output;
        }
    }
}
