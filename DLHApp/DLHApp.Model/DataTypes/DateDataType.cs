using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class DateDataType : TimestampDataType, ITimestampDataType, IDataType
    {
        public override string[] DisplayNames => new string[] { "Date", "DateDataType", "DateType" };

        public override object DefaultValue => new DateTime(1900, 1, 1).Date;
    }
}
