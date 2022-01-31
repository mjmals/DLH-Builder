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
                control.Dock = controls.Count() > 1 ? control.Dock = DockStyle.Top : DockStyle.Fill;

                Controls.Add(control);
            }
        }
    }
}
