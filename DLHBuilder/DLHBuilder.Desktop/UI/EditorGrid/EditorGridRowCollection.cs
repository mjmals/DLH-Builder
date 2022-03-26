using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class EditorGridRowCollection : List<EditorGridRow>
    {
        public EditorGridRowCollection()
        {
            AddRows();
        }

        protected virtual void AddRows()
        {

        }
    }
}
