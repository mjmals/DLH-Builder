using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class SQLDataApplication : DataApplication
    {
        public static SQLDataApplication New()
        {
            SQLDataApplication output = new SQLDataApplication();
            output.Name = "<New SQL Application>";

            return output;
        }
    }
}
