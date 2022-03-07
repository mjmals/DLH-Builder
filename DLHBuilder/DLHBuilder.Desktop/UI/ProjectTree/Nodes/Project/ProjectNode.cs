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
            Nodes.Add(new DataArtifactGroupsNode(project.ArtifactGroups));
            Nodes.Add(new DataStagesNode(project.Stages));

            Expand();
        }

        Project Project 
        { 
            get => (Project)Tag;
            set
            {
                value.PropertyUpdated += OnProjectUpdated;
                Tag = value;
            }
        }

        public override string CollapsedImage => "Project";

        public override string ExpandedImage => "Project";

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
    }
}
