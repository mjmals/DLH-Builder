using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Editors;

namespace DLHWin.Main
{
    internal class EditorPanel : Panel
    {
        public EditorPanel()
        {
            BorderStyle = BorderStyle.FixedSingle;
        }

        public void SetControls(EditorCollection editors)
        {
            Controls.Clear();
            Controls.Add(editors);
        }
    }
}
