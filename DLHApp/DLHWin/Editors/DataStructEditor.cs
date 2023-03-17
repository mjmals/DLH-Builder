﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHApp.Model.DataStructs;
using DLHWin.Editors.SyntaxHighlighters;
using DLHWin.Grids.DataStructs;
using DLHWin.Styles;

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
            GridPanel.Controls.Add(StructDetailPanel);
            BuildStructDetailPanel();
            ScriptPanel.Controls.Add(ScriptBox);
            ScriptPanel.Controls.Add(ScriptToolBar());

            ScriptBox.Text = File.ReadAllText(DataStructPath + ".datastruct");
            new DataStructSyntaxHighlighter().Highlight(ScriptBox);
        }

        string DataStructPath { get; set; }

        DataStruct DataStruct { get; set; }

        Panel GridPanel = new Panel();

        Panel ScriptPanel = new Panel();

        RichTextBox ScriptBox = new RichTextBox() { Dock = DockStyle.Fill, AcceptsTab = true, Font = new Font("Cascadia Code", 9) };

        Panel StructDetailPanel = new Panel() { Dock = DockStyle.Right, AutoSize = false, Width = 300, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };

        DataStructEditorGrid StructGrid { get; set; }

        protected override Control[] EditorControls()
        {
            return new Control[] { GridPanel, ScriptPanel };
        }

        ToolStrip ScriptToolBar()
        {
            ToolStrip output = new ToolStrip();
            output.ImageList = Images.List;

            ToolStripButton updateBtn = new ToolStripButton();
            updateBtn.ImageKey = "Run";
            updateBtn.Text = "Update from Script";
            output.Items.Add(updateBtn);

            return output;
        }

        void BuildStructDetailPanel()
        {
            StructDetailPanel.Controls.Clear();
            StructDetailPanel.Controls.Add(StructRelationshipPanel());

            foreach(Control control in StructDetailPanel.Controls)
            {
                control.Width = StructDetailPanel.Width;
            }
        }

        Panel StructRelationshipPanel()
        {
            Panel output = new Panel();
            output.Height = 20;
            output.BackColor = Color.White;

            Label headerLabel = new Label();
            headerLabel.Text = string.Format("Relationships ({0})", DataStruct.Relationships == null ? "0" : DataStruct.Relationships.Count);
            headerLabel.Font = new Font(this.Font, FontStyle.Bold);
            headerLabel.AutoSize = true;
            headerLabel.Left = 5;
            output.Controls.Add(headerLabel);

            LinkLabel editLabel = new LinkLabel();
            editLabel.Text = "(edit)";
            editLabel.Top = headerLabel.Top;
            editLabel.Left = headerLabel.Right;
            output.Controls.Add(editLabel);

            foreach(DataStructRelationship relationship in DataStruct.Relationships)
            {
                Label relLabel = new Label();
                relLabel.Text = relationship.SourceDataStruct;
                relLabel.Top = output.Controls[output.Controls.Count - 1].Bottom;
                relLabel.Left = 15;
                relLabel.AutoSize = true;
                output.Controls.Add(relLabel);
                output.Height += relLabel.Height + 5;
            }

            return output;
        }
    }
}
