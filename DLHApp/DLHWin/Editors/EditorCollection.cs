using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Editors
{
    internal class EditorCollection : TabControl
    {
        public EditorCollection(params Editor[] editors)
        {
            Dock = DockStyle.Fill;

            foreach(Editor editor in editors)
            {
                TabPages.Add(editor);
            }
        }
    }
}
