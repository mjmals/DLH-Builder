﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class TransformationEditorGridColumnCollection : EditorGridColumnCollection
    {
        public TransformationEditorGridColumnCollection()
        {
            Add("Data Source");
            Add("Transformation");
        }
    }
}