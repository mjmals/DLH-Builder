using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHApp.Build.TemplateRenderers;
using DLHWin.Styles;
using DLHWin.Editors.Dialogs;
using DLHWin.Editors.SyntaxHighlighters;

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

        string ExportFileName { get; set; }

        string ScriptText { get; set; }

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

            ToolStripButton refreshButton = new ToolStripButton();
            refreshButton.ImageKey = "Refresh";
            refreshButton.ToolTipText = "Refresh Template from File";
            refreshButton.Click += ChangeScript;
            toolbar.Items.Add(refreshButton);

            ToolStripButton copyButton = new ToolStripButton();
            copyButton.ImageKey = "Copy";
            copyButton.ToolTipText = "Copy script to clipboard";
            copyButton.Click += CopyScript;
            toolbar.Items.Add(copyButton);

            ToolStripButton exportButton = new ToolStripButton();
            exportButton.ImageKey = "Export File";
            exportButton.ToolTipText = "Export script to file";
            exportButton.Click += ExportScript;
            toolbar.Items.Add(exportButton);

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

            ObjectPanel.Controls.Clear();
            ObjectPanel.Controls.Add(ObjectGrid);
            ObjectPanel.Controls.Add(ObjectList);
            output.Controls.Add(ObjectPanel);

            ObjectList.SelectedValueChanged += ListItemChanged;

            return output;
        }

        ToolStripComboBox ScriptSelector = new ToolStripComboBox() { AutoSize = false, Width = 500 };

        RichTextBox ScriptViewer = new RichTextBox() { Dock = DockStyle.Fill, ReadOnly = true };

        RichTextBox ScriptEditor = new RichTextBox() { Dock = DockStyle.Fill, AcceptsTab = true };

        Panel ObjectPanel = new Panel() { Dock = DockStyle.Right, AutoSize = false, Width = 300 };

        ListBox ObjectList = new ListBox() { Dock = DockStyle.Top, Height = 200 };

        PropertyGrid ObjectGrid = new PropertyGrid() { Dock = DockStyle.Fill, HelpVisible = false };

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

                TemplateModelItem templateItems = ModelItem.GetTemplateItems();
                ObjectList.Items.Clear();
                ObjectList.Tag = templateItems;

                foreach(KeyValuePair<string, object> item in templateItems)
                {
                    ObjectList.Items.Add(item.Key);
                }

                ScriptText = renderer.Render(templateFile, templateItems, out outputName);
                ScriptViewer.Text = ScriptText;
                ExportFileName = outputName;
                HighlightText(ScriptViewer);
            }
            catch (Exception e)
            {
                ScriptText = e.Message;
                ScriptViewer.Text = ScriptText;
                ExportFileName = string.Empty;
            }
        }

        void LoadEditor()
        {
            ScriptEditor.TextChanged -= TemplateEdited;
            ScriptEditor.Clear();
            int cursorPos = ScriptEditor.SelectionStart;
            ScriptEditor.Text = File.ReadAllText(Path.Combine("Templates", GetTemplateFile()), Encoding.ASCII);
            ScriptEditor.SelectAll();
            ScriptEditor.SelectionTabs = new int[] { 10, 20, 30, 40 };
            ScriptEditor.SelectionStart = cursorPos;
            ScriptEditor.SelectionLength = 0;
            ScriptEditor.TextChanged += TemplateEdited;
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

        void ListItemChanged(object sender, EventArgs e)
        {
            TemplateModelItem templateItems = (TemplateModelItem)ObjectList.Tag;
            object selectedItem = templateItems[(string)ObjectList.Text];
            ObjectGrid.SelectedObject = selectedItem;
        }

        void ExportScript(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(ExportFileName))
            {
                return;
            }

            if(ExportFileName == "Error.txt")
            {
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.FileName = ExportFileName;
                dialog.DefaultExt = Path.GetExtension(ExportFileName);
                dialog.Filter = string.Format("{0} Files | *.{1}", dialog.DefaultExt.ToUpper(), dialog.DefaultExt);

                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(dialog.FileName, FileMode.OpenOrCreate))
                    {
                        stream.SetLength(0);

                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.Write(ScriptText);
                        }
                    }
                }
            }
        }

        void HighlightText(RichTextBox textBox)
        {
            SyntaxHighlighter highlighter = null;

            switch (Path.GetExtension(ExportFileName))
            {
                case ".sql":
                    highlighter = new SqlSyntaxHighlighter();
                    break;
                case ".py":
                    highlighter = new PythonSyntaxHighlighter();
                    break;
                case ".json":
                    highlighter = new JsonSyntaxHighlighter();
                    break;
                default:
                    break;
            }

            if(highlighter == null)
            {
                return;
            }

            highlighter.Highlight(textBox);
        }
    }
}
