using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataApplications
{
    public class SqlServerDataApplication : DataApplication, IModelItem
    {
        public override string OutputExtension => "sqlapp.json";

        public override string? DisplayName => "SQL Server";

        public string DatabaseName { get; set; }

        public static SqlServerDataApplication New()
        {
            return new SqlServerDataApplication()
            {
                Name = "New SQL Server Application"
            };
        }
    }
}
