using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Editors
{
    public class TransformationEditor : Editor
    {
        public TransformationEditor(DataArtifactTransformationCollection transformations, Guid itemID)
        {
            Transformations = transformations;
            ItemID = itemID;
            Text = "Transformations";

            Controls.Add(PropertyPanel);
            Controls.Add(GridPanel);
            Controls.Add(Toolbar = new TransformationEditorToolbar(Transformations, ItemID));

            GridPanel.Controls.Add(Grid = new TransformationEditorGrid(Transformations, ItemID));
            PropertyPanel.Controls.Add(Properties);

            Grid.SelectionChanged += GridRowChanged;

            Transformations.CollectionModified += TransformationsModified;
            Resize += OnEditorResize;
        }

        DataArtifactTransformationCollection Transformations { get; set; }

        Guid ItemID { get; set; }

        Panel GridPanel = new Panel() { Dock = DockStyle.Top };

        Panel PropertyPanel = new Panel() { Dock = DockStyle.Fill };

        PropertyGrid Properties = new PropertyGrid() { Dock = DockStyle.Fill };

        TransformationEditorGrid Grid { get; set; }

        TransformationEditorToolbar Toolbar { get; set; }

        void OnEditorResize(object sender, EventArgs e)
        {
            GridPanel.Height = this.Height / 2;
        }

        void GridRowChanged(object sender, EventArgs e)
        {
            if (Grid.SelectedCells.Count > 0)
            {
                Properties.SelectedObject = Grid.SelectedCells[0].OwningRow.Tag;
            }
        }

        void TransformationsModified(object sender, EventArgs e)
        {
            GridPanel.Controls.Clear();
            GridPanel.Controls.Add(Grid = new TransformationEditorGrid(Transformations, ItemID));
            Grid.SelectionChanged += GridRowChanged;
        }
    }
}
