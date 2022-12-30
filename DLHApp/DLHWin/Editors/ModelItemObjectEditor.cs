using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using System.Reflection;
using Newtonsoft.Json;

namespace DLHWin.Editors
{
    internal class ModelItemObjectEditor : Editor
    {
        public ModelItemObjectEditor(string modelItemPath, Type modelItemType) : base()
        {
            ModelItem = (IModelItem)modelItemType.GetMethod("Load").Invoke(null, new[] { modelItemPath });
            PropertyEditor.SelectedObject = ModelItem;
            PropertyEditor.PropertyValueChanged += SetObjectTextEditor;
            Text = ModelItem.Name;
            ObjectTextEditorPanel.Controls.Add(ObjectTextEditor);
            ObjectTextEditorPanel.Controls.Add(Menu());
            SetObjectTextEditor(null, null);
        }

        public IModelItem ModelItem { get; set; }

        public Panel ObjectTextEditorPanel = new Panel();

        public RichTextBox ObjectTextEditor = new RichTextBox() { BorderStyle = BorderStyle.None, Dock = DockStyle.Fill };

        public PropertyGrid PropertyEditor = new PropertyGrid();

        public ToolStrip Menu()
        {
            return new ToolStrip();
        }

        protected override Control[] EditorControls()
        {
            return new Control[] { ObjectTextEditorPanel, PropertyEditor };
        }

        void SetObjectTextEditor(object sender, EventArgs e)
        {
            ObjectTextEditor.Text = JsonConvert.SerializeObject(ModelItem, Formatting.Indented);
        }
    }
}
