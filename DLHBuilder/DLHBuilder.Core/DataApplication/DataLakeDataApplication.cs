using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;

namespace DLHBuilder
{
    public class DataLakeDataApplication : DataApplication
    {
        public static DataLakeDataApplication New()
        {
            DataLakeDataApplication output = new DataLakeDataApplication();
            output.Name = "<New Data Lake Application>";

            return output;
        }
    }
}
