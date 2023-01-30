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

        DataStruct OutputStruct = new DataStruct();

        public void Parse()
        {
            OutputStruct.Fields = new DataStructFieldCollection();

            StructTextArray = StructText.Split('\n');

            StructFieldPointers = StructTextArray.Select((value, index) => new { Value = value, Index = index })
                .Where(x => x.Value.StartsWith("\tStructField"))
                .Select(x => x.Index)
                .ToArray();

            StructConfigPointers = StructTextArray.Select((value, index) => new { Value = value, Index = index })
                .Where(x => x.Value.StartsWith("\tStructConfig"))
                .Select(x => x.Index)
                .ToArray();

            ParseFields();
            ParseConfig();
        }

        void ParseFields()
        {
            int fieldsEndPointer = StructConfigPointers.Length > 0 ? StructConfigPointers.First() : StructTextArray.ToList().FindLastIndex(x => x.StartsWith("]);"));

            foreach(int fieldPointer in StructFieldPointers)
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
    }
}
