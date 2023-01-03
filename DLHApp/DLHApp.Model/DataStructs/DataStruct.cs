using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataTypes;

namespace DLHApp.Model.DataStructs
{
    public class DataStruct : ModelItem, IModelItem
    {
        public DataStruct()
        {

        }

        public DataStruct(string dataStructText)
        {
            DataStructParser parser = new DataStructParser(dataStructText, this);
            parser.Parse();
        }

        public override string BasePath => GetBasePath("Data Structures");

        public DataStructFieldCollection Fields { get; set; }

        protected override string OutputExtension => "datastruct";

        protected override string OutputContent()
        {
            return DataStructExtractor.Extract(this);
        }

        public new static DataStruct New()
        {
            DataStruct output = new DataStruct();
            output.Fields = new DataStructFieldCollection();

            DataStructField idField = new DataStructField();
            idField.Name = "ID";
            idField.DataType = new IntegerDataType();
            idField.IsNullable = false;
            idField.Metadata = new Dictionary<string, string>();
            idField.Metadata.Add("keytype", "PrimaryKey");
            output.Fields.Add(idField);

            DataStructField updateField = new DataStructField();
            updateField.Name = "LastUpdated";
            updateField.DataType = new StringDataType();
            updateField.IsNullable = false;
            updateField.Metadata = new Dictionary<string, string>();
            updateField.Metadata.Add("keytype", "Version");
            output.Fields.Add(updateField);

            return output;
        }

        public static DataStruct Load(string name)
        {
            string filePath = Path.Combine((new DataStruct()).BasePath, name + ".datastruct");
            string fileContent = File.ReadAllText(filePath);

            return new DataStruct(fileContent);
        }
    }
}
