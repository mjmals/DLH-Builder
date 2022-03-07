﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DLHBuilder
{
    public class DataConnectionCollection : BuilderCollection<DataConnection>
    {
        protected override string DirectoryName => "Connections";
    }
}
