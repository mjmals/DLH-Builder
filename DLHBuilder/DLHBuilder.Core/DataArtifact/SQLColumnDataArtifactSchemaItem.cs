using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace DLHBuilder
{
    public class SQLColumnDataArtifactSchemaItem : DataArtifactSchemaItem
    {
        public SqlDataType DataType { get; set; }

        public bool IsPrimaryKey { get; set; }

        public int Length { get; set; }

        public int Precision { get; set; }

        public int Scale { get; set; }

        public bool IsNullable { get; set; }

        public bool IsMax { get; set; }

        public bool IsVersion { get; set; }
    }
}
