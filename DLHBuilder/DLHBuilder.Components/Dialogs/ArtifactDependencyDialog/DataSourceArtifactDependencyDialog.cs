using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Dialogs
{
    public class DataSourceArtifactDependencyDialog : ArtifactDependencyDialog
    {
        public DataSourceArtifactDependencyDialog(DataArtifactReference reference)
        {
            foreach(IDataSource source in reference.ReferencedArtifact.DataSources)
            {
                SelectionTree.Nodes.Add(new TreeNode()
                {
                    Text = source.Name,
                    Name = SelectionAllowedType().Name,
                    Tag = CreateDependency(reference, source),
                    ImageKey = "Data Source",
                    SelectedImageKey = "Data Source"
                }) ;
            }
        }

        IDataArtifactDependency CreateDependency(DataArtifactReference reference, IDataSource source)
        {
            DataArtifactDataSourceDependency output = new DataArtifactDataSourceDependency();
            output.Name = source.Name;
            output.ArtifactID = reference.DataArtifactID;
            output.DataSourceID = source.ID;

            return output;
        }

        protected override Type SelectionAllowedType()
        {
            return typeof(IDataSource);
        }
    }
}
