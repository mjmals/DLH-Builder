using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLHBuilder.Components;

namespace DLHBuilder.Desktop.UI
{
    class EditorPanel : Panel
    {
        public EditorPanel()
        {
            Dock = DockStyle.Fill;
        }

        public void SetControls(EditorCollection editors)
        {
            Controls.Clear();
            Controls.Add(editors);
        }
    }
}
