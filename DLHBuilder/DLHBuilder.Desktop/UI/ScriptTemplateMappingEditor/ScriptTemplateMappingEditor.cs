using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace DLHBuilder.Desktop.UI
{
    class ScriptTemplateMappingEditor : Editor
    {
        public ScriptTemplateMappingEditor(ScriptTemplateCollection templates, ScriptTemplateReferenceCollection templatererences)
        {
            Templates = templates;
            TemplateReferences = templatererences;

            Text = "Script Template Mapping";
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

            DataGridViewComboBoxColumn templateselector = new DataGridViewComboBoxColumn() { HeaderText = "Template", Width = 400 };
            Templates.ForEach(delegate(ScriptTemplate template) { templateselector.Items.Add(template.Path() + "." + template.Name); });
            output.Columns.Add(templateselector);

            DataGridViewTextBoxColumn templatedisplay = new DataGridViewTextBoxColumn() { HeaderText = "Display Name", Width = 400 };
            output.Columns.Add(templatedisplay);

            foreach(ScriptTemplateReference reference in TemplateReferences)
            {
                DataGridViewRow newrow = new DataGridViewRow();

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
    }
}
