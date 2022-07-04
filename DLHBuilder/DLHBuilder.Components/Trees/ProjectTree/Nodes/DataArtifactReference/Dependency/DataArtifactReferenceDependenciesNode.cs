using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactReferenceDependenciesNode : ProjectTreeNode
    {
        public DataArtifactReferenceDependenciesNode(DataArtifactReference reference, IDataStage stage, IDataApplication application)
        {
            Dependencies = reference.Dependencies;
            Reference = reference;
            Stage = stage;
            Application = application;
            Text = "Dependencies";
            AddDependencies();
        }

        public DataArtifactDependencyCollection Dependencies
        {
            get => (DataArtifactDependencyCollection)Tag;
            set
            {
                value.CollectionAdded += OnDependencyAdded;
                Tag = value;
            }
        }

        public DataArtifactReference Reference { get; set; }

        public IDataStage Stage { get; set; }

        public IDataApplication Application { get; set; }

        void OnDependenciesModified(object sender, EventArgs e)
        {
            Nodes.Clear();
            AddDependencies();
        }

        void OnDependencyAdded(object sender, EventArgs e)
        {
            IDataArtifactDependency dependency = (IDataArtifactDependency)sender;
            Tree.SelectedNode = AddDependency(dependency);
        }

        void AddDependencies()
        {
            foreach (IDataArtifactDependency dependency in Dependencies)
            {
                AddDependency(dependency);
            }
        }

        DataArtifactReferenceDependencyNode AddDependency(IDataArtifactDependency dependency)
        {
            DataArtifactReferenceDependencyNode output = new DataArtifactReferenceDependencyNode(dependency, Dependencies);
            Nodes.Add(output);

            return output;
        }

        public override ContextMenuStrip ContextMenuStrip => new DataArtifactReferenceDependenciesMenu(this);
    }
}
