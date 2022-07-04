using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactSQLImportDialog : Form
    {
        public DataArtifactSQLImportDialog(SQLDataConnection connection)
        {
            Connection = connection;

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

            TableTree.Nodes.AddRange(TableNodes());
        }

        SQLDataConnection Connection
        {
            get => (SQLDataConnection)Tag;
            set => Tag = value;
        }

        TreeView TableTree = new TreeView() { ImageList = Images.Items, Dock = DockStyle.Fill };

        ToolStrip Tools = new ToolStrip() { ImageList = Images.Items };

        Panel DisplayPanel = new Panel() { Dock = DockStyle.Fill };

        ToolStripButton TableSelectButton = new ToolStripButton() { ImageKey = "Table", Checked = true, CheckOnClick = true };

        ToolStripButton SQLSelectButton = new ToolStripButton() { ImageKey = "SQL", CheckOnClick = true };

        RichTextBox SQLTextBox = new RichTextBox() { Dock = DockStyle.Fill, AcceptsTab = true };

        TreeNode[] TableNodes()
        {
            List<TreeNode> output = new List<TreeNode>();

            string connstr = new SqlConnectionStringBuilder() { DataSource = Connection.Server, InitialCatalog = Connection.Database, IntegratedSecurity = true }.ConnectionString;

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(Properties.Resources.DatabaseTableColumns, conn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            TreeNode node = new TreeNode();

                            while (rdr.Read())
                            {
                                string tbl = string.Format("{0}.{1}", rdr.GetString(rdr.GetOrdinal("TABLE_SCHEMA")), rdr.GetString(rdr.GetOrdinal("TABLE_NAME")));

                                if (tbl != node.Text)
                                {
                                    TreeNode tablenode = new TreeNode();
                                    tablenode.ImageKey = "Table";
                                    tablenode.Text = tbl;

                                    node = tablenode;
                                    output.Add(tablenode);
                                }
                            }
                        }
                    }
                }
            }

            return output.ToArray();
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
