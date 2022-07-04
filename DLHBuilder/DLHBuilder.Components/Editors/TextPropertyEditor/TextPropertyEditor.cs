using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace DLHBuilder.Components.Editors
{
    public class TextPropertyEditor : Editor
    {
        public TextPropertyEditor(object baseObject, string propertyName, bool enabled = true)
        {
            Text = "Template Editor";

            BaseObject = baseObject;
            PropertyName = propertyName;

            PropertyText = new RichTextBox() { Dock = DockStyle.Fill };
            PropertyText.Text = (string)BaseObject.GetType().GetProperty(PropertyName).GetValue(BaseObject);
            PropertyText.Enabled = enabled;

            if (enabled)
            {
                PropertyText.TextChanged += PropertyTextChanged;
            }

            Controls.Add(PropertyText);

            CopyButton.Click += OnCopyButtonPressed;
            Toolbar.Items.Add(CopyButton);
            Controls.Add(Toolbar);
        }

        protected object BaseObject { get; set; }

        protected string PropertyName { get; set; }

        protected RichTextBox PropertyText { get; set; }

        protected ToolStrip Toolbar = new ToolStrip() { ImageList = Images.Items };

        protected ToolStripMenuItem CopyButton = new ToolStripMenuItem() { ImageKey = "Copy", ToolTipText = "Copy to Clipboard" };

        public void PropertyTextChanged(object sender, EventArgs e)
        {
            BaseObject.GetType().GetProperty(PropertyName).SetValue(BaseObject, PropertyText.Text);
        }

        public void OnCopyButtonPressed(object sender, EventArgs e)
        {
            Clipboard.SetText(PropertyText.Text);
            MessageBox.Show("Copied text to clipboard...");
        }
    }
}
