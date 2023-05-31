using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructReferences;
using DLHApp.Model.DataStructs;
using DLHWin.Grids.DataStructReferences;
using DLHWin.Styles;

namespace DLHWin.Editors
{
    internal class DataStructReferenceEditor : Editor
    {
        public DataStructReferenceEditor(string refPath)
        {
            Reference = DataStructReference.Load(refPath);
            SourceStruct = DataStruct.Load(Reference.SourceDataStruct);
            Text = Reference.Name;

            GridPanel.Controls.Add(RefGrid = new DataStructReferenceEditorGrid(Reference, SourceStruct));
            GridPanel.Controls.Add(Toolbar);

            Toolbar.Items.Add(SourceStructLabel);
            Toolbar.Items.Add(SourceStructDisplay);
            Toolbar.Items.Add(new ToolStripSeparator());
            Toolbar.Items.Add(AddReferenceBtn);
            Toolbar.Items.Add(new ToolStripSeparator());
            Toolbar.Items.Add(AddMetadataColumnBtn);
            SetSourceStructDisplay();

            AddReferenceBtn.Click += AddReferenceField;
        }

        public DataStructReference Reference { get; set; }

        public DataStruct SourceStruct { get; set; }

        Panel GridPanel = new Panel();

        DataStructReferenceEditorGrid RefGrid { get; set; }

        ToolStrip Toolbar = new ToolStrip() { ImageList = Images.List };

        ToolStripLabel SourceStructLabel = new ToolStripLabel() { Text = "Source Data Struct:" };

        ToolStripLabel SourceStructDisplay = new ToolStripLabel();

        ToolStripButton AddReferenceBtn = new ToolStripButton() { Text = "New Field Reference", ImageKey = "Field" };

        ToolStripButton AddMetadataColumnBtn = new ToolStripButton() { Text = "Add Metadata Column", Width = 150, ImageKey = "Add" };

        protected override Control[] EditorControls()
        {
            return new Control[] { GridPanel };
        }

        void SetSourceStructDisplay()
        {
            SourceStructDisplay.Text = Reference.SourceDataStruct;
        }

        void AddReferenceField(object sender, EventArgs e)
        {
            DataStructFieldReference fieldRef = new DataStructFieldReference();
            fieldRef.SourceField = SourceStruct.Fields.FirstOrDefault().Name;
            fieldRef.OutputName = "<Please provide Output Name>";
            Reference.Fields.Add(fieldRef);
            Reference.Save();

            RefGrid.AddRow(fieldRef);
        }
    }
}
