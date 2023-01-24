using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHApp.Model.DataStructs;
using DLHWin.Grids;
using DLHWin.Editors.SyntaxHighlighters;

namespace DLHWin.Editors
{
    internal class DataStructEditor : Editor
    {
        public DataStructEditor(string structPath)
        {
            DataStructPath = structPath;
            DataStruct = DataStruct.Load(DataStructPath);
            Text = DataStruct.Name;

            GridPanel.Controls.Add(StructGrid = new DataStructEditorGrid(DataStruct));
            ScriptPanel.Controls.Add(ScriptBox);

            ScriptBox.Text = File.ReadAllText(DataStructPath + ".datastruct");
            new DataStructSyntaxHighlighter().Highlight(ScriptBox);
        }

        string DataStructPath { get; set; }

        DataStruct DataStruct { get; set; }

        Panel GridPanel = new Panel();

        Panel ScriptPanel = new Panel();

        RichTextBox ScriptBox = new RichTextBox() { Dock = DockStyle.Fill, AcceptsTab = true, Font = new Font("Cascadia Code", 9) };

        DataStructEditorGrid StructGrid { get; set; }

        protected override Control[] EditorControls()
        {
            return new Control[] { GridPanel, ScriptPanel };
        }
    }
}
