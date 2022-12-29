using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHBuilder.Components.Dialogs;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataSourcesMenu : ProjectTreeMenu
    {
        public DataSourcesMenu(DataSourcesNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add SQL Connection Data Source", AddSQLConnectionDataSource));
        }

        DataSourcesNode Node
        {
            get => (DataSourcesNode)Tag;
            set => Tag = value;
        }

        void AddSQLConnectionDataSource(object sender, EventArgs e)
        {
            SQLConnectionSelectionDialog dialog = new SQLConnectionSelectionDialog(Node.Tree.Project.Connections);

            if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SQLConnectionDataSource source = SQLConnectionDataSource.New(dialog.SelectedConnection.ID);
                Node.Sources.Add(source);
            }
        }
    }
}
