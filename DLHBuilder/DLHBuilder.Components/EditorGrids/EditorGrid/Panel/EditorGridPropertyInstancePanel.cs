using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.EditorGrids
{
    public class EditorGridPropertyInstancePanel : EditorGridPanel
    {
        public EditorGridPropertyInstancePanel(IEditorGridCell cell, Type instanceType) : base(cell)
        {
            InstanceType = instanceType;
            Controls.Clear();
            SetControls();
            Grid.SelectedObject = Cell.PropertyValue;
        }

        Type InstanceType { get; set; }

        Type[] SelectionItems()
        {
            List<Type> output = new List<Type>();

            foreach(Type item in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => x.IsAssignableTo(InstanceType) && x.IsInterface == false))
            {
                output.Add(item);
            }

            return output.ToArray();
        }

        public override Control[] PanelControls
        {
            get
            {
                return new Control[] { SelectionToolbar(), Grid };
            }
        }

        ToolStrip SelectionToolbar()
        {
            ToolStrip output = new ToolStrip();

            ToolStripLabel selectionLabel = new ToolStripLabel() { Text = "Type:" };
            output.Items.Add(selectionLabel);

            ToolStripComboBox selectionBox = new ToolStripComboBox() { AutoSize = false, Width = 400 };
            selectionBox.Items.AddRange(SelectionItems());
            selectionBox.SelectedItem = Cell.PropertyValue.GetType();
            selectionBox.SelectedIndexChanged += PropertyInstanceChanged;
            output.Items.Add(selectionBox);

            return output;
        }

        PropertyGrid Grid = new PropertyGrid() { Dock = DockStyle.Fill };

        void PropertyInstanceChanged(object sender, EventArgs e)
        {
            ToolStripComboBox dropDownBox = (ToolStripComboBox)sender;
            Type instanceType = (Type)dropDownBox.SelectedItem;
            Cell.PropertyValue = Activator.CreateInstance(instanceType);
            Cell.ProcessCellUpdate();
            Grid.SelectedObject = Cell.PropertyValue;
            Grid.Refresh();
        }
    }
}
