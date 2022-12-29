using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class AzureStorageConnectionDialog : Form
    {
        public AzureStorageConnectionDialog()
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            Width = 580;
            Height = 400;
            Text = "New Azure Blob Storage Connection";
            Controls.Add(MainPanel());

            ShowDialog();
        }

        public AzureStorageDataConnection Connection;

        Panel MainPanel()
        {
            Panel output = new Panel();
            output.Dock = DockStyle.Fill;

            Controls.AddRange(new Control[] { ConnectionNameLabel, ConnectionName });
            Controls.AddRange(new Control[] { SubscriptionNameLabel, SubscriptionName });
            Controls.AddRange(new Control[] { StorageAccountNameLabel, StorageAccountName });
            Controls.AddRange(new Control[] { ContainerNameLabel, ContainerName });
            Controls.AddRange(new Control[] { AddButton });

            AddButton.Click += AddConnection;

            return output;
        }

        Label ConnectionNameLabel = new Label() { Text = "Connection Name", Width = 150, Location = new Point(50, 50) };
        TextBox ConnectionName = new TextBox() { Width = 300, Location = new Point(225, 50) };

        Label SubscriptionNameLabel = new Label() { Text = "Subscription Name", Width = 150, Location = new Point(50, 100) };
        TextBox SubscriptionName = new TextBox() { Width = 300, Location = new Point(225, 100) };

        Label StorageAccountNameLabel = new Label() { Text = "Storage Account Name", Width = 150, Location = new Point(50, 150) };
        TextBox StorageAccountName = new TextBox() { Width = 300, Location = new Point(225, 150) };

        Label ContainerNameLabel = new Label() { Text = "Container Name", Width = 150, Location = new Point(50, 200) };
        TextBox ContainerName = new TextBox() { Width = 300, Location = new Point(225, 200) };

        Button AddButton = new Button() { Text = "Add Connection", Width = 475, Height = 30, Location = new Point(50, 250) };

        void AddConnection(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(ConnectionName.Text))
            {
                MessageBox.Show("Connection Name requires a value");
                return;
            }

            if (string.IsNullOrEmpty(SubscriptionName.Text))
            {
                MessageBox.Show("Subscription Name requires a value");
                return;
            }

            if (string.IsNullOrEmpty(StorageAccountName.Text))
            {
                MessageBox.Show("Storage Account Name requires a value");
                return;
            }

            if (string.IsNullOrEmpty(ContainerName.Text))
            {
                MessageBox.Show("Container Name requires a value");
                return;
            }

            Connection = new AzureStorageDataConnection()
            {
                ID = Guid.NewGuid(),
                Name = ConnectionName.Text,
                SubscriptionName = SubscriptionName.Text,
                StorageAccountName = StorageAccountName.Text,
                ContainerName = ContainerName.Text
            };

            DialogResult = DialogResult.OK;
        }
    }
}
