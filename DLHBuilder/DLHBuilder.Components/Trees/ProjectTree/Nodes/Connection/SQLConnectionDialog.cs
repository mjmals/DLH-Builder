using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Data.SqlClient;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class SQLConnectionDialog : Form
    {
        public SQLConnectionDialog()
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            Width = 580;
            Height = 300;

            Text = "New SQL Connection";

            Controls.Add(ServerLabel);
            Controls.Add(Server);
            Controls.Add(DatabaseLabel);
            Controls.Add(Database);
            Controls.Add(AuthenticationLabel);
            Controls.Add(Authentication);
            Controls.Add(AddButton);

            AddButton.Click += AddConnection;

            Authentication.DataSource = null;

            foreach(var value in Enum.GetValues(typeof(AuthenticationType)))
            {
                Authentication.Items.Add(value);
            }

            Authentication.SelectedItem = AuthenticationType.Windows;
            Authentication.Enabled = false;

            ShowDialog();
        }

        public SQLDataConnection Connection;

        Label ServerLabel = new Label() { Text = "Server", Width = 150, Location = new Point(50, 50) };

        TextBox Server = new TextBox() { Width = 300, Location = new Point(225, 50) };

        Label DatabaseLabel = new Label() { Text = "Database", Width = 150, Location = new Point(50, 100) };

        ComboBox Database = new ComboBox() { Width = 300, Location = new Point(225, 100) };

        Label AuthenticationLabel = new Label() { Text = "Authentication Mode", Width = 150, Location = new Point(50, 150) };
        
        ComboBox Authentication = new ComboBox() { Width = 300, Location = new Point(225, 150), DataSource = Enum.GetValues(typeof(AuthenticationType)) };

        Button AddButton = new Button() { Text = "Add Connection", Width = 475, Height = 30, Location = new Point(50, 200) };

        void AddConnection(object sender, EventArgs e)
        {
            switch(TestConnection())
            {
                case true:
                    Connection = new SQLDataConnection() { ID = Guid.NewGuid(), Server = Server.Text, Database = Database.Text, Authentication = (AuthenticationType)Authentication.SelectedItem };
                    DialogResult = DialogResult.OK;
                    break;
                default:
                    break;
            }
        }

        bool TestConnection()
        {
            SqlConnectionStringBuilder connstr = new SqlConnectionStringBuilder() { DataSource = Server.Text, InitialCatalog = Database.Text };

            switch((AuthenticationType)Authentication.SelectedItem)
            {
                case AuthenticationType.Windows:
                    connstr.IntegratedSecurity = true;
                    break;
                default:
                    break;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connstr.ConnectionString))
                {
                    conn.Open();
                }

                return true;
            }
            catch
            {
                MessageBox.Show("Connection Failed", "Failed to connect to SQL Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
