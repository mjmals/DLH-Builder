using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataApplicationsNode : ProjectTreeNode
    {
        public DataApplicationsNode(DataApplicationCollection applications)
        {
            Applications = applications;
            Text = "Data Applications";
            AddApplications();
        }

        public DataApplicationCollection Applications
        {
            get => (DataApplicationCollection)Tag;
            set
            {
                value.CollectionAdded += OnApplicationAdded;
                Tag = value;
            }
        }

        public override ContextMenuStrip ContextMenuStrip => new DataApplicationsMenu(this);

        public override bool AllowLabelChange => false;

        void AddApplications()
        {
            foreach(IDataApplication application in Applications.OrderBy(x => x.Ordinal))
            {
                AddApplication(application);
            }
        }

        void OnApplicationAdded(object sender, EventArgs e)
        {
            DataApplicationNode node = AddApplication((IDataApplication)sender);
            Tree.SelectedNode = node;
        }


        DataApplicationNode AddApplication(IDataApplication application)
        {
            DataApplicationNode output = DataApplicationNode.New(application);
            Nodes.Add(output);
            return output;
        }
    }
}
