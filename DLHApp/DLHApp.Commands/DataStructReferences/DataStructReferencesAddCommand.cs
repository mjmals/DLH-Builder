using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructReferences;
using DLHApp.Model.DataStructs;

namespace DLHApp.Commands.DataStructReferences
{
    public class DataStructReferencesAddCommand : Command, IDataStructReferencesCommand, ICommand
    {
        public override string[] Prompt => new string[] { "add" };

        public override string Description => "Adds a new struct ref to specified path.  Can be imported from a data struct using the {linkds} parameter.";

        public override void Run(string[] args)
        {
            base.Run(args);

            if (Args.Length == 0)
            {
                WriteOutput("Please specify a Reference name/path");
                return;
            }

            List<string> linkArgs = new List<string>();

            if(Args.Contains("linkds"))
            {
                int linkArgsPos = Args.ToList().IndexOf("linkds");
                linkArgs.AddRange(Args.TakeLast(Args.Length - linkArgsPos - 1));
                Args = Args.Take(linkArgsPos).ToArray();
            }

            string refPathFull = string.Empty;
            string refName = string.Empty;
            string refPath = string.Empty;

            foreach(string refItem in Args)
            {
                refPathFull += refItem;
            }

            if(!Environment.CurrentDirectory.Contains("Data Applications") && !refPath.StartsWith("Data Applications"))
            {
                refPathFull = Path.Combine("Data Applications", refPathFull);
            }

            if (refPathFull.Split(@"\")[2].ToLower() != "stages")
            {
                List<string> refPathSplit = refPathFull.Split(@"\").ToList();
                refPathSplit.Insert(2, "Stages");
                refPathFull = string.Join(@"\", refPathSplit);
            }

            List<string> refPathItems = refPathFull.Split(@"\").ToList();
            refName = refPathItems.Last();
            refPath = string.Join(@"\", refPathItems.Take(refPathItems.Count() - 1));


            if(linkArgs.Count > 0)
            {
                string dsName = string.Join(" ", linkArgs);

                if(dsName.EndsWith("*"))
                {
                    CreatedLinkedRefs(dsName, refPath, refName);
                    return;
                }

                CreateLinkedRef(dsName, refName, refPath);
                return;
            }

            CreateEmptyRef(refName, refPath);
        }

        void CreateEmptyRef(string refName, string refPath)
        {
            DataStructReference dsr = DataStructReference.New();
            dsr.Name = refName;
            dsr.FolderPath = refPath;
            dsr.Save();
        }

        void CreateLinkedRef(string dsName, string refName, string refPath)
        {
            string dsFileName = Path.Combine(Environment.CurrentDirectory, "Data Structures", dsName + ".datastruct");
            
            if (!File.Exists(dsFileName))
            {
                WriteOutput(string.Format("Could not find Data Struct '{0}'", dsName));
                return;
            }

            DataStruct ds = DataStruct.Load(dsFileName);

            DataStructReference dsr = DataStructReference.New();
            dsr.Name = refName;
            dsr.FolderPath = refPath;
            dsr.SourceDataStruct = dsName;

            foreach(DataStructField field in ds.Fields)
            {
                dsr.Fields.Add(new DataStructFieldReference() { SourceField = field.Name, OutputName = field.Name });
            }

            dsr.Save();
        }

        void CreatedLinkedRefs(string dsPath, string refPath, string refName)
        {
            string dsRootPath = Path.Combine(Environment.CurrentDirectory, "Data Structures");

            foreach(string dsFile in Directory.GetFiles(Path.Combine(dsRootPath, dsPath.Replace("*", "")), "*.datastruct"))
            {
                string dsName = dsFile.Replace(dsRootPath + @"\", "").Replace(".datastruct", "");
                string dsrPath = Path.Combine(refPath, refName);
                string dsrName = Path.GetFileNameWithoutExtension(dsFile);

                CreateLinkedRef(dsName, dsrName, dsrPath);
            }
        }
    }
}
