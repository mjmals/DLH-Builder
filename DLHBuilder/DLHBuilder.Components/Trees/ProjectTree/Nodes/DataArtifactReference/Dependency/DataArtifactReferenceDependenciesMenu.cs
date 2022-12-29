using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHBuilder.Components.Dialogs;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactReferenceDependenciesMenu : ProjectTreeMenu
    {
        public DataArtifactReferenceDependenciesMenu(DataArtifactReferenceDependenciesNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Artifact Dependency", AddReferenceDependency));
            Items.Add(new ProjectTreeMenuButton("Add Data Source Dependency", AddDataSourceDependency));
        }

        DataArtifactReferenceDependenciesNode Node
        {
            get => (DataArtifactReferenceDependenciesNode)Tag;
            set => Tag = value;
        }

        void AddDataSourceDependency(object sender, EventArgs e)
        {
            GetDependencies(new DataSourceArtifactDependencyDialog(Node.Reference));
        }

        void AddReferenceDependency(object sender, EventArgs e)
        {
            GetDependencies(new ReferenceArtifactDependencyDialog(Node.Reference, Node.Stage, Node.Application));
        }

        void GetDependencies(ArtifactDependencyDialog dialog)
        {
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (IDataArtifactDependency dependency in dialog.SelectedDependencies)
                {
                    Node.Reference.Dependencies.Add(dependency);
                }
            }
        }
    }
}
