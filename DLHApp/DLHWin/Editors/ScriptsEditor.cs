using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHApp.Build.TemplateRenderers;

namespace DLHWin.Editors
{
    internal class ScriptsEditor : Editor
    {
        public ScriptsEditor(string modelItemPath, Type modelItemType)
        {
            ModelItemPath = modelItemPath;
            ModelItemType = modelItemType;

            ModelItem = (IModelItem)modelItemType.GetMethod("Load").Invoke(null, new[] { modelItemPath });

            foreach(string template in ModelItem.Templates.OrderBy(x => x))
            {
                ScriptSelector.Items.Add(template);
            }

            ScriptSelector.SelectedIndexChanged += ChangeScript;

            Text = "Scripts";
        }

        string ModelItemPath { get; set; }

        Type ModelItemType { get; set; }

        IModelItem ModelItem { get; set; }

        Panel GetViewerPanel()
        {
            Panel output = new Panel();

            output.Controls.Add(ScriptViewer);

            ToolStrip toolbar = new ToolStrip();
            output.Controls.Add(toolbar);

            ToolStripLabel scriptLabel = new ToolStripLabel() { Text = "Select Script:" };
            toolbar.Items.Add(scriptLabel);
            toolbar.Items.Add(ScriptSelector);

            return output;
        }

        ToolStripComboBox ScriptSelector = new ToolStripComboBox() { AutoSize = false, Width = 500 };

        RichTextBox ScriptViewer = new RichTextBox() { Enabled = false, Dock = DockStyle.Fill };

        protected override Control[] EditorControls()
        {
            return new Control[] { GetViewerPanel() };
        }

        void ChangeScript(object sender, EventArgs e)
        {
            ScriptViewer.Clear();

            string templateFile = ScriptSelector.Text + ".cshtml";
            TemplateRenderer renderer = TemplateRenderer.GetRenderer(templateFile);
            string outputName = string.Empty;
            ScriptViewer.Text =  renderer.Render(templateFile, ModelItem.GetTemplateItems(), out outputName);
        }
    }
}
