using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class HierarchyDataType : DataType, IDataType
    {
        public HierarchyDataType()
        {

        }

        public HierarchyDataType(string length)
        {
            Length = Convert.ToInt32(length);
        }

        public override string[] DisplayNames => new string[] { "Hierarchy", "HierarchyDataType", "HierarchyType" };

        public int Length { get; set; }

        public override string FormattedValue()
        {
            return base.FormattedValue();
        }

        public override object DefaultValue => string.Empty;
    }
}
