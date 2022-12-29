using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace DLHBuilder.Components.Dialogs
{
    public class SQLDataArtifactImportDialog : DataArtifactImportDialog
    {
        public SQLDataArtifactImportDialog(SQLDataConnection connection, DataArtifactImportDialogOptions options) : base(connection, options)
        {
            Text = "Import from SQL";
        }

        protected override Type DataSourceType() => typeof(SQLConnectionDataSource);

        protected override DataTable GetSourceObjects()
        {
            DataTable output = GetSourceTables();

            if (((bool)Options.Items["IncludeStoredProcedures"]) == true)
            {
                output.Merge(GetSourceProcedures());
            }

            return output;
        }

        DataTable GetSourceTables()
        {
            DataTable output = new DataTable();

            SQLDataConnection sqlconn = (SQLDataConnection)Connection;
            string connstring = new SqlConnectionStringBuilder() { DataSource = sqlconn.Server, InitialCatalog = sqlconn.Database, IntegratedSecurity = true }.ConnectionString;

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(Properties.Resources.SQLArtifactImport, conn))
                {
                    da.Fill(output);
                }
            }

            return output;
        }


        string GetStoredProceduresQuery()
        {
            string output = string.Empty;

            SQLDataConnection sqlconn = (SQLDataConnection)Connection;
            string connstring = new SqlConnectionStringBuilder() { DataSource = sqlconn.Server, InitialCatalog = sqlconn.Database, IntegratedSecurity = true }.ConnectionString;

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT ROUTINE_NAME AS ProcedureName FROM INFORMATION_SCHEMA.ROUTINES ORDER BY ProcedureName", conn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                string procedureName = rdr.GetString(rdr.GetOrdinal("ProcedureName"));
                                string query = string.Format(Properties.Resources.SQLArtifactImportStoredProcedure, procedureName);

                                if(string.IsNullOrEmpty(output))
                                {
                                    output = query;
                                }
                                else
                                {
                                    output += System.Environment.NewLine + "UNION ALL" + System.Environment.NewLine + query;
                                }
                            }
                        }
                    }
                }
            }

            return output;
        }

        DataTable GetSourceProcedures()
        {
            DataTable output = new DataTable();

            SQLDataConnection sqlconn = (SQLDataConnection)Connection;
            string connstring = new SqlConnectionStringBuilder() { DataSource = sqlconn.Server, InitialCatalog = sqlconn.Database, IntegratedSecurity = true }.ConnectionString;

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string query = GetStoredProceduresQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.Fill(output);
                }
            }

            return output;
        }
    }
}
