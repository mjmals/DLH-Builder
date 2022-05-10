using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class ProjectNode : ProjectTreeNode
    {
        public ProjectNode(Project project)
        {
            Project = project;
            Text = project.Name;
            Nodes.Add(new ConnectionsNode(project.Connections));
            Nodes.Add(new ScriptTemplatesNode(project.ScriptTemplates));
            Nodes.Add(new DataArtifactFoldersNode(project.ArtifactFolders));
            AddApplications();

            Expand();
        }

        public Project Project 
        { 
            get => (Project)Tag;
            set
            {
                value.PropertyUpdated += OnProjectUpdated;
                value.Applications.CollectionAdded += OnApplicationAdded;
                Tag = value;
            }
        }

        public override string CollapsedImage => "Project";

        public override string ExpandedImage => "Project";

        public override ContextMenuStrip ContextMenuStrip => new ProjectMenu(this);

        public override EditorCollection Editors()
        {
            return new EditorCollection(new PropertyEditor(Project));
        }

        public override void LabelChanged(string text)
        {
            Project.Name = text;
            base.LabelChanged(text);
        }

        void OnProjectUpdated(object sender, EventArgs e)
        {

        }

        void OnApplicationAdded(object sender, EventArgs e)
        {
            DataApplicationNode node = AddApplication((IDataApplication)sender);
            Tree.SelectedNode = node;
        }

        void AddApplications()
        {
            foreach(IDataApplication application in Project.Applications.OrderBy(x => x.Ordinal))
            {
                AddApplication(application);
            }
        }

        DataApplicationNode AddApplication(IDataApplication application)
        {
            DataApplicationNode output = DataApplicationNode.New(application);
            Nodes.Add(output);
            return output;
        }
    }
}
