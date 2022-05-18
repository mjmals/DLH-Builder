﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public interface ICompiledSchemaItem
    {
        public string Name { get; set; }

        public string DataType { get; set; }

        public bool IsNullable { get; set; }

        public DataArtifactSchemaItemKeyType KeyType { get; set; }

        public string Definition { get; set; }

        public int Ordinal { get; set; }

        public bool IsLast { get; set; }
    }
}
