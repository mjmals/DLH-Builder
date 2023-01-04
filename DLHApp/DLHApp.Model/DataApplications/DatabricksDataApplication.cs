﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataApplications
{
    public class DatabricksDataApplication : DataApplication, IModelItem
    {
        protected override string OutputExtension => "dbxapp.json";

        public override string? DisplayName => "Databricks";

        public static DatabricksDataApplication New()
        {
            return new DatabricksDataApplication();
        }
    }
}
