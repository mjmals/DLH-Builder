using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Editors
{
    public class PropertyEditor : Editor
    {
        public PropertyEditor(object propertyobject = null)
        {
            Text = "Properties";

            Grid = new PropertyGrid()
            {
                SelectedObject = propertyobject,
                PropertySort = PropertySort.NoSort,
                Dock = DockStyle.Fill
            };

            SetControls();
        }

        public PropertyGrid Grid { get; set; }

        protected override Control[] EditorControls()
        {
            return new Control[]
            {
                Grid
            };
        }
    }
}
