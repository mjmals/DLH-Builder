using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Dialogs
{
    public class DataArtifactImportControls : Panel
    {
        public DataArtifactImportControls()
        {
            Dock = DockStyle.Bottom;
            Height = 50;
            Controls.Add(ImportButton);
        }

        public Button ImportButton = new Button() { Text = "Import", Width = 200, Padding = new Padding(5), Margin = new Padding(30), Dock = DockStyle.Right };
    }
}
