using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class EditorPanel : Panel
    {
        public EditorPanel()
        {
            Dock = DockStyle.Fill;
        }

        public void SetControls(params Control[] controls)
        {
            Controls.Clear();

            foreach(Control control in controls)
            {
                switch(controls.Count() > 1)
                {
                    case true:
                        int index = controls.ToList().IndexOf(control);
                        control.Dock = index == 0 ? DockStyle.Top : DockStyle.Fill;
                        control.Height = index == 0 ? (Height / controls.Count()) : control.Height;
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
