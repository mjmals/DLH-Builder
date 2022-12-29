﻿using System;
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

        void ParseMetadata()
        {
            int metadataStartPos = -1;
            int metadataEndPos = -1;
            string metadataVal = string.Empty;

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
                Field.Metadata = (Dictionary<string, string>)JsonConvert.DeserializeObject(metadata, typeof(Dictionary<string, string>));
            }
            finally { };
        }
    }
}