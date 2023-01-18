using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHApp.Build.TemplateRenderers;
using DLHWin.Styles;
using DLHWin.Editors.Dialogs;

namespace DLHWin.Editors
{
    internal class ScriptsEditor : Editor
    {
        public ScriptsEditor(string modelItemPath, Type modelItemType)
        {
            ModelItemPath = modelItemPath;
            ModelItemType = modelItemType;

            ModelItem = (IModelItem)modelItemType.GetMethod("Load").Invoke(null, new[] { modelItemPath });

            LoadScriptSelector();

            ScriptSelector.SelectedIndexChanged += ChangeScript;
            ScriptEditor.TextChanged += TemplateEdited;

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
            toolbar.ImageList = Images.List;
            output.Controls.Add(toolbar);

            ToolStripLabel scriptLabel = new ToolStripLabel() { Text = "Select Script:" };
            toolbar.Items.Add(scriptLabel);
            toolbar.Items.Add(ScriptSelector);

            ToolStripButton copyButton = new ToolStripButton();
            copyButton.ImageKey = "Copy";
            copyButton.ToolTipText = "Copy script to clipboard";
            copyButton.Click += CopyScript;
            toolbar.Items.Add(copyButton);

            ToolStripButton manageButton = new ToolStripButton();
            manageButton.ImageKey = "Script Mapping";
            manageButton.ToolTipText = "Manage Template Mappings";
            manageButton.Click += ManageScripts;
            toolbar.Items.Add(manageButton);

            ToolStripButton showhideButton = new ToolStripButton();
            showhideButton.ImageKey = "Hide";
            showhideButton.ToolTipText = @"Show\Hide template editor";
            showhideButton.Click += UpdateEditorVisibility;
            toolbar.Items.Add(showhideButton);

            return output;
        }

        Panel GetEditorPanel()
        {
            Panel output = new Panel();
            output.Controls.Add(ScriptEditor);
            return output;
        }

        ToolStripComboBox ScriptSelector = new ToolStripComboBox() { AutoSize = false, Width = 500 };

        RichTextBox ScriptViewer = new RichTextBox() { Dock = DockStyle.Fill, ReadOnly = true };

        RichTextBox ScriptEditor = new RichTextBox() { Dock = DockStyle.Fill, AcceptsTab = true };

        protected override Control[] EditorControls()
        {
            return new Control[] { GetViewerPanel(), GetEditorPanel() };
        }

        string GetTemplateFile()
        {
            return ScriptSelector.Text + ".cshtml";
        }

        void LoadScriptSelector()
        {
            ScriptSelector.Items.Clear();

            foreach (string template in ModelItem.Templates.OrderBy(x => x))
            {
                ScriptSelector.Items.Add(template);
            }
        }

        void LoadViewer()
        {
            ScriptViewer.Clear();
            string templateFile = GetTemplateFile();

            try
            {
                TemplateRenderer renderer = TemplateRenderer.GetRenderer(templateFile);
                string outputName = string.Empty;
                ScriptViewer.Text = renderer.Render(templateFile, ModelItem.GetTemplateItems(), out outputName);
            }
            catch (Exception e)
            {
                ScriptViewer.Text = e.Message;
            }
        }

        void LoadEditor()
        {
            ScriptEditor.Clear();
            ScriptEditor.Text = File.ReadAllText(Path.Combine("Templates", GetTemplateFile()));
        }

        void ChangeScript(object sender, EventArgs e)
        {
            LoadViewer();
            LoadEditor();
        }

        void TemplateEdited(object sender, EventArgs e)
        {
            using (FileStream stream = new FileStream(Path.Combine("Templates", GetTemplateFile()), FileMode.OpenOrCreate))
            {
                stream.SetLength(0);

                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    writer.Write(ScriptEditor.Text);
                }
            }
            
            LoadViewer();
        }

        void CopyScript(object sender, EventArgs e)
        {
            Clipboard.SetText(ScriptViewer.Text);
        }

        void UpdateEditorVisibility(object sender, EventArgs e)
        {
            if(ScriptEditor.Parent.Visible == true)
            {
                ScriptEditor.Parent.Visible = false;
                ScriptViewer.Parent.Dock = DockStyle.Fill;
                return;
            }

            if (ScriptEditor.Parent.Visible == false)
            {
                ScriptEditor.Parent.Visible = true;
                ScriptViewer.Parent.Dock = DockStyle.None;
                return;
            }
        }

        void ManageScripts(object sender, EventArgs e)
        {
            TemplateMappingDialog dialog = new TemplateMappingDialog(ModelItem.Templates);
            dialog.ShowDialog();


            ModelItem.Save();
            LoadScriptSelector();
        }
    }
}
