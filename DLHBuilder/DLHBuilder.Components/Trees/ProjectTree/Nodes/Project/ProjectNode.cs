using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLHBuilder.Components.Editors;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class ProjectNode : ProjectTreeNode
    {
        public ProjectNode(Project project)
        {
            Project = project;
            Text = project.Name;
            Nodes.Add(new ScriptTemplatesNode(project.ScriptTemplates));
            Nodes.Add(new EnvironmentsNode(project.Environments));
            Nodes.Add(new KeyContainersNode(project.KeyContainers));
            Nodes.Add(new ConnectionsNode(project.Connections));
            Nodes.Add(new DataArtifactFoldersNode(project.ArtifactFolders, project.Artifacts));
            Nodes.Add(new DataApplicationsNode(project.Applications));

            Expand();
        }

        public Project Project 
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
    }
}
