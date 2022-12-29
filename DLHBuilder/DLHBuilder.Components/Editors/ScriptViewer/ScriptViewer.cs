using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLHBuilder.Generator;
using DLHBuilder.Components.Model;

namespace DLHBuilder.Components.Editors
{
    public class ScriptViewer : Editor
    {
        public ScriptViewer(object baseobject, ScriptTemplateReference reference, ScriptTemplateCollection templates)
        {
            string script = string.Format("Script {0} could not be found", reference.Template);
            ScriptTemplate template = new CollatedScriptTemplateCollection(templates).FirstOrDefault(x => x.Path() + "." + x.Name == reference.Template);
            Text = template?.Name;

            if (template != null)
            {
                ScriptEngine engine = new ScriptEngine(template, baseobject);
                script = engine.Render();
            }

            Controls.Add(ScriptBox = NewScriptBox(script));
            Controls.Add(ToolBar());
        }

        ToolStrip ToolBar()
        {
            ToolStrip output = new ToolStrip();
            output.ImageList = Images.Items;

            ToolStripButton copyButton = new ToolStripButton();
            copyButton.ImageKey = "Copy";
            copyButton.ToolTipText = "Copy to Clipboard";
            copyButton.Click += CopyText;
            output.Items.Add(copyButton);

            return output;
        }

        public RichTextBox ScriptBox { get; set; }

        RichTextBox NewScriptBox(string text)
        {
            RichTextBox output = new RichTextBox();
            output.Dock = DockStyle.Fill;
            output.Text = text;
            output.ReadOnly = true;

            return output;
        }

        void CopyText(object sender, EventArgs e)
        {
            Clipboard.SetText(ScriptBox.Text);
            MessageBox.Show("Text copied to Clipboard...");
        }
    }
}
