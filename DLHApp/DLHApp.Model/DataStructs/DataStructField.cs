﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DLHApp.Model.DataTypes;

namespace DLHApp.Model.DataStructs
{
    public class DataStructField : ModelItem, IModelItem
    {
        public DataStructField()
        {
            Metadata = new Dictionary<string, string>();
        }

        public DataStructField(string structFieldText)
        {
            DataStructFieldParser parser = new DataStructFieldParser(structFieldText, this);
            parser.Parse();
        }

        public IDataType DataType { get; set; }

        public bool IsNullable { get; set; }

        public Dictionary<string, string> Metadata { get; set; }

        public new string OutputContent()
        {
            return DataStructFieldExtractor.Extract(this);
        }

        public override void Save() { }
    }
}