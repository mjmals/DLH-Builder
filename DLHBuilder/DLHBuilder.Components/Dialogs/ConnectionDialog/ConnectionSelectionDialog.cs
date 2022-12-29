using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DLHBuilder.Components.Dialogs
{
    public class ConnectionSelectionDialog : Form
    {
        public ConnectionSelectionDialog(DataConnectionCollection connections)
        {
            Text = "Select Connection";
            Width = 450;
            MaximizeBox = false;
            MinimizeBox = false;

            Connections = connections;
            Controls.Add(TabController());
        }

        DataConnectionCollection Connections { get; set; }

        Label ConnectionLabel = new Label() { Text = "Connection:", Width = 150, Location = new Point(50, 50) };

        protected ComboBox Connection = new ComboBox() { Width = 325, Location = new Point(50, 80) };

        Button SetConnectionButton = new Button() { Text = "Select Connection", Width = 325, Location = new Point(50, 110) };

        public DataConnection SelectedConnection { get; set; }

        public DataArtifactImportDialogOptions Options = new DataArtifactImportDialogOptions() { Items = new Dictionary<string, object>() };

        public TabControl TabController()
        {
            TabControl output = new TabControl();
            output.Dock = DockStyle.Fill;

            TabPage connectionPage = new TabPage();
            connectionPage.Text = "Select Connection";
            connectionPage.Controls.Add(ConnectionPanel());
            output.TabPages.Add(connectionPage);

            TabPage propertiesPage = new TabPage();
            propertiesPage.Text = "Properties";
            propertiesPage.Controls.Add(PropertiesPanel);
            output.TabPages.Add(propertiesPage);

            return output;
        }

        public Panel ConnectionPanel()
        {
            Panel output = new Panel();
            output.Dock = DockStyle.Fill;
            output.Controls.Add(ConnectionLabel);
            output.Controls.Add(Connection);
            output.Controls.Add(SetConnectionButton);

            Connection.DisplayMember = "Name";
            Connection.SelectedValueChanged += SetProperties;
            AddConnections(Connections);

            SetConnectionButton.Click += SetConnection;

            return output;
        }

        public Panel PropertiesPanel = new Panel() { Dock = DockStyle.Fill };

        public Panel SQLPropertiesPanel()
        {
            Panel output = new Panel();
            output.Dock = DockStyle.Fill;
            output.Controls.Add(IncludeStoredProcedures);

            return output;
        }

        public CheckBox IncludeStoredProcedures = new CheckBox() { Text = "Include Stored Procedures", Width = 200, Location = new Point(20, 20) };

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
            Options.Items.Add("IncludeStoredProcedures", IncludeStoredProcedures.Checked);

            DialogResult = DialogResult.OK;
        }

        void SetProperties(object sender, EventArgs e)
        {
            DataConnection selected = (DataConnection)Connection.SelectedItem;

            PropertiesPanel.Controls.Clear();

            if(selected is SQLDataConnection)
            {
                PropertiesPanel.Controls.Add(SQLPropertiesPanel());
                return;
            }
        }
    }
}
