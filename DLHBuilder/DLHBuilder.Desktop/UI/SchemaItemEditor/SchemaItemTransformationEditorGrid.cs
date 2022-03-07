using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class SchemaItemTransformationEditorGrid : DataGridView
    {
        public SchemaItemTransformationEditorGrid(DataArtifactTransformationCollection transformations, DataStageCollection stages)
        {
            Transformations = transformations;
            Stages = stages;
            Columns.AddRange(GridColumns());
            Rows.AddRange(GridRows(transformations, stages));
            CellValueChanged += CellEdited;
        }

        DataArtifactTransformationCollection Transformations { get; set; }

        DataStageCollection Stages { get; set; }

        DataGridViewColumn[] GridColumns()
        {
            return new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "Data Stage" },
                new DataGridViewTextBoxColumn() { HeaderText = "Definition" }
            };
        }

        DataGridViewRow[] GridRows(DataArtifactTransformationCollection transformations, DataStageCollection stages)
        {
            List<DataGridViewRow> output = new List<DataGridViewRow>();

            foreach(DataStage stage in stages)
            {
                DataGridViewRow row = new DataGridViewRow();

                DataGridViewTextBoxCell datastage = new DataGridViewTextBoxCell() { Value = stage.Name };
                datastage.Tag = stage;
                row.Cells.Add(datastage);

                DataGridViewTextBoxCell definition = new DataGridViewTextBoxCell();
                definition.Value = Transformations.Exists(x => x.DataStageID == stage.ID) ? Transformations.FirstOrDefault(x => x.DataStageID == stage.ID).Definition : null;
                row.Cells.Add(definition);

                output.Add(row);
            }

            return output.ToArray();
        }

        void CellEdited(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0)
            {
                CancelEdit();
                return;
            }

            DataStage stage = (DataStage)Rows[e.RowIndex].Cells[0].Tag;

            if(!Transformations.Exists(x => x.DataStageID == stage.ID))
            {
                Transformations.Add(new DataArtifactTransformation() { DataStageID = stage.ID });
            }

            Transformations.FirstOrDefault(x => x.DataStageID == stage.ID).Definition = Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
