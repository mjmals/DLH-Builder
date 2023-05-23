using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DLHApp.Model.DataStructs
{
    public class DataStructParser
    {
        public DataStructParser(string structText, DataStruct dataStruct)
        {
            StructText = structText;
            OutputStruct = dataStruct;
        }

        string StructText { get; set; }

        string[] StructTextArray { get; set; }

        int[] StructFieldPointers { get; set; }

        int[] StructConfigPointers { get; set; }

        int[] StructRelationshipPointers { get; set; }

        DataStruct OutputStruct = new DataStruct();

        public void Parse()
        {
            OutputStruct.Fields = new DataStructFieldCollection();
            OutputStruct.Relationships = new DataStructRelationshipCollection();

            StructTextArray = StructText.Split('\n');

            StructFieldPointers = StructTextArray.Select((value, index) => new { Value = value, Index = index })
                .Where(x => x.Value.StartsWith("\tStructField"))
                .Select(x => x.Index)
                .ToArray();

            StructRelationshipPointers = StructTextArray.Select((value, index) => new { Value = value, Index = index })
                .Where(x => x.Value.StartsWith("\tStructRelationship"))
                .Select(x => x.Index)
                .ToArray();

            StructConfigPointers = StructTextArray.Select((value, index) => new { Value = value, Index = index })
                .Where(x => x.Value.StartsWith("\tStructConfig"))
                .Select(x => x.Index)
                .ToArray();

            ParseFields();
            ParseRelationships();
            ParseConfig();
            ParseFieldMetadata();
        }

        void ParseFields()
        {
            int fieldsEndPointer = StructConfigPointers.Length > 0 ? StructConfigPointers.First() : StructTextArray.ToList().FindLastIndex(x => x.StartsWith("]);"));

            if (StructRelationshipPointers.Length > 0)
            {
                fieldsEndPointer = StructRelationshipPointers.First();
            }

            foreach (int fieldPointer in StructFieldPointers)
            {
                string fieldText = string.Empty;

                int fieldStart = fieldPointer;
                int fieldEnd = StructFieldPointers.ToList().Where(x => x > fieldPointer).FirstOrDefault();
                fieldEnd = fieldEnd == 0 ? fieldsEndPointer : fieldEnd;
                
                for(int i = fieldStart; i < fieldEnd; i++)
                {
                    fieldText += StructTextArray[i];
                }

                fieldText = fieldText.EndsWith("),") ? fieldText.Substring(0, fieldText.LastIndexOf(",")) : fieldText;
                fieldText = fieldText.Replace("\tStructField", "StructField");

                DataStructField field = new DataStructField(fieldText);
                OutputStruct.Fields.Add(field);
            }
        }

        void ParseRelationships()
        {
            int relsEndPointer = StructConfigPointers.Length > 0 ? StructConfigPointers.First() : StructTextArray.ToList().FindLastIndex(x => x.StartsWith("]);"));

            foreach (int relPointer in StructRelationshipPointers)
            {
                string relText = string.Empty;

                int relStart = relPointer;
                int relEnd = StructFieldPointers.ToList().Where(x => x > relPointer).FirstOrDefault();
                relEnd = relEnd == 0 ? relsEndPointer : relEnd;

                for (int i = relStart; i < relEnd; i++)
                {
                    relText += StructTextArray[i];
                }

                relText = relText.EndsWith("),") ? relText.Substring(0, relText.LastIndexOf(",")) : relText;
                relText = relText.Replace("\tStructRelationship", "StructRelationship");

                DataStructRelationship relationship = new DataStructRelationship(relText);
                OutputStruct.Relationships.Add(relationship);
            }
        }

        void ParseConfig()
        {
            int configEndPointer = StructTextArray.ToList().FindLastIndex(x => x.StartsWith("]);"));

            foreach(int configPointer in StructConfigPointers)
            {
                string configText = StructTextArray[configPointer];
                configText = configText.Substring(configText.IndexOf("(") + 1);
                configText = configText.Substring(0, configText.LastIndexOf(")"));

                string[] configData = configText.Split(",");
                
                for (int i = 0; i < configData.Length; i++)
                {
                    configData[i] = configData[i].TrimStart().TrimEnd().Replace("\"", "");
                }

                switch(configData[0].ToLower())
                {
                    case "connectionname":
                        OutputStruct.SourceConnection = configData[1];
                        break;
                    case "sourceitemname":
                        OutputStruct.SourceItemName = configData[1];
                        break;
                }
            }
        }

        void ParseFieldMetadata()
        {
            string[] metadataLines = StructTextArray.Where(x => x.StartsWith("struct.Fields")).ToArray();

            foreach (string line in metadataLines)
            {
                try
                {
                    int fieldNameStartPos = line.IndexOf("Fields[") + 8;
                    int fieldNameEndPos = line.IndexOf("]", fieldNameStartPos + 1) - 1;
                    string fieldName = line.Substring(fieldNameStartPos, fieldNameEndPos - fieldNameStartPos);

                    int metadataStartPos = line.IndexOf("(") + 1;
                    int metadataEndPos = line.IndexOf(")", metadataStartPos + 1);
                    string[] metadata = line.Substring(metadataStartPos, metadataEndPos - metadataStartPos).Replace("\"", "").Split(",");

                    DataStructField field = OutputStruct.Fields.FirstOrDefault(x => x.Name.ToLower() == fieldName.ToLower());

                    if(field != null)
                    {
                        if(field.Metadata == null)
                        {
                            continue;
                        }

                        if (!field.Metadata.ContainsKey(metadata[0]))
                        {
                            field.Metadata.Add(metadata[0], metadata[1].Trim());
                        }
                    }
                }
                catch {  }
            }
        }
    }
}
