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

            if(!string.IsNullOrEmpty(dataStruct.SourceConnection))
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

            return output;
        }
    }
}
