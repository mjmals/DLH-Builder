using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructs;
using DLHApp.Model.DataStructImporters;

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

            string[] connPath = string.Join(" ", Args).Split(@"\");
            string connItem = connPath.LastOrDefault();
            string connFile = string.Join(@"\", connPath.Take(connPath.Length - 1));
            string connFilePath = Path.Combine(Environment.CurrentDirectory, "Connections", connFile + ".sqlcon.json");

            if (!File.Exists(connFilePath))
            {
                WriteOutput(string.Format("Could not find connection {0}", connFile));
                return;
            }

            SqlServerDataStructImporter importer = new SqlServerDataStructImporter(connFilePath);
            DataStruct ds = importer.GetDataStruct(connItem);
            ds.Name = connItem.Split(".").Last();
            ds.FolderPath = Path.Combine(Environment.CurrentDirectory, "Data Structures", "Imports");
            ds.Save();
        }
    }
}
