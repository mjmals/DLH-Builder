using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHBuilder.Desktop.UI
{
    class EditorGridCellController
    {
        public EditorGridCellController()
        {

        }

        public object BaseObject { get; set; }

        public PropertyInfo BaseProperty { get; set; }

        public object UpdateValue { get; set; }

        public EventHandler UpdateEvent { get; set; }
    }
}
