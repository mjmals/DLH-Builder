using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.BuildProfiles;
using DLHApp.Build;
using DLHWin.Styles;

namespace DLHWin.Editors.Dialogs
{
    internal class BuildProfileRunDialog : Form
    {
        public BuildProfileRunDialog(string buildProfile = null)
        {
            BuildProfileName = buildProfile;
            Text = "Run Build Profiles";

            Width = 1500;
            Height = 800;

            Controls.Add(BodyPanel);
            Controls.Add(ToolBar);

            ToolBar.Items.Add(RunBtn);
            RunBtn.Click += RunBuild;

            BodyPanel.Panel1.Controls.Add(ProfileGrid = SetupProfileGrid());
            BodyPanel.Panel2.Controls.Add(BuildOutputPanel());

            Resize += OnDialogResize;
            ProfileGrid.RowsAdded += ProfileListChanged;
            ProfileGrid.RowsRemoved += ProfileListChanged;

            OnDialogResize(null, null);

            if(!string.IsNullOrEmpty(BuildProfileName))
            {
                ProfileGrid.Rows.Add(new string[] { BuildProfileName, GetBuildProfile(BuildProfileName).UserConfig.TargetFolder });
            }
        }

        string BuildProfileName { get; set; }

        ToolStrip ToolBar = new ToolStrip() { ImageList = Images.List };

        ToolStripButton RunBtn = new ToolStripButton() { ImageKey = "Run", Text = "Run Selected (0)" };

        SplitContainer BodyPanel = new SplitContainer() { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal };

        DataGridView ProfileGrid { get; set; }

        RichTextBox BuildOutputBox = new RichTextBox() { Dock = DockStyle.Fill };

        TabControl BuildOutputPanel()
        {
            TabControl output = new TabControl() { Dock = DockStyle.Fill };

            TabPage buildOutputPage = new TabPage() { Text = "Build Output" };
            buildOutputPage.Controls.Add(BuildOutputBox);
            output.Controls.Add(buildOutputPage);

            return output;
        }


        DataGridView SetupProfileGrid()
        {
            DataGridView output = new DataGridView() { Dock = DockStyle.Fill, AllowUserToAddRows = true, RowHeadersVisible = false };
            output.CellValueChanged += CellValueChanged;

            DataGridViewComboBoxColumn profileColumn = new DataGridViewComboBoxColumn() { HeaderText = "Build Profile", Width = 300 };
            profileColumn.Items.AddRange(BuildProfiles);
            output.Columns.Add(profileColumn);

            DataGridViewTextBoxColumn targetColumn = new DataGridViewTextBoxColumn() { HeaderText = "Destination Path", Width = 300 };
            output.Columns.Add(targetColumn);

            return output;
        }

        string[] BuildProfiles 
        { 
            get
            {
                if(_buildProfiles == null)
                {
                    _buildProfiles = GetBuildProfiles();
                }

                return _buildProfiles;
            }
        }

        string[] _buildProfiles { get; set; }

        string[] GetBuildProfiles()
        {
            List<string> output = new List<string>();

            foreach(string file in Directory.GetFiles("Build Profiles", "*.json", SearchOption.AllDirectories))
            {
                if(!file.Contains(".local."))
                {
                    output.Add(file.Replace(".json", ""));
                }
            }

            return output.ToArray();
        }


        void OnDialogResize(object sender, EventArgs e)
        {
            foreach(DataGridViewColumn column in ProfileGrid.Columns)
            {
                column.Width = ProfileGrid.Width / ProfileGrid.Columns.Count - 2;
            }
        }

        void CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = ProfileGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            DataGridViewColumn column = ProfileGrid.Columns[e.ColumnIndex];

            switch(column.HeaderText)
            {
                case "Build Profile":
                    ProfileSelectionChanged((DataGridViewComboBoxCell)cell, e.RowIndex);
                    break;
                default:
                    break;
            }
        }

        BuildProfile GetBuildProfile(string name)
        {
            return BuildProfile.Load(name);
        }


        void ProfileSelectionChanged(DataGridViewComboBoxCell cell, int rowIndex)
        {
            ProfileGrid.Rows[rowIndex].Cells[cell.ColumnIndex + 1].Value = GetBuildProfile(cell.Value.ToString()).UserConfig.TargetFolder;
        }

        void ProfileListChanged(object sender, EventArgs e)
        {
            RunBtn.Text = string.Format("Run Selected ({0})", ProfileGrid.Rows.Count - 1);
        }

        void RunBuild(object sender, EventArgs e)
        {
            BuildOutputBox.Clear();

            foreach(DataGridViewRow profileRow in ProfileGrid.Rows)
            {
                if (profileRow.Cells[0].Value == null)
                {
                    continue;
                }

                string buildProfilePath = profileRow.Cells[0].Value.ToString();
                string buildTarget = profileRow.Cells[1].Value.ToString();

                string outputMessage = string.Format("Build of Profile \"{0}\" to \"{1}\" -> ", buildProfilePath, buildTarget) + "{0}\n";

                try
                {
                    BuildEngine engine = new BuildEngine(buildProfilePath);
                    engine.Run();
                    BuildOutputBox.AppendText(string.Format(outputMessage, "SUCCESSFUL"));
                }
                catch (Exception ex)
                {
                    BuildOutputBox.AppendText(string.Format(outputMessage, "FAILED:"));
                    BuildOutputBox.AppendText(ex.Message);
                }
            }
        }
    }
}
