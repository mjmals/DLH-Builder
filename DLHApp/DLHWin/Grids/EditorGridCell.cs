using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Grids
{
    internal abstract class EditorGridCell : DataGridViewCell
    {
        public abstract Type ColumnType { get; }

        public abstract Type CellType { get; }

        public string BaseProperty { get; set; }

        public virtual void SetValue(object value)
        {
            Value = value;
        }

        public virtual void UpdateBaseProperty(object baseObject)
        {
            baseObject.GetType().GetProperty(BaseProperty).SetValue(baseObject, this.Value);
        }

    }
}
