using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHWin.ProjectTree;
using DLHWin.Editors.Dialogs;
using DLHWin.Config;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DLHWin.Main
{
    internal class MainForm : Form
    {
        public MainForm()
        {
            SetTitle();

            BodyPanel = new SplitContainer();
            BodyPanel.Dock = DockStyle.Fill;
            Controls.Add(BodyPanel);

            BodyPanel.Panel2.Controls.Add(DisplayPanel = new DisplayPanel());
            DisplayPanel.TerminalPanel.CommandExecuted += TerminalCommandExecuted;

            BodyPanel.Panel1.Controls.Add(ExplorerPanel = new ExplorerPanel());
            
            Controls.Add(ToolBar = new ToolBar());
            Controls.Add(Menu = new MenuBar());
            ConfigureToolBars();
            
            Controls.Add(StatusBar = new StatusBar());

            WindowState = FormWindowState.Maximized;

            if(File.Exists(Path.Combine(Environment.CurrentDirectory, "project.json")))
            {
                LoadProject(Path.Combine(Environment.CurrentDirectory, "project.json"));
            }
        }


        void SetTitle()
        {
            Text = "DLHWin";
        }

        ProjectController Project { get; set; }

        SplitContainer BodyPanel { get; set; }

        ExplorerPanel ExplorerPanel { get; set; }

        DisplayPanel DisplayPanel { get; set; }

        MenuBar Menu { get; set; }

        StatusBar StatusBar { get; set; }

        ToolBar ToolBar { get; set; }

        void ConfigureToolBars()
        {
            Menu.SetMenuItemClick(@"File\New\Project", NewProject);
            Menu.SetMenuItemClick(@"File\Open", OpenProject);

            UserConfigRecentProjectCollection recentFiles = UserConfig.Load().RecentProjects;

            foreach (string recentFile in recentFiles)
            {
                string menuItem = string.Format(@"File\Recent Files\{0}", recentFile);
                Menu.SetMenuItemClick(menuItem, OpenRecentProject);
            }

            Menu.SetMenuItemClick(@"Terminal\Hide Terminal", HideTerminal);
            Menu.SetMenuItemClick(@"Terminal\Show Terminal", ShowTerminal);

            ToolBar.SetToolbarItemClick("NewProject", NewProject);
            ToolBar.SetToolbarItemClick("OpenProject", OpenProject);
            ToolBar.SetToolbarItemClick("Build", RunBuild);
            ToolBar.SetToolbarItemClick("Refresh", RefreshProject);
            ToolBar.SetToolbarItemClick("ApplyFilter", FilterProject);
        }

        void NewProject(object? sender, EventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                
                if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    ProjectController.Create(dialog.FileName);
                    LoadProject(Path.Combine(dialog.FileName, "project.json"));
                }
            }
        }

        void OpenProject(object? sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog() { Filter = "project.json | project.json" })
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    LoadProject(dialog.FileName);
                }
            }
        }

        void OpenRecentProject(object sender, EventArgs e)
        {
            string fileName = string.Empty;

            if(sender is string)
            {
                fileName = (string)sender;
            }

            if(sender is ToolStripMenuItem)
            {
                fileName = ((ToolStripMenuItem)sender).Text;
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                LoadProject(fileName);
            }
        }

        void LoadProject(string fileName)
        {
            Project = ProjectController.Load(fileName);
            Environment.CurrentDirectory = Path.GetDirectoryName(fileName);
            ExplorerPanel.Project = Project;
            ExplorerPanel.TreeSelectionChanged += OnTreeSelectionChanged;
            DisplayPanel.TerminalPanel.ResetTerminalCommand();
            UserConfig.Load().RecentProjects.Add(fileName);
        }

        void OnTreeSelectionChanged(object sender, TreeViewEventArgs e)
        {
            ProjectTreeNode node = (ProjectTreeNode)e.Node;
            DisplayPanel.EditorPanel.SetControls(node.Editors());
            //DisplayPanel.TerminalPanel.ResetTerminalCommand(node.Name);
        }

        void TerminalCommandExecuted(object sender, EventArgs e)
        {
            ExplorerPanel.Tree.RefreshTree();
        }

        void HideTerminal(object sender, EventArgs e)
        {
            DisplayPanel.HideTerminal();
        }

        void ShowTerminal(object sender, EventArgs e)
        {
            DisplayPanel.ShowTerminal();
        }

        void RefreshProject(object sender, EventArgs e)
        {
            ExplorerPanel.Tree.RefreshTree();
        }

        void FilterProject(object sender, EventArgs e)
        {
            string filter = ToolBar.GetTextboxValue("FilterText");
            ExplorerPanel.ApplyFilter(filter);
        }

        void RunBuild(object sender, EventArgs e)
        {
            using (BuildProfileRunDialog dialog = new BuildProfileRunDialog())
            {
                dialog.ShowDialog();
            }
        }
    }

}
