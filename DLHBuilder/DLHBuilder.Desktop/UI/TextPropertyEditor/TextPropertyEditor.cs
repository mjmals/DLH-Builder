using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace DLHBuilder.Desktop.UI
{
    class TextPropertyEditor : Editor
    {
        public TextPropertyEditor(object baseobject, string propertyname)
        {
            Text = "Template Editor";

            BaseObject = baseobject;
            PropertyName = propertyname;

            PropertyText = new RichTextBox() { Dock = DockStyle.Fill };
            PropertyText.Text = (string)BaseObject.GetType().GetProperty(PropertyName).GetValue(BaseObject);
            PropertyText.TextChanged += PropertyTextChanged;
            Controls.Add(PropertyText);
        }

        object BaseObject { get; set; }

        string PropertyName { get; set; }

        RichTextBox PropertyText { get; set; }

        public void PropertyTextChanged(object sender, EventArgs e)
        {
            BaseObject.GetType().GetProperty(PropertyName).SetValue(BaseObject, PropertyText.Text);
        }
    }
}
