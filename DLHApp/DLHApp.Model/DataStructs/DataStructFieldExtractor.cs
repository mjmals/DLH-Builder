using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHApp.Model.DataStructs
{
    public static class DataStructFieldExtractor
    {
        public static string Extract(DataStructField field)
        {
            string metadata = "{}";

            if(field.Metadata != null && field.Metadata.Count > 0)
            {
                int metadataIndex = 0;
                string metadataVal = string.Empty;

                foreach(KeyValuePair<string, string> keyValue in field.Metadata)
                {
                    metadataVal += string.Format("{0}:{1}", keyValue.Key, keyValue.Value);
                    metadataVal += metadataIndex == (field.Metadata.Count - 1) ? string.Empty : ", ";
                    metadataIndex++;
                }

                metadata = "{" + metadataVal + "}";
            }

            return string.Format("StructField(\"{0}\", {1}, {2}, {3})", field.Name, field.DataType.FormattedValue(), field.IsNullable.ToString(), metadata);
        }

    }
}
