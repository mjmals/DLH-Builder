using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactImportDialog : Form
    {
        public DataArtifactImportDialog(DataConnection connection)
        {
            Connection = connection;

            ObjectPanel.Controls.Add(ObjectTree = new DataArtifactImportObjectTree());
            Controls.Add(ObjectPanel);

            Controls.Add(SelectorPanel);
            Controls.Add(ControlPanel);

            WindowState = FormWindowState.Maximized;
        }
        
        DataConnection Connection { get; set; }

        public DataArtifact Artifact { get; set; }

        DataArtifactImportObjectTree ObjectTree { get; set; }

        Panel ObjectPanel = new Panel() { Dock = DockStyle.Left, Width = 400 };

        Panel SelectorPanel = new Panel() { Dock = DockStyle.Fill };

        Panel ControlPanel = new Panel() { Dock = DockStyle.Bottom, Height = 50 };
    }
}
