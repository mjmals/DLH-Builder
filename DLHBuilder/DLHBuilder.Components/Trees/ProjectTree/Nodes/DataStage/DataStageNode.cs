using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLHBuilder.Components.Editors;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataStageNode : ProjectTreeNode
    {
        public DataStageNode(IDataStage stage, IDataApplication parentApplication)
        {
            Stage = stage;
            ParentApplication = parentApplication;
            Text = stage.Name;
            Name = "Data Stages." + stage.Name;
            AddFolders();
            AddReferences();
        }

        public IDataStage Stage
        {
            get => (IDataStage)Tag;
            set
            {
                value.Folders.CollectionAdded += OnFolderAdded;
                value.Folders.CollectionRemoved += OnFolderRemoved;
                value.ArtifactReferences.CollectionAdded += OnReferenceAdded;
                value.ArtifactReferences.CollectionRemoved += OnReferenceRemoved;
                Tag = value;
            }
        }

        public IDataApplication ParentApplication { get; set; }

        public override string CollapsedImage => "Data Stage";

        public override string ExpandedImage => "Data Stage";

        public override ContextMenuStrip ContextMenuStrip => new DataStageMenu(this);

        public override EditorCollection Editors()
        {
            return new EditorCollection
            (
                new ScriptTemplateMappingEditor(Tree.Project.ScriptTemplates, Stage.ArtifactDefaultScriptTemplates, "Artifact Default Templates"),
                new ScriptTemplateMappingEditor(Tree.Project.ScriptTemplates, Stage.ScriptTemplates),
                new PropertyEditor(Stage)
            );
        }

        public override void LabelChanged(string text)
        {
            Stage.Name = text;
            Name = "Data Stages." + text;
            base.LabelChanged(text);
        }

        void OnFolderAdded(object sender, EventArgs e)
        {
            DataStageFolder folder = (DataStageFolder)sender;
            Tree.SelectedNode = AddFolder(folder);
        }

        void AddFolders()
        {
            foreach(DataStageFolder folder in Stage.Folders)
            {
                AddFolder(folder);
            }
        }

        DataStageFolderNode AddFolder(DataStageFolder folder)
        {
            DataStageFolderNode output = new DataStageFolderNode(folder, Stage, ParentApplication);
            output.Name = Name + "." + (folder.Path.Count > 0 ? folder.FullPath : folder.Name);
            
            string parentNodeName = Name + (folder.Path.Count > 0 ? "." + string.Join('.', folder.Path) : "");
            
            if(parentNodeName == this.Name)
            {
                Nodes.Add(output);
            }
            else
            {
                ProjectTreeNode parentNode = (ProjectTreeNode)Nodes.Find(parentNodeName, true).FirstOrDefault();
                parentNode.Nodes.Add(output);
            }

            return output;
        }

        void OnFolderRemoved(object sender, EventArgs e)
        {
            DataStageFolder folder = (DataStageFolder)sender;

            foreach(DataStageFolder fldr in Stage.Folders.Where(x => x.FullPath.StartsWith(folder.FullPath + ".")).ToList())
            {
                Stage.Folders.Remove(fldr);
            }

            foreach (DataArtifactReference reference in Stage.ArtifactReferences.Where(x => x.FullPath.StartsWith(folder.FullPath + ".")).ToList())
            {
                Stage.ArtifactReferences.Remove(reference);
            }

            string nodeName = Name + "." + (folder.Path.Count > 0 ? folder.FullPath : folder.Name);

            if (Nodes.Find(nodeName, true) != null)
            { 
                Nodes.RemoveByKey(nodeName);
            }
        }

        void OnReferenceAdded(object sender, EventArgs e)
        {
            DataArtifactReference reference = (DataArtifactReference)sender;
            Tree.SelectedNode = AddReference(reference);
        }

        void AddReferences()
        {
            foreach(DataArtifactReference reference in Stage.ArtifactReferences.OrderBy(x => x.ReferencedArtifact.Name))
            {
                AddReference(reference);
            }
        }

        DataArtifactReferenceNode AddReference(DataArtifactReference reference)
        {
            DataArtifactReferenceNode output = new DataArtifactReferenceNode(reference, Stage, ParentApplication);
            output.Name = Name + "." + (reference.Path.Count > 0 ? reference.FullPath : reference.ID);

            string parentNodeName = Name + (reference.Path.Count > 0 ? "." + string.Join('.', reference.Path) : "");

            if (parentNodeName == this.Name)
            {
                Nodes.Add(output);
            }
            else
            {
                ProjectTreeNode parentNode = (ProjectTreeNode)Nodes.Find(parentNodeName, true).FirstOrDefault();
                parentNode.Nodes.Add(output);
            }

            return output;
        }

        void OnReferenceRemoved(object sender, EventArgs e)
        {
            DataArtifactReference reference = (DataArtifactReference)sender;
            string nodeName = Name + "." + (reference.Path.Count > 0 ? reference.FullPath : reference.ID);

            if (Nodes.Find(nodeName, true) != null)
            {
                Nodes.Remove(Nodes.Find(nodeName, true).FirstOrDefault());
            }
        }
    }
}
