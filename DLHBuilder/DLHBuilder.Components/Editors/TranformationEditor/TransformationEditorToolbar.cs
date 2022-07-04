using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace DLHBuilder.Components.Editors
{
    public class TransformationEditorToolbar : ToolStrip
    {
        public TransformationEditorToolbar(DataArtifactTransformationCollection transformations, Guid itemID)
        {
            Transformations = transformations;
            ItemID = itemID;
            Items.Add(AddTransformationDropDown);
            AddTransformationDropDownButtons();
        }

        DataArtifactTransformationCollection Transformations { get; set; }

        Guid ItemID { get; set; }

        ToolStripDropDownButton AddTransformationDropDown = new ToolStripDropDownButton()
        {
            Text = "Add Transformation",
            AutoSize = false,
            Width = 150
        };

        void AddTransformationDropDownButtons()
        {
            foreach(Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => x.IsAssignableTo(typeof(IDataArtifactTransformation)) && x.IsInterface == false && x.IsAbstract == false))
            {
                IDataArtifactTransformation transformation = (IDataArtifactTransformation)Activator.CreateInstance(type);

                ToolStripButton button = new ToolStripButton();
                button.Text = transformation.Title;
                button.AutoSize = false;
                button.Width = 150;
                button.Tag = type;
                button.Click += TransformationDropDownSelected;
                AddTransformationDropDown.DropDownItems.Add(button);
            }
        }

        void TransformationDropDownSelected(object sender, EventArgs e)
        {
            Type transformationType = (Type)((ToolStripButton)sender).Tag;

            if(Transformations.Exists(x => x.ReferencedObjectID == ItemID && x.GetType() == transformationType))
            {
                MessageBox.Show("A transformation of this type already exists for this item");
                return;
            }

            IDataArtifactTransformation transformation = (IDataArtifactTransformation)Activator.CreateInstance(transformationType);
            transformation.ID = Guid.NewGuid();
            transformation.ReferencedObjectID = ItemID;
            Transformations.Add(transformation);
        }
    }
}
