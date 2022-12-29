using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components
{
    public class EditorCollection : TabControl
    {
        public EditorCollection(params Editor[] editors)
        {
            Editors = editors;
            SetEditors();

            Dock = DockStyle.Fill;
        }

        Editor[] Editors { get; set; }
        
        void SetEditors()
        {
            TabPages.Clear();
            TabPages.AddRange(Editors);
        }
    }
}
