using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataStructs
{
    public static class DataStructExtractor
    {
        public static string Extract(DataStruct dataStruct)
        {
            string output = "StructType([";

            for(int i = 0; i < dataStruct.Fields.Count; i++)
            {
                output += string.Format("\n\t{0}", dataStruct.Fields[i].OutputContent());
                output += i == (dataStruct.Fields.Count - 1) ? string.Empty : ",";
            }

            if (dataStruct.Relationships != null)
            {
                foreach (DataStructRelationship relationship in dataStruct.Relationships)
                {
                    if (output.Split("\n").Length > 1)
                    {
                        output += ",";
                    }

                    string joins = string.Join(@", ", relationship.Joins.Select(x => string.Format("StructJoin({0},{1}{2})", x.SourceField, x.TargetField, (x.IsCaseSensitive ? ",true" : ""))));
                    string relText = string.Format("StructRelationship(Source={0}, Joins=[{1}], Output={2})", relationship.SourceDataStruct, joins, relationship.OutputField);
                    output += string.Format("\n\t{0}", relText);
                }
            }

            if (!string.IsNullOrEmpty(dataStruct.SourceConnection))
            {
                if(output.Split("\n").Length > 1)
                {
                    output += ",";
                }

                output += string.Format("\n\tStructConfig(\"{0}\", \"{1}\")", "ConnectionName", dataStruct.SourceConnection);
            }

            if (!string.IsNullOrEmpty(dataStruct.SourceItemName))
            {
                if (output.Split("\n").Length > 1)
                {
                    output += ",";
                }

                output += string.Format("\n\tStructConfig(\"{0}\", \"{1}\")", "SourceItemName", dataStruct.SourceItemName);
            }

            output += "\n]);";


            foreach(DataStructField field in dataStruct.Fields)
            {
                if(field.Metadata == null)
                {
                    continue;
                }

                output += "\n";

                foreach(var metadata in field.Metadata)
                {
                    output += string.Format("\nstruct.Fields[\"{0}\"].Metadata.Add(\"{1}\", \"{2}\");", field.Name, metadata.Key, metadata.Value);
                }
            }

            return output;
        }
    }
}
