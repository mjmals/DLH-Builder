using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DLHApp.Model.DataTypes;
using DLHApp.Model.Connections;

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

        public string SourceConnection { get; set; }

        public Connection SourceConnectionDetail()
        {
            if(string.IsNullOrEmpty(SourceConnection))
            {
                return null;
            }

            foreach(string connectionExtension in Connection.GetConnectionTypeExtensions())
            {
                string connectionFilename = Path.Combine("Connections", string.Format("{0}.{1}", SourceConnection, connectionExtension));

                if (File.Exists(connectionFilename))
                {
                    return Connection.Load(connectionFilename);
                }
            }

            return null;
        }

        public string SourceItemName { get; set; }

        public DataStructRelationshipCollection Relationships { get; set; }

        public override string OutputExtension => "datastruct";

        protected override string OutputContent()
        {
            return DataStructExtractor.Extract(this);
        }

        public new static DataStruct New()
        {
            DataStruct output = new DataStruct();
            output.Fields = new DataStructFieldCollection();
            output.Relationships = new DataStructRelationshipCollection();

            DataStructField idField = new DataStructField();
            idField.Name = "ID";
            idField.DataType = new IntegerDataType();
            idField.IsNullable = false;
            idField.Metadata = new DataStructFieldMetadataCollection();
            idField.Metadata.Add("keytype", "PrimaryKey");
            output.Fields.Add(idField);

            DataStructField updateField = new DataStructField();
            updateField.Name = "LastUpdated";
            updateField.DataType = new StringDataType();
            updateField.IsNullable = false;
            updateField.Metadata = new DataStructFieldMetadataCollection();
            updateField.Metadata.Add("keytype", "Version");
            output.Fields.Add(updateField);

            return output;
        }

        public static DataStruct Load(string name)
        {
            string basePath = (new DataStruct()).BasePath;
            string filePath = Path.Combine(name.StartsWith(basePath) ? "" : basePath, name + (name.EndsWith(".datastruct") ? "" : ".datastruct"));
            string fileContent = File.ReadAllText(filePath);

            return new DataStruct(fileContent) { Name = Path.GetFileNameWithoutExtension(filePath), FolderPath = filePath.Replace(basePath + @"\", "").Replace(@"\" + Path.GetFileName(filePath), "") };
        }
    }
}
