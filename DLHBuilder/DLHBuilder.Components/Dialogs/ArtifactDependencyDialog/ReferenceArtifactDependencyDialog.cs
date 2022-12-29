using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Dialogs
{
    public class ReferenceArtifactDependencyDialog : ArtifactDependencyDialog
    {
        public ReferenceArtifactDependencyDialog(DataArtifactReference reference, IDataStage stage, IDataApplication application)
        {
            foreach (IDataStage stg in application.Stages.OrderBy(x => x.Ordinal))
            {
                SelectionTree.Nodes.Add(new TreeNode() { Text = stg.Name, Name = stg.Name, Tag = stg, ImageKey = "Data Stage", SelectedImageKey = "Data Stage" }) ;
            }

            foreach (var stageFolder in application.Stages.SelectMany(stg => stg.Folders.Select(fldr => new { Stage = stg.Name, Folder = fldr.Name, Path = fldr.Path })).OrderBy(o => o.Path.Count))
            {
                string root = stageFolder.Stage + (stageFolder.Path.Count > 0 ? "." + string.Join('.', stageFolder.Path) : "");
                
                RootNode(root).Nodes.Add(new TreeNode() 
                    { 
                        Text = stageFolder.Folder, 
                        Name = root + "." + stageFolder.Folder, 
                        ImageKey = "Folder Closed", 
                        SelectedImageKey = "Folder Closed" 
                    }
                );
            }

            foreach(var stageArtifact in application.Stages.SelectMany(stg => stg.ArtifactReferences.Select(aft => new { Stage = stg, Reference = aft })))
            {
                string root = stageArtifact.Stage.Name + (stageArtifact.Reference.Path.Count > 0 ? "." + string.Join('.', stageArtifact.Reference.Path) : "");
                
                RootNode(root).Nodes.Add(new TreeNode() 
                    { 
                        Text = stageArtifact.Reference.ReferencedArtifact.Name, 
                        Name = SelectionAllowedType().Name, 
                        Tag = CreateDependency(stageArtifact.Reference, stageArtifact.Stage),
                        ImageKey = "Data Artifact", 
                        SelectedImageKey = "Data Artifact" 
                    }
                );
            }
        }

        IDataArtifactDependency CreateDependency(DataArtifactReference reference, IDataStage stage)
        {
            DataArtifactReferenceDependency output = new DataArtifactReferenceDependency();
            output.Name = stage.Name + "." + reference.FullPath.Replace(reference.ID.ToString(), reference.ReferencedArtifact.Name);
            output.ArtifactID = reference.ID;
            output.StageID = stage.ID;

            return output;
        }

        protected override Type SelectionAllowedType()
        {
            return typeof(DataArtifactReference);
        }
    }
}