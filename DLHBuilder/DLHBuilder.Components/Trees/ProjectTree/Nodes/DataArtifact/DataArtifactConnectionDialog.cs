using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactConnectionDialog : Form
    {
        public DataArtifactConnectionDialog(DataConnectionCollection connections)
        {
            Text = "Select Connection";
            Width = 450;
            MaximizeBox = false;
            MinimizeBox = false;

            foreach(DataConnection connection in connections.OrderBy(x => x.Name))
            {
                Connection.Items.Add(connection);
            }

            Connection.DisplayMember = "Name";
            Connection.SelectedValueChanged += SetConnection;
            
            Controls.Add(ConnectionLabel);
            Controls.Add(Connection);
        }

        Label ConnectionLabel = new Label() { Text = "Connection:", Width = 150, Location = new Point(50, 50) };

        ComboBox Connection = new ComboBox() { Width = 325, Location = new Point(50, 80) };

        public DataConnection SelectedConnection { get; set; }

        void SetConnection(object sender, EventArgs e)
        {
            SelectedConnection = (DataConnection)Connection.SelectedItem;
            DialogResult = DialogResult.OK;
        }
    }
}
