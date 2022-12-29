using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactReferenceDependencyNode : ProjectTreeNode
    {
        public DataArtifactReferenceDependencyNode(IDataArtifactDependency dependency, DataArtifactDependencyCollection dependencies)
        {
            Dependency = dependency;
            Dependencies = dependencies;
            Text = Dependency.Name;
        }

        public IDataArtifactDependency Dependency
        {
            get => (IDataArtifactDependency)Tag;
            set => Tag = value;
        }

        public DataArtifactDependencyCollection Dependencies { get; set; }

        public override string CollapsedImage => "Dependency";

        public override string ExpandedImage => "Dependency";
    }
}
