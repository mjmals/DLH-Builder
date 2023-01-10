using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructs;

namespace DLHApp.Model.DataStructImporters
{
    public abstract class DataStructImporter
    {
        protected virtual Dictionary<string, Dictionary<string, string>> GetSourceStructures()
        {
            return null;
        }

        public Dictionary<string, Dictionary<string, string>> SourceStructures 
        { 
            get
            {
                if(_sourceStructures == null)
                {
                    _sourceStructures = GetSourceStructures();
                }

                return _sourceStructures;
            }
            set => _sourceStructures = value;
        }

        protected Dictionary<string, Dictionary<string, string>> _sourceStructures { get; set; }

        public virtual DataStruct GetDataStruct(string name)
        {
            DataStruct output = new DataStruct();
            Dictionary<string, string> fields = SourceStructures[name];

            string structDef = "StructType([";
            string fieldsDef = string.Empty;

            foreach(KeyValuePair<string, string> field in fields)
            {
                fieldsDef += "\n\t" + field.Value + ",";
            }

            structDef += fieldsDef.Substring(0, fieldsDef.Length - 1);
            structDef += "\n]);";

            DataStructParser parser = new DataStructParser(structDef, output);
            parser.Parse();

            return output;
        }
    }
}
