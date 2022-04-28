using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class SQLDataArtifactImportDialog : DataArtifactImportDialog
    {
        public SQLDataArtifactImportDialog(SQLDataConnection connection) : base(connection)
        {
            Text = "Import from SQL";
        }
    }
}
