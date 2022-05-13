using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactReferenceDialog : Form
    {
        public DataArtifactReferenceDialog(DataArtifactCollection artifacts, string path)
        {
            Artifacts = artifacts;
            Path = path;

            Text = "Link Data Artifact";
            WindowState = FormWindowState.Maximized;

            Controls.Add(SelectorPanel);

            Controls.Add(ControlPanel);
            ControlPanel.ImportButton.Click += RunImport;

            Controls.Add(ObjectPanel);

            ObjectPanel.Controls.Add(ObjectTree = new DataArtifactReferenceObjectTree(Artifacts));
            ObjectTree.AfterCheck += ObjectTreeChecked;
            ObjectTree.AfterSelect += ObjectTreeSelected;

            SelectorPanel.Controls.Add(TransformationTree = new DataArtifactReferenceTransformationTree(null, null));
        }

        DataArtifactCollection Artifacts { get; set; }

        string Path { get; set; }

        DataArtifactReferenceObjectTree ObjectTree { get; set; }

        DataArtifactReferenceTransformationTree TransformationTree { get; set; }

        DataArtifactReferenceControls ControlPanel = new DataArtifactReferenceControls();

        public Dictionary<DataArtifactReference, DataArtifactTransformationCollection> SelectedReferences = new Dictionary<DataArtifactReference, DataArtifactTransformationCollection>();

        Panel ObjectPanel = new Panel() { Dock = DockStyle.Left, Width = 400 };

        Panel SelectorPanel = new Panel() { Dock = DockStyle.Fill };

        void ObjectTreeChecked(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Tag == null || e.Node.Tag.GetType() != typeof(DataArtifactReference))
            {
                return;
            }

            DataArtifactReference reference = (DataArtifactReference)e.Node.Tag;

            switch (e.Node.Checked)
            {
                case true:
                    if (!SelectedReferences.ContainsKey(reference))
                    {
                        SelectedReferences.Add(reference, new DataArtifactTransformationCollection());
                        SetTransformations(reference);
                    }
                    break;
                case false:
                    SelectedReferences.Remove(reference);
                    break;
            }

            ReloadSelectorTree();
        }

        void SetTransformations(DataArtifactReference reference)
        {
            DataArtifactTransformationCollection transformations = SelectedReferences[reference];

            DefinitionDataArtifactTransformation artifactDefinition = new DefinitionDataArtifactTransformation();
            artifactDefinition.ID = Guid.NewGuid();
            artifactDefinition.ReferencedObjectID = reference.DataArtifactID;
            transformations.Add(artifactDefinition);

            foreach (DataArtifactSchemaItem schemaItem in reference.ReferencedArtifact.Schema)
            {
                SchemaInclusionDataArtifactTransformation schemaTransformation = new SchemaInclusionDataArtifactTransformation();
                schemaTransformation.ID = Guid.NewGuid();
                schemaTransformation.ReferencedObjectID = schemaItem.ID;
                schemaTransformation.Include = true;
                transformations.Add(schemaTransformation);
            }
        }

        void ObjectTreeSelected(object sender, TreeViewEventArgs e)
        {
            ReloadSelectorTree();
        }

        void ReloadSelectorTree()
        {
            TreeNode selectedNode = ObjectTree.SelectedNode;

            if (selectedNode != null && selectedNode.Tag != null)
            {
                DataArtifactReference reference = (DataArtifactReference)selectedNode.Tag;

                SelectorPanel.Controls.Clear();

                DataArtifactTransformationCollection transformations = SelectedReferences.ContainsKey(reference) ? SelectedReferences[reference] : null;
                SelectorPanel.Controls.Add(TransformationTree = new DataArtifactReferenceTransformationTree(reference.ReferencedArtifact.Schema, transformations));
            }
            else
            {
                SelectorPanel.Controls.Add(TransformationTree = new DataArtifactReferenceTransformationTree(null, null));
            }

            TransformationTree.AfterCheck += SelectorTreeChecked;
        }

        void SelectorTreeChecked(object sender, TreeViewEventArgs e)
        {
            SchemaInclusionDataArtifactTransformation transformation = (SchemaInclusionDataArtifactTransformation)e.Node.Tag;
            transformation.Include = e.Node.Checked;
        }

        void RunImport(object sender, EventArgs e)
        {
            foreach (DataArtifactReference reference in SelectedReferences.Keys)
            {
                reference.Path = Path.Split('.').ToList();
                reference.Transformations.AddRange(SelectedReferences[reference]);
            }

            DialogResult = DialogResult.OK;
        }
    }
}
