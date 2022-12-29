using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class MSSQLDataStage : DataStage
    {
        public string Schema { get; set; }

        public override string ToString()
        {
            return string.Format("{0} (MSSQL)", Name);
        }

        public static MSSQLDataStage New()
        {
            MSSQLDataStage output = new MSSQLDataStage();
            output.ID = Guid.NewGuid();
            output.Name = "<New MSSQL Stage>";

            return output;
        }
    }
}
