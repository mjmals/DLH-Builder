using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHWin.ProjectTree;

namespace DLHWin.Main
{
    internal class MainForm : Form
    {
        public MainForm()
        {
            SetTitle();
            Controls.Add(DisplayPanel = new DisplayPanel());
            DisplayPanel.TerminalPanel.CommandExecuted += TerminalCommandExecuted;

            Controls.Add(ExplorerPanel = new ExplorerPanel());
            
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

        ExplorerPanel ExplorerPanel { get; set; }

        DisplayPanel DisplayPanel { get; set; }

        MenuBar Menu { get; set; }

        StatusBar StatusBar { get; set; }

        ToolBar ToolBar { get; set; }

        void ConfigureToolBars()
        {
            Menu.SetMenuItemClick(@"File\New\Project", NewProject);
            Menu.SetMenuItemClick(@"File\Open", OpenProject);

            Menu.SetMenuItemClick(@"Terminal\Hide Terminal", HideTerminal);
            Menu.SetMenuItemClick(@"Terminal\Show Terminal", ShowTerminal);

            ToolBar.SetToolbarItemClick("OpenProject", OpenProject);
            ToolBar.SetToolbarItemClick("Refresh", RefreshProject);
        }

        void NewProject(object? sender, EventArgs e)
        {
            MessageBox.Show("New Project Selected");
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

        void LoadProject(string fileName)
        {
            Project = ProjectController.Load(fileName);
            Environment.CurrentDirectory = Path.GetDirectoryName(fileName);
            ExplorerPanel.Project = Project;
            ExplorerPanel.TreeSelectionChanged += OnTreeSelectionChanged;
            DisplayPanel.TerminalPanel.ResetTerminalCommand();
        }

        void OnTreeSelectionChanged(object sender, TreeViewEventArgs e)
        {
            ProjectTreeNode node = (ProjectTreeNode)e.Node;
            DisplayPanel.EditorPanel.SetControls(node.Editors());
            DisplayPanel.TerminalPanel.ResetTerminalCommand(node.Name);
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
    }

}
