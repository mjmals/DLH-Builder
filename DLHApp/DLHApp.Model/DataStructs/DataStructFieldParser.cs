using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DLHApp.Model.DataTypes;

namespace DLHApp.Model.DataStructs
{
    internal class DataStructFieldParser
    {
        public DataStructFieldParser(string structFieldText, DataStructField field)
        {
            Field = field;
            structFieldText = structFieldText.Substring(0, structFieldText.Length - 1).Replace("StructField(", "");
            StructFieldConfig = structFieldText.Split(",");
        }

        
        string[] StructFieldConfig { get; set; }

        DataStructField Field { get; set; }

        public void Parse()
        {
            if (StructFieldConfig.Length == 0)
            {
                Console.WriteLine("Error: No parameters supplied");
            }

            for(int i = 0; i < StructFieldConfig.Length; i++)
            {
                StructFieldConfig[i] = StructFieldConfig[i].Trim();
            }

            ParseName();
            ParseDataType();
            ParseNullability();
            ParseMetadata();
        }

        void ParseName()
        {
            Field.Name = StructFieldConfig[0].Replace("\"", "");
        }

        void ParseDataType()
        {
            string datatypeText = string.Empty;

            if(!datatypeText.EndsWith(")"))
            {
                int pos = 1;

                datatypeText = StructFieldConfig[pos];

                while(datatypeText.EndsWith(")") == false)
                {
                    pos++;
                    datatypeText += "," + StructFieldConfig[pos];
                }

                StructFieldConfig[1] = datatypeText;

                if(pos > 1)
                {
                    List<string> configValues = StructFieldConfig.ToList();

                    for(int i = 2; i <= pos; i++)
                    {
                        configValues.RemoveAt(2);
                    }

                    StructFieldConfig = configValues.ToArray();
                }
            }

            Field.DataType = new DataTypeParser(datatypeText).Parse();
        }

        void ParseNullability()
        {
            int checkpos = 2;
            bool isBool = false;

            while(!isBool && checkpos < StructFieldConfig.Length)
            {
                bool.TryParse(StructFieldConfig[checkpos], out isBool);

                if(isBool)
                {
                    Field.IsNullable = bool.Parse(StructFieldConfig[checkpos]);
                }

                checkpos++;
            }
        }

        void ParseMetadata()
        {
            int metadataStartPos = -1;
            int metadataEndPos = -1;
            string metadataVal = string.Empty;

            Field.KeyTypes = new DataStructFieldKeyTypeCollection();
            Field.Metadata = new DataStructFieldMetadataCollection();

            for (int i = 0; i < StructFieldConfig.Length; i++)
            {
                if (StructFieldConfig[i].StartsWith("{"))
                {
                    metadataStartPos = i;
                }

                if (StructFieldConfig[i].StartsWith("{") && StructFieldConfig[i].EndsWith("}"))
                {
                    break;
                }

                if (metadataStartPos > -1)
                {
                    metadataVal += !string.IsNullOrEmpty(metadataVal) ? "," : "";
                    metadataVal += StructFieldConfig[i];
                }


                if (StructFieldConfig[i].EndsWith("}"))
                {
                    metadataEndPos = i;
                }
            }

            if (metadataEndPos > -1)
            {
                StructFieldConfig[metadataStartPos] = metadataVal;
                StructFieldConfig = StructFieldConfig.Take(StructFieldConfig.Length - metadataStartPos + 2).ToArray();
            }

            string metadata = StructFieldConfig[3];
            metadata = metadata.Replace("{", "{\"").Replace(",", "\",\"").Replace(":", "\":\"").Replace("}", "\"}");

            try
            {
                string[] metadataItems = metadata.Split(",");

                for (int i = 0; i < metadataItems.Length; i++)
                {
                    string item = metadataItems[i];

                    if(item.Contains("["))
                    {
                        while(!item.Contains("]"))
                        {
                            i++;
                            item += "," + metadataItems[i];
                        }
                    }

                    ExtractFromMetadata(item);
                }
            }
            catch
            {

            }
            finally 
            { 

            }
        }

        void ExtractFromMetadata(string metadata)
        {
            if(!metadata.Contains(":"))
            {
                return;
            }

            metadata = metadata.Replace("{", "").Replace("})", "").Replace("}", "").Replace("\"", "");
            string[] metadataEntry = metadata.Split(":");
            string metadataKey = metadataEntry[0];
            string metadataValue = metadataEntry[1];

            try
            {
                switch (metadataKey.ToLower())
                {
                    case "keytype":
                        Field.KeyTypes?.Add((DataStructFieldKeyType)Enum.Parse(typeof(DataStructFieldKeyType), metadataValue, true));
                        break;
                    case "keytypes":
                        metadataValue = metadataValue.Replace("[", "").Replace("]", "");
                        string[] keytypes = metadataValue.Trim().Split(",");
                        keytypes.ToList().ForEach((x) => Field.KeyTypes?.Add((DataStructFieldKeyType)Enum.Parse(typeof(DataStructFieldKeyType), x, true)));
                        break;
                    case "casesensitive":
                        Field.IsCaseSensitive = bool.Parse(metadataValue);
                        break;
                    default:
                        Field.Metadata.Add(metadataKey, metadataValue);
                        break;
                }
            }
            catch(Exception e)
            {

            }
        }
    }
}
