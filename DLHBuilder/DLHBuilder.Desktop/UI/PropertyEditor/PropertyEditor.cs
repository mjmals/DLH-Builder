﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class PropertyEditor : PropertyGrid
    {
        public PropertyEditor(DockStyle dock, object propertyobject = null)
        {
            Dock = dock;
            SelectedObject = propertyobject;
            PropertySort = PropertySort.NoSort;
        }
    }
}
