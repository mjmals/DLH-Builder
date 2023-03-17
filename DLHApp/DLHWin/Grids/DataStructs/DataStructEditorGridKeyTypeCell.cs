using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructs;

namespace DLHWin.Grids.DataStructs
{
    internal class DataStructEditorGridKeyTypeCell : EditorGridTextCell
    {
        public override void SetValue(object value)
        {
            DataStructFieldKeyTypeCollection keyTypes = (DataStructFieldKeyTypeCollection)value;

            if (keyTypes != null && keyTypes.Count > 0)
            {
                Value = string.Join(",", keyTypes.Select(x => x.ToString()).ToArray());
                return;
            }

            Value = string.Empty;
        }
    }
}
