using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Editors
{
    public class TransformationEditorGrid : DataGridView
    {
        public TransformationEditorGrid(DataArtifactTransformationCollection transformations, Guid itemID)
        {
            Transformations = transformations;
            ItemID = itemID;

            Dock = DockStyle.Fill;
            AllowUserToAddRows = false;
            RowHeadersVisible = false;
            ReadOnly = true;

            Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Transformation Type", Width = 300 });
            Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Transformation Setting", Width = 500 });

            AddRows();
        }

        DataArtifactTransformationCollection Transformations { get; set; }
        Guid ItemID { get; set; }

        void AddRows()
        {
            foreach(IDataArtifactTransformation transformation in Transformations.Where(x => x.ReferencedObjectID == ItemID))
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = transformation.Title });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = transformation.ToString() });
                row.Tag = transformation;
                Rows.Add(row);
            }
        }
    }
}
