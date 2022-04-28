using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLHBuilder.Generator;
using DLHBuilder.Desktop.Model;

namespace DLHBuilder.Desktop.UI
{
    class ScriptViewer : Editor
    {
        public ScriptViewer(object baseobject, ScriptTemplateReference reference, ScriptTemplateCollection templates)
        {
            string script = string.Format("Script {0} could not be found", reference.Template);
            ScriptTemplate template = new CollatedScriptTemplateCollection(templates).FirstOrDefault(x => x.Path() + "." + x.Name == reference.Template);

            if (template != null)
            {
                ScriptEngine engine = new ScriptEngine(template, baseobject);
                script = engine.Render();
            }

            Controls.Add(ScriptBox(script));
            Controls.Add(ToolBar());
        }

        ToolStrip ToolBar()
        {
            ToolStrip output = new ToolStrip();
            output.ImageList = Images.Items;

            ToolStripButton copybutton = new ToolStripButton();
            copybutton.ImageKey = "Copy";

            return output;
        }

        RichTextBox ScriptBox(string text)
        {
            RichTextBox output = new RichTextBox();
            output.Dock = DockStyle.Fill;
            output.Text = text;

            return output;
        }
    }
}
