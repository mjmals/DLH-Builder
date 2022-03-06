using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class Editor : TabPage
    {
        protected virtual EditorControl[] EditorControls { get; set; }

        public void SetControls()
        {
            Controls.Clear();

            foreach (EditorControl control in EditorControls)
            {
                switch (EditorControls.Count() > 1)
                {
                    case true:
                        int index = EditorControls.ToList().IndexOf(control);
                        control.Dock = index == 0 ? DockStyle.Top : DockStyle.Fill;
                        control.Height = index == 0 ? (Height / EditorControls.Count()) : control.Height;
                        break;
                    case false:
                        control.Dock = DockStyle.Fill;
                        break;
                }

                Controls.Add(control);
            }
        }
    }
}
