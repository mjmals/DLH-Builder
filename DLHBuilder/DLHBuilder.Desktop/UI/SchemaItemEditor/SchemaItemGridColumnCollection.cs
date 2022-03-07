using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class SchemaItemGridColumnCollection : List<DataGridViewColumn>
    {
        public SchemaItemGridColumnCollection()
        {
            Add(Name);
        }

        DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn() { HeaderText = "Name" };
    }
}
