using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Grids.DataStructs
{
    internal class DataStructEditorGridMetadataColumnDialog : Form
    {
        public DataStructEditorGridMetadataColumnDialog()
        {
            Text = "New Metadata Column";
            Height = 175;
            Width = 500;

            Controls.Add(ToolBar);
            ToolBar.Items.Add(AddBtn);
            AddBtn.Click += Add;

            Controls.Add(ColumnLabel);
            Controls.Add(ColumnBox);
        }

        public string ColumnName { get; set; }

        ToolStrip ToolBar = new ToolStrip();

        ToolStripButton AddBtn = new ToolStripButton() { Text = "Add Metadata Column" };

        Label ColumnLabel = new Label() { Location = new Point(50, 50), Text = "Column Name:" };

        TextBox ColumnBox = new TextBox() { Location = new Point(200, 50), Width = 200 };

        void Add(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(ColumnBox.Text))
            {
                MessageBox.Show("Please specify a Column Name");
                return;
            }

            ColumnName = ColumnBox.Text;
            DialogResult = DialogResult.OK;
            Close();
            Dispose();
        }
    }
}
