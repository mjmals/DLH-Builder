using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DLHBuilder.Components.Dialogs
{
    public class ADLSDataStageDialog : Form
    {
        public ADLSDataStageDialog(Project project)
        {
            Text = "New ADLS Data Stage";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            Width = 580;
            Height = 300;

            Connection.DataSource = project.Connections.Where(x => x.GetType() == typeof(AzureStorageDataConnection)).ToList();
            Connection.DisplayMember = "Name";

            Controls.Add(MainPanel());
            ShowDialog();
        }

        public ADLSDataStage DataStage = ADLSDataStage.New();

        Label StageNameLabel = new Label() { Text = "Data Stage Name", Width = 150, Location = new Point(50, 50) };
        TextBox StageName = new TextBox() { Width = 300, Location = new Point(225, 50) };

        Label ConnectionLabel = new Label() { Text = "Destination Connection", Width = 150, Location = new Point(50, 100) };
        ComboBox Connection = new ComboBox() { Width = 300, Location = new Point(225, 100) };

        Button AddButton = new Button() { Text = "Add Data Stage", Width = 475, Height = 30, Location = new Point(50, 150) };

        Panel MainPanel()
        {
            Panel output = new Panel();
            output.Dock = DockStyle.Fill;

            output.Controls.AddRange(new Control[] { StageNameLabel, StageName });
            output.Controls.AddRange(new Control[] { ConnectionLabel, Connection });
            output.Controls.AddRange(new Control[] { AddButton });

            AddButton.Click += Add;

            return output;
        }

        void Add(object sender = null, EventArgs e = null)
        {
            AzureStorageDataConnection selectedConnection = null;

            if(Connection.SelectedItem != null)
            {
                selectedConnection = (AzureStorageDataConnection)Connection.SelectedItem;
            }

            DataStage = new ADLSDataStage()
            {
                Name = StageName.Text,
                ConnectionID = selectedConnection == null ? Guid.Empty : selectedConnection.ID
            };

            DialogResult = DialogResult.OK;
        }
    }
}
