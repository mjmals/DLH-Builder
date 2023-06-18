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

            if(field.Metadata != null || field.KeyTypes != null || field.IsCaseSensitive)
            {
                int metadataIndex = 0;
                string metadataVal = string.Empty;

                if(field.KeyTypes?.Count() > 0)
                {
                    string keytypeValues = string.Empty;

                    foreach(DataStructFieldKeyType keyType in field.KeyTypes)
                    {
                        keytypeValues += (keytypeValues.Length > 0 ? "," : "") + keyType.ToString();
                    }

                    metadataVal = string.Format("keytypes:[{0}]", keytypeValues);
                }

                if(field.IsCaseSensitive)
                {
                    metadataVal += (string.IsNullOrEmpty(metadata) ? "" : ", ") + "casesensitive:true";
                }

                /*foreach(KeyValuePair<string, string> keyValue in field.Metadata)
                {
                    metadataVal += metadataVal.Length > 0 ? "," : "";
                    metadataVal += string.Format("{0}:{1}", keyValue.Key, keyValue.Value);
                    metadataIndex++;
                }*/

                metadata = "{" + metadataVal + "}";
            }

            return string.Format("StructField(\"{0}\", {1}, {2}, {3})", field.Name, field.DataType.FormattedValue(), field.IsNullable.ToString(), metadata);
        }

    }
}
