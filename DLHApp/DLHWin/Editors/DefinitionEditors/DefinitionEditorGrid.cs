using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.CodeParsers;

namespace DLHWin.Editors.DefinitionEditors
{
    internal abstract class DefinitionEditorGrid : DataGridView
    {
        public DefinitionEditorGrid(string fileName, string[] identifiers = null, string identifierLabel = null)
        {
            FileName = fileName;
            Identifiers = identifiers;
            IdentifierLabel = identifierLabel;

            Dock = DockStyle.Fill;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            AllowUserToAddRows = false;
            Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = (string.IsNullOrEmpty(IdentifierLabel) ? "Identifier" : IdentifierLabel), AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Definition", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DefaultCellStyle = new DataGridViewCellStyle() { WrapMode = DataGridViewTriState.True } });
            CellEnter += CellSelected;
        }

        string FileName { get; set; }

        string[] Identifiers { get; set; }

        string IdentifierLabel { get; set; }

        protected CodeParser Parser
        { 
            get => _parser;
            set
            {
                _parser = value;
                PopulateGrid();
            }
        }

        CodeParser _parser { get; set; }

        protected virtual void PopulateGrid()
        {
            Rows.Clear();
            CodeParserOutput parseOutput = Parser.Parse();

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
            else
            {
                foreach (CodeParserOutputLine parseLine in parseOutput)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = parseLine.Identifier });
                    row.Cells.Add(new DataGridViewTextBoxCell());
                    Rows.Add(row);
                }
            }

            foreach(CodeParserOutputLine parseLine in parseOutput)
            {
                DataGridViewRow identifierRow = null;

                foreach(DataGridViewRow row in Rows)
                {
                    if (row.Cells[0].Value.ToString().ToLower() == parseLine.Identifier.ToLower())
                    {
                        identifierRow = row;
                        break;
                    }
                }

                if (identifierRow != null)
                {
                    DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)identifierRow.Cells[1];
                    cell.Value = parseLine.Expression;
                    cell.Tag = parseLine;
                }
            }
        }

        void CellSelected(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.Tag is CodeParserOutputLine)
            {
                DefinitionCellSelected?.Invoke(cell, new DefinitionCellSelectedEventArgs((CodeParserOutputLine)cell.Tag, e.RowIndex, e.ColumnIndex));
            }
        }

        public EventHandler<DefinitionCellSelectedEventArgs> DefinitionCellSelected { get; set; }

        public class DefinitionCellSelectedEventArgs
        {
            public DefinitionCellSelectedEventArgs(CodeParserOutputLine parserLine, int rowIndex, int columnIndex)
            {
                ParserLine = parserLine;
                RowIndex = rowIndex;
                ColumnIndex = columnIndex;
            }

            public CodeParserOutputLine ParserLine { get; set; }

            public int RowIndex { get; set; }

            public int ColumnIndex { get; set; }
        }
    }
}
