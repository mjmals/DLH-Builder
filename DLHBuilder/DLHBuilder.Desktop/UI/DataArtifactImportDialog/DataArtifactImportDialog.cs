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
            
            SelectorPanel.Controls.Add(SchemaTree = new DataArtifactImportSchemaItemTree(null, null));
            Controls.Add(SelectorPanel);

            ControlPanel.ImportButton.Click += ImportButtonClicked;
            Controls.Add(ControlPanel);

            ObjectPanel.Controls.Add(ObjectTree = new DataArtifactImportObjectTree(connection, SourceArtifacts));
            ObjectTree.AfterSelect += ObjectTreeSelectionChanged;
            ObjectTree.AfterCheck += ObjectTreeChecked;
            Controls.Add(ObjectPanel);

            WindowState = FormWindowState.Maximized;
        }
        
        protected DataConnection Connection { get; set; }

        public DataArtifact Artifact { get; set; }

        public DataArtifactImportSelectionCollection SelectedArtifacts = new DataArtifactImportSelectionCollection();

        DataArtifactImportObjectTree ObjectTree { get; set; }

        DataArtifactImportSchemaItemTree SchemaTree { get; set; }

        Panel ObjectPanel = new Panel() { Dock = DockStyle.Left, Width = 400 };

        Panel SelectorPanel = new Panel() { Dock = DockStyle.Fill };

        DataArtifactImportControls ControlPanel = new DataArtifactImportControls();

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
                schemaItem.DataType = new SourceDataType(row["Schema.DataType"].ToString());
                artifact.Schema.Add(schemaItem);
            }
        }

        protected virtual void ObjectTreeSelectionChanged(object sender, TreeViewEventArgs e)
        {
            DataArtifact artifact = (DataArtifact)e.Node.Tag;
            SetSchemaTree(artifact);
        }

        protected virtual void ObjectTreeChecked(object sender, TreeViewEventArgs e)
        {
            DataArtifact artifact = (DataArtifact)e.Node.Tag;

            switch(e.Node.Checked)
            {
                case true:
                    if(!SelectedArtifacts.ContainsKey(artifact))
                    {
                        DataArtifactSchemaItemCollection items = new DataArtifactSchemaItemCollection();
                        items.AddRange(artifact.Schema);
                        SelectedArtifacts.Add(artifact, items);
                    }
                    break;
                case false:
                    SelectedArtifacts.Remove(artifact);
                    break;
            }

            SetSchemaTree(artifact);
        }

        void SetSchemaTree(DataArtifact artifact)
        {
            SelectorPanel.Controls.Clear();
            SchemaTree = new DataArtifactImportSchemaItemTree(artifact, SelectedArtifacts);
            SelectorPanel.Controls.Add(SchemaTree);
        }

        void ImportButtonClicked(object sender, EventArgs e)
        {
            foreach(DataArtifact artifact in SelectedArtifacts.Keys)
            {
                artifact.Schema.Clear();
                artifact.Schema.AddRange(SelectedArtifacts[artifact]);
                artifact.Schema.ForEach(delegate(DataArtifactSchemaItem schemaItem) { schemaItem.DataType = new DataTypeConverter(Connection.GetType(), ((SourceDataType)schemaItem.DataType).DataTypeName).GetDataType(); });
            }

            DialogResult = DialogResult.OK;
        }
    }
}
