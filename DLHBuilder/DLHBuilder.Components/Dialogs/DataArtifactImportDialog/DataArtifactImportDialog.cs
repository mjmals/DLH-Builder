using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

namespace DLHBuilder.Components.Dialogs
{
    public class DataArtifactImportDialog : Form
    {
        public DataArtifactImportDialog(DataConnection connection, DataArtifactImportDialogOptions options)
        {
            Connection = connection;
            Options = options;
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

        protected DataArtifactImportDialogOptions Options { get; set; }

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
                    artifact.DataSources.Add(TransformDataSource(source, row));

                    if(DataArtifactNameViolation.IsViolation(artifact.Name))
                    {
                        RenameDataArtifactTransformation renameTransformation = new RenameDataArtifactTransformation();
                        renameTransformation.ID = Guid.NewGuid();
                        renameTransformation.OriginalName = artifact.Name;
                        renameTransformation.ReferencedObjectID = artifact.ID;

                        artifact.Name = DataArtifactNameViolation.RemoveViolations(artifact.Name);
                        artifact.Transformations.Add(renameTransformation);
                    }
                }

                artifact.Schema.Add(TransformSchemaItem(row));
            }
        }

        DataSource TransformDataSource(DataTable source, DataRow row)
        {
            DataSource dataSource = (DataSource)Activator.CreateInstance(DataSourceType());
            dataSource.ID = Guid.NewGuid();

            if(dataSource is ConnectionDataSource)
            {
                ((ConnectionDataSource)dataSource).ConnectionID = Connection.ID;
            }

            foreach (DataColumn column in source.Columns)
            {
                if (column.ColumnName.Contains("DataSource."))
                {
                    string propertyName = column.ColumnName.Replace("DataSource.", "");

                    PropertyInfo property = dataSource.GetType().GetProperty(propertyName);

                    if(property.PropertyType.IsEnum)
                    {
                        property.SetValue(dataSource, Enum.Parse(property.PropertyType, row[column.ColumnName].ToString()));
                        continue;
                    }

                    property.SetValue(dataSource, row[column.ColumnName]);
                }
            }

            return dataSource;
        }

        DataArtifactSchemaItem TransformSchemaItem(DataRow row)
        {
            DataArtifactSchemaItem schemaItem = new DataArtifactSchemaItem();
            schemaItem.ID = Guid.NewGuid();
            schemaItem.Name = row["Schema.Name"].ToString();
            schemaItem.KeyType = (DataArtifactSchemaItemKeyType)Enum.Parse(typeof(DataArtifactSchemaItemKeyType), row["Schema.KeyType"].ToString());
            schemaItem.IsNullable = Convert.ToBoolean(row["Schema.IsNullable"].ToString());
            
            SourceDataType dataType = new SourceDataType(row["Schema.DataType"].ToString());
            schemaItem.DataType = dataType;
            dataType.Properties = new DataTypeConverterProperties();

            dataType.Properties.Add("IsCaseSensitive", Convert.ToBoolean(row["DataType.IsCaseSensitive"].ToString()));
            dataType.Properties.Add("IsAccentSensitive", Convert.ToBoolean(row["DataType.IsAccentSensitive"].ToString()));

            if(DataArtifactNameViolation.IsViolation(schemaItem.Name))
            {
                RenameDataArtifactTransformation renameTransformation = new RenameDataArtifactTransformation();
                renameTransformation.ID = Guid.NewGuid();
                renameTransformation.OriginalName = schemaItem.Name;
                renameTransformation.ReferencedObjectID = schemaItem.ID;

                schemaItem.Name = DataArtifactNameViolation.RemoveViolations(schemaItem.Name);
                schemaItem.Transformations.Add(renameTransformation);
            }

            return schemaItem;
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
                artifact.Schema.ForEach(delegate(DataArtifactSchemaItem schemaItem) { schemaItem.DataType = new SourceDataTypeConverter(Connection.GetType(), ((SourceDataType)schemaItem.DataType)).GetDataType(); });
            }

            DialogResult = DialogResult.OK;
        }
    }
}
