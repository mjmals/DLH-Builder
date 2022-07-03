using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Dialogs
{
    public class DataArtifactReferenceObjectTree : TreeView
    {
        public DataArtifactReferenceObjectTree(DataArtifactCollection artifacts)
        {
            Artifacts = artifacts;
            CheckBoxes = true;
            Dock = DockStyle.Fill;
            Width = 400;
            ImageList = Images.Items;
            ImageKey = "Data Artifact";
            SelectedImageKey = "Data Artifact";

            LoadArtifactFolders();
            LoadArtifacts();
        }

        DataArtifactCollection Artifacts { get; set; }

        const string ArtifactFolderRoot = "Data Artifacts";

        void LoadArtifactFolders()
        {
            TreeNode rootNode = new TreeNode();
            rootNode.Name = ArtifactFolderRoot;
            rootNode.Text = ArtifactFolderRoot;
            rootNode.ImageKey = "Folder Open";
            rootNode.SelectedImageKey = "Folder Open";
            rootNode.Expand();
            Nodes.Add(rootNode);

            foreach(DataArtifact artifact in Artifacts)
            {
                string path = ArtifactFolderRoot;

                foreach(string folder in artifact.ArtifactNamespace)
                {
                    path += string.Format(".{0}", folder);

                    if(Nodes.Find(path, true).Count() == 0)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Name = path;
                        newNode.Text = folder;
                        newNode.ImageKey = "Folder Closed";
                        newNode.SelectedImageKey = "Folder Closed";

                        string parentFolder = path.Substring(0, path.LastIndexOf('.'));
                        TreeNode parentNode = Nodes.Find(parentFolder, true).FirstOrDefault();
                        parentNode.Nodes.Add(newNode);
                    }
                }
            }
        }

        void LoadArtifacts()
        {
            foreach(DataArtifact artifact in Artifacts)
            {
                string parentFolderName = string.Format("{0}.{1}", ArtifactFolderRoot, string.Join('.', artifact.ArtifactNamespace));
                TreeNode parentNode = Nodes.Find(parentFolderName, true).FirstOrDefault();

                DataArtifactReference reference = new DataArtifactReference();
                reference.ID = Guid.NewGuid();
                reference.DataArtifactID = artifact.ID;
                reference.ReferencedArtifact = artifact;

                TreeNode artifactNode = new TreeNode();
                artifactNode.Name = string.Format("{0}.{1}", ArtifactFolderRoot, artifact.FullName);
                artifactNode.Text = artifact.Name;
                artifactNode.Tag = reference;

                parentNode.Nodes.Add(artifactNode);
            }
        }
    }
}
