using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataSourceNode : ProjectTreeNode
    {
        public DataSourceNode(IDataSource source)
        {
            Source = source;
            Text = Source.Name;
        }

        IDataSource Source
        {
            get => (IDataSource)Tag;
            set
            {
                ((DataSource)value).PropertyUpdated += OnPropertyUpdated;
                Tag = value;
            }
        }

        public override string CollapsedImage => "Data Source";

        public override string ExpandedImage => "Data Source";

        public override void LabelChanged(string text)
        {
            Source.Name = text;
            base.LabelChanged(text);
        }

        void OnPropertyUpdated(object sender, EventArgs e)
        {
            Text = Source.Name;
        }
    }
}
