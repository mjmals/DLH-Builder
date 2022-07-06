using System;
using System.Text.RegularExpressions;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using DLHBuilder.Generator;

namespace DLHBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            //DataTypeCollection datatypes = new DataTypeCollection();
            //datatypes.ForEach(x => Console.WriteLine(x.Name));
            Console.Write("Enter project path:");
            string path = Console.ReadLine();
            Project project = Project.Load(path);
            DataArtifact dataArtifact = project.Artifacts.First(x=> x.Name== "TableTestSimple");
            //DataArtifactSchemaItem[] primaryKeys = dataArtifact.ListPrimaryKeys();
            //Console.WriteLine(string.Join(',', primaryKeys.Select(e => e.Name)));
            SQLConnectionDataSource sqlDataSource = (SQLConnectionDataSource)dataArtifact.DataSources[0];
            //ADLSCompiledDataArtifact baseArtifact = (ADLSCompiledDataArtifact)project.Artifacts.First(x => x.Name == "TableTestSimple");

            Console.WriteLine(sqlDataSource.Schema);
            Console.ReadKey();

        }

        

        List<Type> DataTypes()
        {
            Assembly syslib = typeof(string).Assembly;

            Type[] types = syslib.GetTypes()
                .Where(x => x.Namespace == "System")
                .Where(x => x.IsPrimitive || (new string[] { "String", "Object" }).Contains(x.Name))
                .ToArray();

            return types.ToList();
        }
    }
}
