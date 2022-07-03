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
        public SQLDataArtifactImportDialog(SQLDataConnection connection) : base(connection)
        {
            Text = "Import from SQL";
        }

        protected override Type DataSourceType() => typeof(SQLConnectionDataSource);

        protected override DataTable GetSourceObjects()
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
    }
}
