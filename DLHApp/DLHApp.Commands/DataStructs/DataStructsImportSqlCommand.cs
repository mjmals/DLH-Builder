using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructs;
using DLHApp.Model.DataStructImporters;
using System.Reflection.Metadata;

namespace DLHApp.Commands.DataStructs
{
    public class DataStructsImportSqlCommand : Command, IDataStructsImportCommand, ICommand
    {
        public override string[] Prompt => new string[] { "sql" };

        public override void Run(string[] args)
        {
            base.Run(args);

            if (Args.Length == 0)
            {
                WriteOutput(@"Please specify a connection path\connection item name");
                return;
            }

            string[] connPath = string.Join(" ", Args[0]).Split(@"\");
            string connItem = connPath.LastOrDefault();
            string connFile = string.Join(@"\", connPath.Take(connPath.Length - 1));
            string connFilePath = Path.Combine(Environment.CurrentDirectory, "Connections", connFile + ".sqlcon.json");

            if (!File.Exists(connFilePath))
            {
                WriteOutput(string.Format("Could not find connection {0}", connFile));
                return;
            }

            string savePath = "Imports";

            if (Args.Length == 2)
            { 
                savePath = Args[1];
            }

            SqlServerDataStructImporter importer = new SqlServerDataStructImporter(connFilePath);
            DataStruct[] structs = importer.GetDataStructs(connItem);

            foreach (DataStruct ds in structs)
            {
                try
                {
                    ds.Name = ds.SourceItemName.Split(".").Last();
                    ds.FolderPath = Path.Combine(Environment.CurrentDirectory, "Data Structures", savePath);
                    ds.Save();
                }
                catch { }
            }
        }
    }
}
