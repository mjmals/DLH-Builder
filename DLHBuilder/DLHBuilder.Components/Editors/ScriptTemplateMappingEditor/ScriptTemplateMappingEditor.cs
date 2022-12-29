using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using DLHBuilder.Components.Model;

namespace DLHBuilder.Components.Editors
{
    public class ScriptTemplateMappingEditor : Editor
    {
        public ScriptTemplateMappingEditor(ScriptTemplateCollection templates, ScriptTemplateReferenceCollection templatererences, string title = null)
        {
            Templates = new CollatedScriptTemplateCollection(templates);
            TemplateReferences = templatererences;

            string baseTitle = "Script Template Mapping";
            Text = string.IsNullOrEmpty(title) ? baseTitle : string.Format("{0}: {1}", title, baseTitle);
            Controls.Add(MappingTable = NewMappingTable());
            Controls.Add(Toolbar());
        }

        public ScriptTemplateCollection Templates { get; set; }

        public ScriptTemplateReferenceCollection TemplateReferences { get; set; }

        ToolStrip Toolbar()
        {
            ToolStrip output = new ToolStrip();
            output.Width = 200;
            output.Items.Add(new ToolStripLabel() { Text = "Template Reference Type: ", Width = 200 });

            ToolStripComboBox templatetypes = new ToolStripComboBox() { Width = 200, AutoSize = false };
            Enum.GetNames(typeof(ScriptTemplateReferenceType)).ToList().ForEach(delegate(string templatetype) { templatetypes.Items.Add(templatetype); });
            templatetypes.Text = Enum.GetName(typeof(ScriptTemplateReferenceType), TemplateReferences.Type);
            output.Items.Add(templatetypes);

            output.Items.Add(new ToolStripSeparator());
            output.Items.Add(new ToolStripButton("Add Mapping", null, CreateRow));

            return output;
        }

        DataGridView MappingTable { get; set; }

        DataGridView NewMappingTable()
        {
            DataGridView output = new DataGridView();
            output.Dock = DockStyle.Fill;
            output.AllowUserToAddRows = false;
            output.CellValueChanged += CellUpdated;
            output.Rows.CollectionChanged += RowCreated;
            output.MouseClick += GridClicked;

            DataGridViewComboBoxColumn templateselector = new DataGridViewComboBoxColumn() { HeaderText = "Template", Width = 400 };
            Templates.ForEach(delegate(ScriptTemplate template) { templateselector.Items.Add(template.Path() + "." + template.Name); });
            output.Columns.Add(templateselector);

            DataGridViewTextBoxColumn templatedisplay = new DataGridViewTextBoxColumn() { HeaderText = "Display Name", Width = 400 };
            output.Columns.Add(templatedisplay);

            foreach(ScriptTemplateReference reference in TemplateReferences)
            {
                DataGridViewRow newrow = new DataGridViewRow();
                newrow.Tag = reference;

                DataGridViewComboBoxCell templatecell = new DataGridViewComboBoxCell();
                Templates.ForEach(delegate (ScriptTemplate template) { templatecell.Items.Add(template.Path() + "." + template.Name); });
                templatecell.Value = reference.Template;
                newrow.Cells.Add(templatecell);

                DataGridViewTextBoxCell displaynamecell = new DataGridViewTextBoxCell();
                displaynamecell.Value = reference.DisplayName;
                newrow.Cells.Add(displaynamecell);

                output.Rows.Add(newrow);
            }

            return output;
        }

        void CreateRow(object sender, EventArgs e)
        {
            int rowid = MappingTable.Rows.Add();

            ScriptTemplateReference reference = new ScriptTemplateReference();

            TemplateReferences.Add(reference);
            MappingTable.Rows[rowid].Tag = reference;
        }

        void CellUpdated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            DataGridViewRow row = grid.Rows[e.RowIndex];

            if(row.Tag == null)
            {
                return;
            }

            ScriptTemplateReference reference = (ScriptTemplateReference)row.Tag;
            reference.Template = (string)row.Cells[0].EditedFormattedValue;
            reference.DisplayName = (string)row.Cells[1].EditedFormattedValue;
        }

        void RowCreated(object sender, CollectionChangeEventArgs e)
        {
            
        }

        void GridClicked(object sender, MouseEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            grid.DefaultCellStyle.BackColor = Color.White;

            if (e.Button == MouseButtons.Right)
            {
                int rowindex = grid.HitTest(e.X, e.Y).RowIndex;

                if (rowindex > -1)
                {
                    DataGridViewRow row = grid.Rows[rowindex];
                    ScriptTemplateReference reference = (ScriptTemplateReference)row.Tag;

                    grid.Rows[rowindex].DefaultCellStyle.BackColor = grid.DefaultCellStyle.SelectionBackColor;
                    //grid.ContextMenuStrip.Show(GridMenu(reference), new Point(e.X, e.Y));
                    grid.ContextMenuStrip = GridMenu(reference);
                    grid.ContextMenuStrip.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        ContextMenuStrip GridMenu(ScriptTemplateReference reference)
        {
            ContextMenuStrip output = new ContextMenuStrip();
            output.Items.Add("Delete Template Mapping", null, DeleteMapping);

            foreach (ToolStripMenuItem item in output.Items)
            {
                item.Tag = reference;
            }

            return output;
        }

        void DeleteMapping(object sender, EventArgs e)
        {
            ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;
            ScriptTemplateReference reference = (ScriptTemplateReference)menuitem.Tag;

            foreach (DataGridViewRow row in MappingTable.Rows)
            {
                if(row.Tag == reference)
                {
                    MappingTable.Rows.Remove(row);
                }
            }

            TemplateReferences.Remove(reference);
        }
    }
}
