using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DLHBuilder.Components;
using DLHBuilder.Components.Editors;
using DLHBuilder.Components.Trees;
using DLHBuilder.Components.Trees.ProjectTreeView;

namespace DLHBuilder.Desktop.UI
{
    class MainForm : Form
    {
        public MainForm()
        {
            Text = "DLH Builder";
            WindowState = FormWindowState.Maximized;

            Controls.Add(editorpanel);
            Controls.Add(explorerpanel);
            Controls.Add(tools);
            Controls.Add(menu);
            Controls.Add(statbar);

            editorpanel.SetControls(new EditorCollection(new PropertyEditor()));

            Initialize();
        }

        string projectpath = string.Empty;

        Project project;

        MainMenu menu = new MainMenu();

        ToolBar tools = new ToolBar();

        StatusBar statbar = new StatusBar();

        ProjectTree tree = null;

        ExplorerPanel explorerpanel = new ExplorerPanel(new TreeView());

        EditorPanel editorpanel = new EditorPanel();

        void Initialize()
        {
            menu.FileMenu.NewProjectMenuPressed += NewProject;
            menu.FileMenu.NewMSSQLProjectMenuPressed += NewMSSQLProject;
            tools.NewButton.Click += NewProject;
            tools.OpenButton.Click += LoadProject;
            tools.SaveButton.Click += SaveProject;
        }

        void LoadProject(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Project Files (*.project.json)|*.project.json";
            
            switch(dialog.ShowDialog())
            {
                case DialogResult.OK:
                    project = Project.Load(dialog.FileName);
                    projectpath = Path.GetDirectoryName(Path.GetDirectoryName(dialog.FileName));
                    tree = new ProjectTree(project);
                    explorerpanel.Reset(tree);
                    ProjectTreeLoaded(null, null);
                    SetTitle();
                    break;
                default:
                    break;
            }
        }

        void SaveProject(object sender, EventArgs e)
        {
            if(project != null)
            {
                if(string.IsNullOrEmpty(projectpath))
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    
                    switch(dialog.ShowDialog())
                    {
                        case DialogResult.OK:
                            projectpath = dialog.SelectedPath;
                            break;
                        default:
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(projectpath))
                {
                    project.Save(projectpath);
                    MessageBox.Show("Project saved successfully...");
                    SetTitle();
                }

                return;
            }

            MessageBox.Show("No Project currently open.  Create or load project to continue.");
        }

        void SetTitle()
        {
            if(project != null)
            {
                Text = string.Format("DLH Builder - {0}", Path.Combine(projectpath, project.Name));
                return;
            }

            Text = "DLH Builder";
        }

        void ProjectTreeLoaded(object sender, EventArgs e)
        {
            tree.AfterSelect += ProjectTreeNodeSelected;
        }

        void NewProject(object sender, EventArgs e)
        {
            project = new Project();
            project.Name = "<New Project>";

            tree = new ProjectTree(project);
            ProjectTreeLoaded(null, null);
            explorerpanel.Reset(tree);
            projectpath = string.Empty;
        }

        void NewMSSQLProject(object sender, EventArgs e)
        {
            project = new MSSQLProject();

            tree = new ProjectTree(project);
            ProjectTreeLoaded(null, null);
            explorerpanel.Reset(tree);
            projectpath = string.Empty;
        }

        void ProjectTreeNodeSelected(object sender, EventArgs e)
        {
            ProjectTree tview = (ProjectTree)sender;
            ProjectTreeNode node = (ProjectTreeNode)tview.SelectedNode;
            
            editorpanel.Controls.Clear();
            editorpanel.SetControls(node.Editors());
        }
    }
}
