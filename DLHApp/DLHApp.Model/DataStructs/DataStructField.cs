using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using DLHApp.Model.DataTypes;

namespace DLHApp.Model.DataStructs
{
    public class DataStructField : ModelItem, IModelItem
    {
        public DataStructField()
        {
            KeyTypes = new DataStructFieldKeyTypeCollection();
            Metadata = new DataStructFieldMetadataCollection();
        }

        public DataStructField(string structFieldText)
        {
            DataStructFieldParser parser = new DataStructFieldParser(structFieldText, this);
            parser.Parse();
        }

        public new string? Name { get => base.Name; set => base.Name = value; }

        public IDataType DataType { get; set; }

        public DataStructFieldKeyTypeCollection KeyTypes { get; set; }

        public bool IsNullable { get; set; }

        public bool IsCaseSensitive { get; set; }

        public DataStructFieldMetadataCollection Metadata { get; set; }

        public new string OutputContent()
        {
            return DataStructFieldExtractor.Extract(this);
        }

        public override void Save() { }
    }
}
