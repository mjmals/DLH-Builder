using System;
using System.Text.RegularExpressions;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DLHBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Server: ");
            string server = Console.ReadLine();

            Console.Write("Database: ");
            string database = Console.ReadLine();

            Console.Write("Schema: ");
            string schema = Console.ReadLine();

            Console.Write("Table: ");
            string table = Console.ReadLine();

            SqlConnectionStringBuilder connstr = new SqlConnectionStringBuilder()
            {
                DataSource = server,
                InitialCatalog = database,
                IntegratedSecurity = true
            };

            using (SqlConnection conn = new SqlConnection(connstr.ConnectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connected...");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    DataArtifact artifact = new DataArtifact();
                    artifact.Name = table;
                    artifact.LoadDefinitions = new LoadDefinitionCollection();

                    SQLServerLoadDefinitionSource source = new SQLServerLoadDefinitionSource();
                    source.Server = server;
                    source.Database = database;
                    source.Schema = schema;
                    source.Table = table;

                    LoadDefinition definition = new LoadDefinition();
                    definition.Source = source;

                    artifact.LoadDefinitions.Add(definition);

                    DataArtifactCollection artifacts = new DataArtifactCollection();
                    artifacts.Name = "MCS";
                    artifacts.Add(artifact);

                    Console.Write("Object Created");
                }
                
            }

            Console.ReadKey();
        }
    }
}
