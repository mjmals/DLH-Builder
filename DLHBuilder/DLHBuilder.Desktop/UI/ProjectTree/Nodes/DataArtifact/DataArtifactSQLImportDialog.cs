using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactSQLImportDialog : Form
    {
        public DataArtifactSQLImportDialog(SQLDataConnection connection)
        {
            Text = "Import Database Object";
            Width = 800;
            Height = 600;

            DisplayPanel.Controls.Add(TableTree);
            Controls.Add(DisplayPanel);
            Controls.Add(Tools);

            TableSelectButton.Click += TableSelectButtonClicked;
            SQLSelectButton.Click += SQLSelectButtonClicked;

            Tools.Items.Add(TableSelectButton);
            Tools.Items.Add(SQLSelectButton);
            Tools.Items.Add(new ToolStripSeparator());
            Tools.Items.Add(new ToolStripButton(null, Images.Items.Images["Run"]));
        }

        TreeView TableTree = new TreeView() { ImageList = Images.Items, Dock = DockStyle.Fill };

        ToolStrip Tools = new ToolStrip() { ImageList = Images.Items };

        Panel DisplayPanel = new Panel() { Dock = DockStyle.Fill };

        ToolStripButton TableSelectButton = new ToolStripButton() { ImageKey = "Table", Checked = true, CheckOnClick = true };

        ToolStripButton SQLSelectButton = new ToolStripButton() { ImageKey = "SQL", CheckOnClick = true };

        RichTextBox SQLTextBox = new RichTextBox() { Dock = DockStyle.Fill, AcceptsTab = true };

        TreeNode[] TableNodes()
        {
            return null;
        }

        void TableSelectButtonClicked(object sender, EventArgs e)
        {
            SQLSelectButton.Checked = false;
            DisplayPanel.Controls.Clear();
            DisplayPanel.Controls.Add(TableTree);
        }

        void SQLSelectButtonClicked(object sender, EventArgs e)
        {
            TableSelectButton.Checked = false;
            DisplayPanel.Controls.Clear();
            DisplayPanel.Controls.Add(SQLTextBox);
        }
    }
}
