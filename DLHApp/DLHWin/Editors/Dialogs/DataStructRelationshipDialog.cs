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
            toolBar.Click += AddRelationship;
            toolBar.Items.Add(addBtn);

            ToolStripButton removeBtn = new ToolStripButton() { Text = "Delete Relationship", ImageKey = "Delete" };
            toolBar.Click += RemoveRelationship;
            toolBar.Items.Add(removeBtn);

            DataGridView grid = new DataGridView() { Dock = DockStyle.Fill };
            grid.Name = "relationshipGrid";
            grid.AllowUserToAddRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grid.SelectionChanged += LoadJoins;
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

        void AddRelationship(object sender, EventArgs e)
        {

        }

        void RemoveRelationship(object sender, EventArgs e)
        {

        }

        Panel JoinPanel()
        {
            Panel output = new Panel() { Dock = DockStyle.Fill };

            ToolStrip toolBar = new ToolStrip() { ImageList = Images.List };

            ToolStripButton addBtn = new ToolStripButton() { Text = "Add Join", ImageKey = "Add" };
            toolBar.Click += AddJoin;
            toolBar.Items.Add(addBtn);

            ToolStripButton removeBtn = new ToolStripButton() { Text = "Delete Join", ImageKey = "Delete" };
            toolBar.Click += RemoveJoin;
            toolBar.Items.Add(removeBtn);

            DataGridView grid = new DataGridView() { Dock = DockStyle.Fill };
            grid.Name = "joinGrid";
            grid.AllowUserToAddRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grid.Columns.Add("Source Field", "Source Field");
            grid.Columns.Add("Target Field", "Target Field");
            grid.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Is Case Sensitive", HeaderText = "Is Case Sensitive?" });

            output.Controls.Add(grid);
            output.Controls.Add(toolBar);

            return output;
        }

        void LoadJoins(object sender, EventArgs e)
        {
            DataGridView joinGrid = (DataGridView)Controls.Find("joinGrid", true).First();
            DataGridView relationshipGrid = (DataGridView)Controls.Find("relationshipGrid", true).First();

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

        void AddJoin(object sender, EventArgs e)
        {

        }

        void RemoveJoin(object sender, EventArgs e)
        {

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
