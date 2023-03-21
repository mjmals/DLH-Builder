using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DLHApp.Model.Connections;

namespace DLHApp.Model.DataStructImporters
{
    public class SqlServerDataStructImporter : DataStructImporter
    {
        public SqlServerDataStructImporter(string sqlConnection)
        {
            Connection = SqlServerConnection.Load(sqlConnection);
            SourceConnectionName = Path.GetFileNameWithoutExtension(sqlConnection).Replace(".sqlcon", "");
        }

        SqlServerConnection Connection { get; set; }

        SqlConnectionStringBuilder ConnectionStringBuilder()
        {
            SqlConnectionStringBuilder output = new SqlConnectionStringBuilder() { DataSource = Connection.Server, InitialCatalog = Connection.Database, TrustServerCertificate = true };

            switch (Connection.Authentication)
            {
                case SqlServerAuthenticationType.Windows:
                    output.IntegratedSecurity = true;
                    break;
                case SqlServerAuthenticationType.Azure:
                    output.Authentication = SqlAuthenticationMethod.ActiveDirectoryPassword;
                    Console.Write("Enter Username: ");
                    string? user = Console.ReadLine();
                    output.UserID = user;
                    Console.Write("Enter password: ");
                    var password = new System.Security.SecureString();
                    while (true)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter)
                            break;
                        password.AppendChar(key.KeyChar);
                        Console.Write("*");
                    }
                    output.Password = new System.Net.NetworkCredential(string.Empty, password).Password;

                    break;
                default:
                    output.IntegratedSecurity = true;
                    break;
            }

            return output;
        }

        protected override Dictionary<string, Dictionary<string, string>> GetSourceStructures()
        {
            Dictionary<string, Dictionary<string, string>> output = new Dictionary<string, Dictionary<string, string>>();

            SqlConnectionStringBuilder connBuilder = ConnectionStringBuilder();

            using (SqlConnection conn = new SqlConnection(connBuilder.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(Properties.Resources.SqlServerStructImporter, conn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            string tableName = string.Empty;
                            Dictionary<string, string> fields = new Dictionary<string, string>();

                            while (rdr.Read())
                            {
                                string rdrTable = rdr.GetString(rdr.GetOrdinal("TableFullName"));

                                if(rdrTable != tableName)
                                {
                                    tableName = rdrTable;
                                    fields = new Dictionary<string, string>();

                                    output.Add(tableName, fields);
                                }

                                fields.Add(rdr.GetString(rdr.GetOrdinal("ColumnName")), rdr.GetString(rdr.GetOrdinal("StructField")));
                            }
                        }
                    }
                }
            }
            
            return output;
        }
    }
}
