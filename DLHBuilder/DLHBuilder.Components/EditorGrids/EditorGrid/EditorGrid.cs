using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.EditorGrids
{
    public abstract class EditorGrid : Editor
    {
        public EditorGrid(object[] baseObjects)
        {
            BaseObjects = baseObjects;
            Controls.Add(Grid = new EditorGridStructure(ColumnDefinitions, BaseObjects));
            Grid.CellControlChange += UpdateControlDisplay;
        }

        public object[] BaseObjects { get; set; }

        public virtual EditorGridColumnDefinition[] ColumnDefinitions { get; }

        protected EditorGridStructure Grid { get; set; }

        protected EditorGridPanel Panel { get; set; }

        public void UpdateControlDisplay(object sender, EditorGridControlChangeEventArgs e)
        {
            Controls.Remove(Panel);
            Grid.Dock = DockStyle.Fill;

            if (e.Panel != null)
            {
                Panel = e.Panel;
                Panel.Dock = DockStyle.Bottom;
                Panel.Height = this.Height / 2;
                Controls.Add(Panel);

                Grid.Dock = DockStyle.Top;
                Grid.Height = this.Height / 2;
                return;
            }
        }
    }
}
