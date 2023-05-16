using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DLHApp.Model.Connections;

namespace DLHWin.Editors.Dialogs
{
    internal class SqlConnectionDialog : Form
    {
        public SqlConnectionDialog(SqlServerConnection connection = null)
        {
            Connection = connection;

            Text = "Connect to SQL Server";
            Height = 250;
            Width = 500;

            Controls.Add(ToolBar);
            Controls.Add(ServerLabel);
            Controls.Add(ServerBox);
            Controls.Add(DatabaseLabel);
            Controls.Add(DatabaseBox);

            ToolBar.Items.Add(ImportButton);
            ImportButton.Click += Import;

            ServerBox.Leave += GetDatabases;
        }

        public SqlServerConnection Connection { get; set; }

        string ServerName = string.Empty;

        ToolStrip ToolBar = new ToolStrip();

        ToolStripButton ImportButton = new ToolStripButton() { Text = "Use Connection", AutoSize = false, Width = 90 };

        Label ServerLabel = new Label() { Location = new Point(50, 50), Width = 100, Text = "Server Name:" };

        TextBox ServerBox = new TextBox() { Location = new Point(180, 50), Width = 200 };

        Label DatabaseLabel = new Label() { Location = new Point(50, 100), Width = 100, Text = "Database Name:" };

        ComboBox DatabaseBox = new ComboBox() { Location = new Point(180, 100), Width = 200 };

        void Import(object sender, EventArgs e)
        {
            if(!TestConnection())
            {
                return;
            }

            Connection = SqlServerConnection.New();
            Connection.Server = ServerBox.Text;
            Connection.Database = DatabaseBox.Text;
            Connection.Authentication = SqlServerAuthenticationType.Windows;

            DialogResult = DialogResult.OK;
            Close();
        }

        void GetDatabases(object sender, EventArgs e)
        {
            if(ServerBox.Text == ServerName)
            {
                return;
            }

            ServerName = ServerBox.Text;
            DatabaseBox.Items.Clear();

            using (SqlConnection conn = new SqlConnection(new SqlConnectionStringBuilder() { DataSource = ServerBox.Text, IntegratedSecurity = true, TrustServerCertificate = true, ConnectTimeout = 10 }.ConnectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT name FROM sys.databases ORDER BY name", conn))
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if(rdr.HasRows)
                            {
                                while(rdr.Read())
                                {
                                    DatabaseBox.Items.Add(rdr.GetString(0));
                                }
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SqlConnectionStringBuilder() { DataSource = ServerBox.Text, InitialCatalog = DatabaseBox.Text, IntegratedSecurity = true, TrustServerCertificate = true, ConnectTimeout = 10 }.ConnectionString))
                {
                    conn.Open();
                }

                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}
