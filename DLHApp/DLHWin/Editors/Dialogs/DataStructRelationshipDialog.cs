using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructs;
using DLHWin.Styles;

namespace DLHWin.Editors.Dialogs
{
    internal class DataStructRelationshipDialog : Form
    {
        public DataStructRelationshipDialog(DataStruct dataStruct)
        {
            DataStruct = dataStruct;
            Relationships = DataStruct.Relationships == null ? new DataStructRelationshipCollection() : DataStruct.Relationships;
            Text = string.Format("Edit Relationships for {0}", DataStruct.Name);

            Width = 800;
            Height = 500;

            Controls.Add(SplitPanel);
            SplitPanel.Panel1.Controls.Add(RelationshipPanel());
            SplitPanel.Panel2.Controls.Add(JoinPanel());
            
            Controls.Add(ToolBar);
            ToolBar.Items.Add(SaveBtn);
            SaveBtn.Click += Save;
        }

        DataStruct DataStruct { get; set; }

        DataStructRelationshipCollection Relationships { get; set; }

        SplitContainer SplitPanel = new SplitContainer() { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal };

        ToolStrip ToolBar = new ToolStrip() { ImageList = Images.List };

        ToolStripButton SaveBtn = new ToolStripButton() { Text = "Save Changes", ImageKey = "Save" };

        Panel RelationshipPanel()
        {
            Panel output = new Panel() { Dock = DockStyle.Fill };

            ToolStrip toolBar = new ToolStrip() { ImageList = Images.List };
            
            ToolStripButton addBtn = new ToolStripButton() { Text = "Add Relationship", ImageKey = "Add" };
            addBtn.Click += AddRelationship;
            toolBar.Items.Add(addBtn);

            ToolStripButton removeBtn = new ToolStripButton() { Text = "Delete Relationship", ImageKey = "Delete" };
            removeBtn.Click += RemoveRelationship;
            toolBar.Items.Add(removeBtn);

            DataGridView grid = new DataGridView() { Dock = DockStyle.Fill };
            grid.Name = "relationshipGrid";
            grid.AllowUserToAddRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grid.SelectionChanged += LoadJoins;
            grid.CellEndEdit += RelationshipGridCellUpdated;
            grid.Columns.Add("Source Data Struct", "Source Data Struct");
            grid.Columns.Add("Output Field", "Output Field");
            LoadRelationships(grid);

            output.Controls.Add(grid);
            output.Controls.Add(toolBar);

            return output;
        }

        void LoadRelationships(DataGridView grid)
        {
            grid.Rows.Clear();

            foreach(DataStructRelationship rel in Relationships)
            {
                grid.Rows.Add(rel.SourceDataStruct, rel.OutputField);
                grid.Rows[grid.Rows.Count - 1].Tag = rel;
            }
        }

        DataGridView GetRelationshipGrid()
        {
            return (DataGridView)Controls.Find("relationshipGrid", true).First();
        }

        void AddRelationship(object sender, EventArgs e)
        {
            DataStructRelationship rel = new DataStructRelationship();
            Relationships.Add(rel);
            
            DataGridView grid = GetRelationshipGrid();
            grid.Rows.Add();
            grid.Rows[grid.Rows.Count - 1].Tag = rel;
        }

        void RemoveRelationship(object sender, EventArgs e)
        {
            DataGridView grid = GetRelationshipGrid();
            DataGridViewRow row = grid.SelectedCells[0].OwningRow;
            DataStructRelationship rel = (DataStructRelationship)row.Tag;

            Relationships.Remove(rel);
            grid.Rows.Remove(row);
        }

        void RelationshipGridCellUpdated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grid = GetRelationshipGrid();
            DataStructRelationship rel = (DataStructRelationship)grid.Rows[e.RowIndex].Tag;

            switch (e.ColumnIndex)
            {
                case 0:
                    rel.SourceDataStruct = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    break;
                case 1:
                    rel.OutputField = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    break;
            }
        }

        Panel JoinPanel()
        {
            Panel output = new Panel() { Dock = DockStyle.Fill };

            ToolStrip toolBar = new ToolStrip() { ImageList = Images.List };

            ToolStripButton addBtn = new ToolStripButton() { Text = "Add Join", ImageKey = "Add" };
            addBtn.Click += AddJoin;
            toolBar.Items.Add(addBtn);

            ToolStripButton removeBtn = new ToolStripButton() { Text = "Delete Join", ImageKey = "Delete" };
            removeBtn.Click += RemoveJoin;
            toolBar.Items.Add(removeBtn);

            DataGridView grid = new DataGridView() { Dock = DockStyle.Fill };
            grid.Name = "joinGrid";
            grid.AllowUserToAddRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grid.CellEndEdit += JoinGridCellUpdated;
            grid.Columns.Add("Source Field", "Source Field");
            grid.Columns.Add("Target Field", "Target Field");
            grid.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Is Case Sensitive", HeaderText = "Is Case Sensitive?" });

            output.Controls.Add(grid);
            output.Controls.Add(toolBar);

            return output;
        }

        void LoadJoins(object sender, EventArgs e)
        {
            DataGridView joinGrid = GetJoinGrid();
            DataGridView relationshipGrid = GetRelationshipGrid();

            DataStructRelationship rel = (DataStructRelationship)relationshipGrid.Rows[relationshipGrid.SelectedCells[0].RowIndex].Tag;

            joinGrid.Tag = rel;
            joinGrid.Rows.Clear();

            if(rel.Joins != null)
            {
                foreach(DataStructRelationshipJoin join in rel.Joins)
                {
                    joinGrid.Rows.Add(join.SourceField, join.TargetField, join.IsCaseSensitive);
                    joinGrid.Rows[joinGrid.Rows.Count - 1].Tag = join;
                }
            }
        }

        DataGridView GetJoinGrid()
        {
            return (DataGridView)Controls.Find("joinGrid", true).First();
        }

        void AddJoin(object sender, EventArgs e)
        {
            DataStructRelationshipJoin join = new DataStructRelationshipJoin();

            DataGridView grid = GetJoinGrid();
            DataStructRelationship rel = (DataStructRelationship)grid.Tag;

            rel.Joins.Add(join);
            grid.Rows.Add(new DataGridViewRow() { Tag = join });
        }

        void RemoveJoin(object sender, EventArgs e)
        {
            DataGridView grid = GetJoinGrid();
            DataStructRelationship rel = (DataStructRelationship)grid.Tag;
            DataGridViewRow row = grid.SelectedCells[0].OwningRow;
            DataStructRelationshipJoin join = (DataStructRelationshipJoin)row.Tag;

            rel.Joins.Remove(join);
            grid.Rows.Remove(row);
        }

        void JoinGridCellUpdated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grid = GetJoinGrid();
            DataStructRelationshipJoin join = (DataStructRelationshipJoin)grid.Rows[e.RowIndex].Tag;

            switch (e.ColumnIndex)
            {
                case 0:
                    join.SourceField = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    break;
                case 1:
                    join.TargetField = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    break;
                case 2:
                    join.IsCaseSensitive = (bool)grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    break;
            }
        }

        void Save(object sender, EventArgs e)
        {
            DataStruct.Relationships = Relationships;
            DataStruct.Save();

            DialogResult = DialogResult.OK;
            Close();
            Dispose();
        }
    }
}
