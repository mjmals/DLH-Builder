using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataSourcesNode : ProjectTreeNode
    {
        public DataSourcesNode(DataSourceCollection sources)
        {
            Sources = sources;
            Text = "Data Sources";
            AddDataSources();
        }

        public DataSourceCollection Sources
        {
            get => (DataSourceCollection)Tag;
            set
            {
                value.CollectionAdded += OnDataSourceAdded;
                Tag = value;
            }
        }

        public override ContextMenuStrip ContextMenuStrip => new DataSourcesMenu(this);

        void OnDataSourceAdded(object sender, EventArgs e)
        {
            DataSourceNode node = AddDataSource((IDataSource)sender);
            Tree.SelectedNode = node;
        }

        DataSourceNode AddDataSource(IDataSource datasource)
        {
            DataSourceNode output = new DataSourceNode(datasource);
            Nodes.Add(output);
            return output;
        }

        void AddDataSources()
        {
            foreach(IDataSource source in Sources)
            {
                AddDataSource(source);
            }
        }
    }
}
