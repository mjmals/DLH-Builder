using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    abstract class DataApplicationNode : ProjectTreeNode
    {
        public DataApplicationNode(IDataApplication application)
        {
            Application = application;
            Text = Application.Name;

            AddStages();
        }

        public IDataApplication Application
        {
            get => (DataApplication)Tag;
            set
            {
                ((DataApplication)value).PropertyUpdated += OnPropertyUpdated;
                value.Stages.CollectionAdded += OnStageAdded;
                Tag = value;
            }
        }

        public override string CollapsedImage => "Data Application";

        public override string ExpandedImage => "Data Application";

        public static DataApplicationNode New(IDataApplication application)
        {
            if(application is SQLDataApplication)
            {
                return new SQLDataApplicationNode((SQLDataApplication)application);
            }

            if (application is DataLakeDataApplication)
            {
                return new DataLakeDataApplicationNode((DataLakeDataApplication)application);
            }

            return null;
        }

        public override void LabelChanged(string text)
        {
            Application.Name = text;
            base.LabelChanged(text);
        }

        void OnPropertyUpdated(object sender, EventArgs e)
        {
            Text = Application.Name;
        }

        void OnStageAdded(object sender, EventArgs e)
        {
            DataStageNode node = AddStage((IDataStage)sender);
            Tree.SelectedNode = node;
        }

        void AddStages()
        {
            foreach (IDataStage stage in Application.Stages.OrderBy(x => x.Ordinal))
            {
                AddStage(stage);
            }
        }

        DataStageNode AddStage(IDataStage stage)
        {
            DataStageNode output = new DataStageNode(stage, Application);
            Nodes.Add(output);
            return output;
        }
    }
}
