using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Editors
{
    public class ScriptTemplateEditor : TextPropertyEditor
    {
        public ScriptTemplateEditor(object baseObject, string propertyName, bool enabled = true) : base(baseObject, propertyName, enabled)
        {
            Toolbar.Items.Add(new ToolStripSeparator());
            Toolbar.Items.Add(new ToolStripLabel("Template Engine:"));
            Toolbar.Items.Add(EngineSelector());
        }

        ScriptTemplate Template => (ScriptTemplate)BaseObject;

        ToolStripComboBox EngineSelector()
        {
            ToolStripComboBox output = new ToolStripComboBox();
            output.AutoSize = false;
            output.Items.AddRange(Enum.GetNames(typeof(ScriptTemplateEngineType)).OrderBy(x => x).ToArray());
            output.Enabled = PropertyText.Enabled;
            output.Text = Enum.GetName(typeof(ScriptTemplateEngineType), Template.Engine);
            output.SelectedIndexChanged += SetEngine;

            return output;
        }

        void SetEngine(object sender, EventArgs e)
        {
            ToolStripComboBox selector = (ToolStripComboBox)sender;
            Template.Engine = (ScriptTemplateEngineType)Enum.Parse(typeof(ScriptTemplateEngineType), selector.Text);
        }
    }
}
