using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructs;
using DLHApp.Model.DataTypes;
using DLHApp.Model.DataTypes.Converters;
using System.Data;

namespace DLHWin.Grids.DataStructs
{
    internal class DataStructEditorGrid : EditorGrid
    {
        public DataStructEditorGrid(DataStruct dataStruct)
        {
            DataStruct = dataStruct;
            RowValues = dataStruct.Fields;

            AddMetadata();
        }

        public DataStruct DataStruct { get => (DataStruct)Tag; set => Tag = value; }

        public override void Reload()
        {
            base.Refresh();
            RowValues = DataStruct.Fields;
            AddMetadata();
        }

        protected override EditorGridColumnCollection GridColumns => new EditorGridColumnCollection()
        {
            new EditorGridColumn("Name", "Name", typeof(EditorGridTextCell)),
            new EditorGridColumn("Data Type", "DataType", typeof(DataStructEditorGridDataTypeCell)),
            new EditorGridColumn("Is Nullable?", "IsNullable", typeof(EditorGridCheckCell)),
            new EditorGridColumn("Key Types", "KeyTypes", typeof(DataStructEditorGridKeyTypeCell)),
            new EditorGridColumn("Is Case Sensitive?", "IsCaseSensitive", typeof(EditorGridCheckCell))
        };

        void AddMetadata()
        {
            foreach (DataStructField field in DataStruct.Fields)
            {
                if (field.Metadata != null)
                {
                    foreach (var metadata in field.Metadata)
                    {
                        AddMetadataColumn(metadata.Key);
                        int columnIndex = Columns[metadata.Key].Index;
                        Rows[DataStruct.Fields.IndexOf(field)].Cells[columnIndex].Value = metadata.Value;
                        DataStructEditorGridFieldMetadataCell cell = new DataStructEditorGridFieldMetadataCell();
                        cell.BaseProperty = metadata.Key;
                        cell.SetValue(metadata.Value);
                        Rows[DataStruct.Fields.IndexOf(field)].Cells[columnIndex].Tag = cell;
                    }
                }
            }
        }


        public void AddMetadataColumn(string name)
        {
            if (Columns.Contains(name))
            {
                return;
            }

            Columns.Add(name, name);
            Columns[Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            foreach (DataGridViewRow row in Rows)
            {
                DataGridViewColumn column = Columns[Columns.Count - 1];
                DataGridViewCell gridCell = Rows[row.Index].Cells[column.Index];

                if (gridCell.Tag == null)
                {
                    gridCell.Tag = new DataStructEditorGridFieldMetadataCell() { BaseProperty = column.Name };
                }
            }
        }

        protected override void PasteGridFull()
        {
            DataTable pasteValues = ConvertPasteValuesTable();

            DataStructFieldCollection fields = new DataStructFieldCollection();

            foreach(DataRow pasteValue in pasteValues.Rows)
            {
                DataStructField field = new DataStructField() { KeyTypes = new DataStructFieldKeyTypeCollection(), Metadata = new DataStructFieldMetadataCollection() };

                try
                {
                    if (!string.IsNullOrEmpty(GetPasteValue(pasteValue, "Name")))
                    {
                        field.Name = GetPasteValue(pasteValue, "Name");
                    }

                    if (!string.IsNullOrEmpty(GetPasteValue(pasteValue, "Data Type")))
                    {
                        field.DataType = new DataTypeParser(GetPasteValue(pasteValue, "Data Type")).Parse();
                    }

                    if (!string.IsNullOrEmpty(GetPasteValue(pasteValue, "Is Nullable?")))
                    {
                        field.IsNullable = Convert.ToBoolean(GetPasteValue(pasteValue, "Is Nullable?"));
                    }

                    if (!string.IsNullOrEmpty(GetPasteValue(pasteValue, "Key Types")))
                    {
                        string[] keyTypesText = GetPasteValue(pasteValue, "Key Types").Split(",");

                        foreach(string keyTypeText in keyTypesText)
                        {
                            field.KeyTypes.Add(Enum.Parse<DataStructFieldKeyType>(keyTypeText.Trim()));
                        }
                    }

                    if (!string.IsNullOrEmpty(GetPasteValue(pasteValue, "Is Case Sensitive?")))
                    {
                        field.IsCaseSensitive = Convert.ToBoolean(GetPasteValue(pasteValue, "Is Case Sensitive?"));
                    }

                    for (int i = 5; i < pasteValues.Columns.Count; i++)
                    {
                        string metadataItem = pasteValues.Columns[i].ColumnName;

                        if (!string.IsNullOrEmpty(GetPasteValue(pasteValue, metadataItem)))
                        {
                            field.Metadata.Add(metadataItem, GetPasteValue(pasteValue, metadataItem));
                        }
                    }

                    fields.Add(field);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message, "Paste Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            DataStruct.Fields = fields;
            DataStruct.Save();
            Reload();
            GridPasted?.Invoke(null, null);
        }
    }
}
