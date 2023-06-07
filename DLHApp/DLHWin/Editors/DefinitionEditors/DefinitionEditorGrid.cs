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
        public DefinitionEditorGrid(string fileName)
        {
            FileName = fileName;
            Dock = DockStyle.Fill;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            AllowUserToAddRows = false;
            Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Identifier", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Definition", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DefaultCellStyle = new DataGridViewCellStyle() { WrapMode = DataGridViewTriState.True } });
            CellEnter += CellSelected;
        }

        string FileName { get; set; }

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

            foreach(CodeParserOutputLine parseLine in parseOutput)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = parseLine.Identifier });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = parseLine.Expression, Tag = parseLine });

                Rows.Add(row);
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
