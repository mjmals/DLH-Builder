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
            ModelItemPath = modelItemPath;
            ModelItemType = modelItemType;
            LoadModelItem();

            Text = ModelItem.Name;

            PropertyPanel.Controls.AddRange(new Control[] { PropertyEditor, ErrorBox });
            PropertyEditor.SelectedObject = ModelItem;
            PropertyEditor.PropertyValueChanged += SetObjectTextEditor;
            PropertyEditor.PropertyValueChanged += PropertyUpdated;
            PropertyEditor.ExpandAllGridItems();
            
            ObjectTextEditorPanel.Controls.Add(ObjectTextEditor);
            ObjectTextEditorPanel.Controls.Add(Menu());
            ObjectTextEditor.TextChanged += TextEditorUpdated;
            
            SetObjectTextEditor(null, null);
        }

        public IModelItem ModelItem { get; set; }

        public string ModelItemPath { get; set; }

        public Type ModelItemType { get; set; }

        public Panel ObjectTextEditorPanel = new Panel();

        public RichTextBox ObjectTextEditor = new RichTextBox() { BorderStyle = BorderStyle.None, Dock = DockStyle.Fill, AcceptsTab = true, SelectionTabs = new int[] { 10, 20, 30, 40 }, Font = new Font("Cascadia Code", 10) };

        public Panel PropertyPanel = new Panel() { Dock = DockStyle.Fill };

        public PropertyGrid PropertyEditor = new PropertyGrid() { Dock = DockStyle.Fill };

        public RichTextBox ErrorBox = new RichTextBox() { Dock = DockStyle.Fill, Enabled = false, Visible = false };

        public ToolStrip Menu()
        {
            ToolStrip output = new ToolStrip();
            output.ImageList = Images.List;

            ToolStripButton refreshBtn = new ToolStripButton();
            refreshBtn.Text = "Refresh";
            refreshBtn.ImageKey = "Refresh";
            refreshBtn.Click += RefreshEditor;
            output.Items.Add(refreshBtn);

            return output;
        }


        protected override Control[] EditorControls()
        {
            return new Control[] { ObjectTextEditorPanel, PropertyPanel };
        }

        void LoadModelItem()
        {
            ModelItem = (IModelItem)ModelItemType.GetMethod("Load").Invoke(null, new[] { ModelItemPath });
        }

        void SetObjectTextEditor(object sender, EventArgs e)
        {
            ObjectTextEditor.Text = JsonConvert.SerializeObject(ModelItem, Formatting.Indented);
        }

        void PropertyUpdated(object sender, PropertyValueChangedEventArgs e)
        {
            ModelItem.Save();
        }

        void TextEditorUpdated(object sender, EventArgs e)
        {
            PropertyEditor.Visible = true;
            ErrorBox.Visible = false;
            string modelItemName = ModelItem.Name;

            try
            {
                Type type = ModelItem.GetType();
                string sourcePath = ModelItem.SourcePath;
                
                ModelItem = (IModelItem)JsonConvert.DeserializeObject(ObjectTextEditor.Text, type);
                ModelItem.Name = modelItemName;
                ModelItem.SourcePath = sourcePath;
                ModelItem.Save();
                
                PropertyEditor.SelectedObject = ModelItem;
            }
            catch(Exception ex)
            {
                PropertyEditor.Visible = false;
                ErrorBox.Visible = true;

                ErrorBox.Text = string.Format("Error parsing object data: \n{0}", ex.Message);
            }
        }

        void RefreshEditor(object sender, EventArgs e)
        {
            LoadModelItem();
            SetObjectTextEditor(null, null);
            PropertyEditor.SelectedObject = ModelItem;
        }
    }
}
