using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using System.Reflection;
using Newtonsoft.Json;
using DLHWin.Styles;

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

        public RichTextBox ObjectTextEditor = new RichTextBox() { BorderStyle = BorderStyle.None, Dock = DockStyle.Fill, AcceptsTab = true, Font = new Font("Cascadia Code", 10) };

        public PropertyGrid PropertyEditor = new PropertyGrid();

        public ToolStrip Menu()
        {
            ToolStrip output = new ToolStrip();
            output.ImageList = Images.List;

            ToolStripButton updateBtn = new ToolStripButton();
            updateBtn.Text = "Update from Json";
            updateBtn.ImageKey = "Run";
            updateBtn.Click += UpdateFromJson;
            output.Items.Add(updateBtn);

            return output;
        }

        void UpdateFromJson(object sender, EventArgs e)
        {
            Type type = ModelItem.GetType();
            string sourcePath = ModelItem.SourcePath;
            ModelItem = (IModelItem)JsonConvert.DeserializeObject(ObjectTextEditor.Text, type);
            ModelItem.SourcePath = sourcePath;
            ModelItem.Save();
            PropertyEditor.SelectedObject = ModelItem;
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
