using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactReferenceTransformationTree : TreeView
    {
        public DataArtifactReferenceTransformationTree(DataArtifactSchemaItemCollection schemaItems, DataArtifactTransformationCollection transformations)
        {
            Dock = DockStyle.Fill;
            CheckBoxes = transformations == null ? false : true;
            ImageList = Images.Items;
            ImageKey = "Schema Item";
            SelectedImageKey = "Schema Item";

            SchemaItems = schemaItems;
            Transformations = transformations;

            if(schemaItems != null)
            {
                AddTransformations();
            }
        }

        DataArtifactSchemaItemCollection SchemaItems { get; set; }

        DataArtifactTransformationCollection Transformations { get; set; }

        void AddTransformations()
        {
            foreach(DataArtifactSchemaItem schemaItem in SchemaItems)
            {
                TreeNode node = new TreeNode();
                node.Text = schemaItem.Name;
                Nodes.Add(node);

                if(Transformations != null)
                {
                    if(Transformations.Exists(x => x.ReferencedObjectID == schemaItem.ID))
                    {
                        SchemaInclusionDataArtifactTransformation transformation = (SchemaInclusionDataArtifactTransformation)Transformations.FirstOrDefault(x => x.ReferencedObjectID == schemaItem.ID);
                        node.Checked = transformation.Include;
                        node.Tag = transformation;
                    }
                }
            }
        }
    }
}
