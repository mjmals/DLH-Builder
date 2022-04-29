using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactImportDialog : Form
    {
        public DataArtifactImportDialog(DataConnection connection)
        {
            Connection = connection;
            TransformSourceObjects();
            
            ObjectPanel.Controls.Add(ObjectTree = new DataArtifactImportObjectTree(connection, SourceArtifacts));
            Controls.Add(ObjectPanel);

            Controls.Add(SelectorPanel);
            Controls.Add(ControlPanel);

            WindowState = FormWindowState.Maximized;
        }
        
        protected DataConnection Connection { get; set; }

        public DataArtifact Artifact { get; set; }

        DataArtifactImportObjectTree ObjectTree { get; set; }

        Panel ObjectPanel = new Panel() { Dock = DockStyle.Left, Width = 400 };

        Panel SelectorPanel = new Panel() { Dock = DockStyle.Fill };

        Panel ControlPanel = new Panel() { Dock = DockStyle.Bottom, Height = 50 };

        protected DataArtifactCollection SourceArtifacts = new DataArtifactCollection();

        protected virtual Type DataSourceType() => null;

        protected virtual DataTable GetSourceObjects()
        {
            return new DataTable();
        }

        protected virtual void TransformSourceObjects()
        {
            DataTable source = GetSourceObjects();
            DataArtifact artifact = new DataArtifact() { Name = string.Empty };
            DataSource dataSource;

            foreach(DataRow row in source.Rows)
            {
                if(row["Name"].ToString() != artifact.Name)
                {
                    artifact = new DataArtifact();
                    artifact.ID = Guid.NewGuid();
                    artifact.Name = row["Name"].ToString();
                    artifact.ArtifactNamespace = row["ArtifactNamespace"].ToString().Split(',').ToList();
                    SourceArtifacts.Add(artifact);
                    
                    artifact.DataSources = new DataSourceCollection();
                    dataSource = (DataSource)Activator.CreateInstance(DataSourceType());
                    artifact.DataSources.Add(dataSource);

                    foreach(DataColumn column in source.Columns)
                    {
                        if(column.ColumnName.Contains("DataSource."))
                        {
                            string propertyName = column.ColumnName.Replace("DataSource.", "");
                            dataSource.GetType().GetProperty(propertyName).SetValue(dataSource, row[column.ColumnName]);
                        }
                    }
                }

                DataArtifactSchemaItem schemaItem = new DataArtifactSchemaItem();
                schemaItem.ID = Guid.NewGuid();
                schemaItem.Name = row["Schema.Name"].ToString();
                artifact.Schema.Add(schemaItem);
            }
        }
    }
}
