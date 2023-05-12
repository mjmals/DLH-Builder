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

        protected string SourceConnectionName = string.Empty;

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

        public virtual DataStruct[] GetDataStructs(string name)
        {
            List<DataStruct> output = new List<DataStruct>();

            if(name.EndsWith("*"))
            {
                name = name.Substring(0, name.Length - 1);

                foreach(var ds in SourceStructures)
                {
                    if(ds.Key.ToLower().StartsWith(name.ToLower()))
                    {
                        try
                        {
                            output.Add(GetDataStruct(ds.Key));
                        }
                        catch { }
                    }
                }
            }
            else
            {
                output.Add(GetDataStruct(name));
            }

            return output.ToArray();
        }

        public virtual DataStruct[] GetDataStructs()
        {
            List<DataStruct> output = new List<DataStruct>();

            foreach(var ds in SourceStructures)
            {
                output.Add(GetDataStruct(ds.Key));
            }

            return output.ToArray();
        }

        protected virtual DataStruct GetDataStruct(string name)
        {
            DataStruct output = new DataStruct();
            Dictionary<string, string> fields = SourceStructures[name];

            string structDef = "StructType([";
            string fieldsDef = string.Empty;

            foreach (KeyValuePair<string, string> field in fields)
            {
                fieldsDef += "\n\t" + field.Value + ",";
            }

            structDef += fieldsDef;

            structDef += "\n\tStructConfig(\"ConnectionName\", \"" + SourceConnectionName + "\"),";
            structDef += "\n\tStructConfig(\"SourceItemName\", \"" + name + "\")";

            structDef += "\n]);";

            DataStructParser parser = new DataStructParser(structDef, output);
            parser.Parse();

            return output;
        }
    }
}
