using System;
using System.Text.RegularExpressions;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;
using Microsoft.Win32;

namespace DLHBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Project project = new Project();
            project.Name = "Delta Lake Example";

            string server = "SQLDB-01";
            string database = "SalesDB";
            string table = "tblSales";

            DataArtifactCollection salesdb = project.CreateDataArtfactCollection(database);

            DataArtifact sales = salesdb.CreateDataArtifact(table);

            LoadDefinition salesdef = sales.CreateLoadDefinition();

            SQLServerLoadDefinitionSource salesdefsource = new SQLServerLoadDefinitionSource();
            salesdefsource.Server = server;
            salesdefsource.Database = database;
            salesdefsource.Schema = "dbo";
            salesdefsource.Table = table;
            salesdef.Source = salesdefsource;

            


            Console.Write("Specify Project save path: ");
            string path = Console.ReadLine();

            project.Save(path);
        }    
    }
}
