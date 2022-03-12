using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DLHBuilder.Desktop.UI
{
    class ConnectionSelectionDialog : Form
    {
        public ConnectionSelectionDialog(DataConnectionCollection connections)
        {
            Text = "Select Connection";
            Width = 450;
            MaximizeBox = false;
            MinimizeBox = false;

            AddConnections(connections);

            Connection.DisplayMember = "Name";
            Connection.SelectedValueChanged += SetConnection;
            
            Controls.Add(ConnectionLabel);
            Controls.Add(Connection);
        }

        Label ConnectionLabel = new Label() { Text = "Connection:", Width = 150, Location = new Point(50, 50) };

        protected ComboBox Connection = new ComboBox() { Width = 325, Location = new Point(50, 80) };

        public DataConnection SelectedConnection { get; set; }

        protected virtual void AddConnections(DataConnectionCollection connections)
        {
            foreach (DataConnection connection in connections.OrderBy(x => x.Name))
            {
                Connection.Items.Add(connection);
            }
        }

        void SetConnection(object sender, EventArgs e)
        {
            SelectedConnection = (DataConnection)Connection.SelectedItem;
            DialogResult = DialogResult.OK;
        }
    }
}
