using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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

            Initialize();
        }

        string projectpath = string.Empty;

        Project project;

        MainMenu menu = new MainMenu();

        ToolBar tools = new ToolBar();

        ProjectTree tree = null;

        ExplorerPanel explorerpanel = new ExplorerPanel(new TreeView());

        EditorPanel editorpanel = new EditorPanel();

        void Initialize()
        {
            menu.FileMenu.NewProjectMenuPressed += NewProject;
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
                }

                return;
            }

            MessageBox.Show("No Project currently open.  Create or load project to continue.");
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

        void ProjectTreeNodeSelected(object sender, EventArgs e)
        {
            ProjectTree tview = (ProjectTree)sender;
            ProjectTreeNode node = (ProjectTreeNode)tview.SelectedNode;
            
            editorpanel.Controls.Clear();
            editorpanel.SetControls(node.Editors());
        }
    }
}
