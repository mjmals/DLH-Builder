using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DLHBuilder.Components.EditorGrids
{
    public class EditorGridBatchUpdatePanel : EditorGridPanel
    {
        public EditorGridBatchUpdatePanel(IEditorGridCell[] cells, IEditorGridCell sampleCell = null) : base(sampleCell)
        {
            Cells = cells;
        }

        protected IEditorGridCell[] Cells { get; set; }

        public override Control[] PanelControls
        {
            get
            {
                List<Control> output = new List<Control>();
                output.Add(new Label() { Text = "Specify new value: ", Width = 200, Location = new Point(50, 50) });
                output.Add(SelectionControl = GetSelectionControl());

                Button runBtn = new Button() { Text = "Update Selected Cells", Width = 200, Location = new Point(50, 100) };
                runBtn.Click += RunUpdate;
                output.Add(runBtn);

                return output.ToArray();
            }
        }

        Control SelectionControl { get; set; }

        Control GetSelectionControl()
        {
            Control output = new TextBox();

            if(Cell is EditorGridObjectCell)
            {
                output = new ComboBox();
                ComboBox dropBox = (ComboBox)output;
                EditorGridDropdownCell dropCell = (EditorGridDropdownCell)Cell;

                foreach (var item in dropCell.DatasourceDefinition.Values())
                {
                    if (item.Value is string)
                    {
                        dropBox.Items.Add(item.Value);
                    }
                    else
                    {
                        dropBox.DisplayMember = dropCell.DatasourceDefinition.DisplayProperty;
                        dropBox.Items.Add(item.Value);
                    }
                }
            }

            output.Width = 300;

            if(Cell is EditorGridCheckCell)
            {
                output = new CheckBox();
            }

            output.Location = new Point(250, 50);

            return output;
        }

        void RunUpdate(object sender, EventArgs e)
        {
            foreach(IEditorGridCell cell in Cells)
            {
                if(SelectionControl is TextBox)
                {
                    cell.PropertyValue = ((TextBox)SelectionControl).Text;
                }

                if (SelectionControl is CheckBox)
                {
                    cell.PropertyValue = ((CheckBox)SelectionControl).Checked;
                }

                if (SelectionControl is ComboBox)
                {
                    cell.PropertyValue = ((ComboBox)SelectionControl).SelectedItem;
                }

                cell.ProcessCellUpdate();
            }
        }
    }
}
