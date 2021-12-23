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

            DataArtifactCollection group = project.CreateDataArtfactCollection("DatabaseA");

            DataArtifact artifact = group.CreateDataArtifact("tblSales");

            Console.Write("Specify Project save path: ");
            string path = Console.ReadLine();

            project.Save(path);
        }    
    }
}
