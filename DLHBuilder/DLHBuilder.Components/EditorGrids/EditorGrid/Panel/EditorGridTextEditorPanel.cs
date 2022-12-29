using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.EditorGrids
{
    public class EditorGridTextEditorPanel : EditorGridPanel
    {
        public EditorGridTextEditorPanel(IEditorGridCell cell) : base(cell)
        {
            TextEditor.Text = cell.PropertyValue.ToString();
            TextEditor.TextChanged += OnTextUpdated;
        }

        public override Control[] PanelControls
        {
            get
            {
                return new Control[]
                {
                    TextEditor,
                    ToolBar
                };
            }
        }

        RichTextBox TextEditor = new RichTextBox() { Dock = DockStyle.Fill };

        ToolStrip ToolBar = new ToolStrip();

        void OnTextUpdated(object sender, EventArgs e)
        {
            Cell.PropertyValue = TextEditor.Text;
            Cell.ProcessCellUpdate();
        }
    }
}
