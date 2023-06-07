using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.CodeParsers;

namespace DLHWin.Editors.DefinitionEditors
{
    internal class DefinitionGroupEditorGrid : DataGridView
    {
        public DefinitionGroupEditorGrid(string definitionDirectory, string[] identifiers, string identifierLabel)
        {
            DefinitionDirectory = definitionDirectory;
            Identifiers = identifiers;
            IdentifierLabel = identifierLabel;

            Dock = DockStyle.Fill;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            AllowUserToAddRows = false;

            Populate();
        }

        public string DefinitionDirectory { get; set; }

        public string[] Identifiers { get; set; }

        public string IdentifierLabel { get; set; }

        string[] DefinitionFiles
        {
            get
            {
                if(_definitionFiles == null)
                {
                    _definitionFiles = Directory.GetFiles(DefinitionDirectory, "*.def.*").ToArray();
                }

                return _definitionFiles;
            }
        }

        string[] _definitionFiles { get; set; }

        void Populate()
        {
            BuildColumns();
            LoadIdentifiers();
            LoadDefinitions();
        }

        void BuildColumns()
        {
            Rows.Clear();
            Columns.Clear();

            Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = (string.IsNullOrEmpty(IdentifierLabel) ? "Identifier" : IdentifierLabel), AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });

            foreach(string defFile in DefinitionFiles)
            {
                Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(defFile)), AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DefaultCellStyle = new DataGridViewCellStyle() { WrapMode = DataGridViewTriState.True } });
            }
        }


        void LoadIdentifiers()
        {
            if(Identifiers != null)
            {
                foreach(string identifier in Identifiers)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = identifier });
                    row.Cells.Add(new DataGridViewTextBoxCell());
                    Rows.Add(row);
                }
            }
        }

        void LoadDefinitions()
        {
            for(int i = 0; i < DefinitionFiles.Length; i++)
            {
                LoadDefinition(i);
            }
        }

        void LoadDefinition(int definitionIndex)
        {
            string defFile = DefinitionFiles[definitionIndex];

            CodeParser parser = CodeParser.GetCodeParser(defFile);
            CodeParserOutput parserOutput = parser.Parse();

            foreach (CodeParserOutputLine parserLine in parserOutput)
            {
                DataGridViewRow identifierRow = null;

                foreach(DataGridViewRow row in Rows)
                {
                    if (row.Cells[0].Value.ToString().ToLower() == parserLine.Identifier.ToLower())
                    {
                        identifierRow = row;
                        break;
                    }
                }

                if (identifierRow != null)
                {
                    DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)identifierRow.Cells[definitionIndex + 1];
                    cell.Value = parserLine.Expression;
                    cell.Tag = parserLine;
                }
            }
        }
    }
}
