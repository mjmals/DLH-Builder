using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DLHApp.Model.Connections;

namespace DLHApp.Commands.Connections
{
    public class ConnectionsAddSqlCommand : Command, ICommand, IConnectionsAddCommand
    {
        public override string[] Prompt => new string[] { "sql" };

        public override string Description => "Creates a new sql connection file";

        public override void Run(string[] args)
        {
            base.Run(args);

            string server = GetInput("Server Name");
            string database = GetInput("Database Name");
            SqlServerAuthenticationType auth = GetAuth();

            SqlServerConnection conn = SqlServerConnection.New();
            conn.Server = server;
            conn.Database = database;
            conn.Authentication = auth;
            conn.Save();
        }

        string GetAuthValue()
        {
            return GetInput(string.Format("Authentication Type ({0})", GetEnumValuesString(typeof(SqlServerAuthenticationType))));
        }

        SqlServerAuthenticationType GetAuth()
        {
            Type enumType = typeof(SqlServerAuthenticationType);

            string authValue = GetAuthValue();

            while(TestEnumValue(enumType, authValue) == false)
            {
                WriteOutput("Error: Incorrect Authentication Type selected");
                authValue = GetAuthValue();
            }

            return (SqlServerAuthenticationType)ParseEnumValue(typeof(SqlServerAuthenticationType), authValue);
        }
    }
}
