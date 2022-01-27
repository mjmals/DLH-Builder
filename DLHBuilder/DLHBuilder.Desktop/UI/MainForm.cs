using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class MainForm : Form
    {
        public MainForm()
        {
            Text = "DLH Builder";
            WindowState = FormWindowState.Maximized;

            editorpanel.Controls.Add(new PropertyEditor(DockStyle.Fill));

            Controls.Add(editorpanel);
            Controls.Add(explorerpanel);
            Controls.Add(tools);
            Controls.Add(menu);

            Initialize();
        }

        Project project;

        MainMenu menu = new MainMenu();

        ToolBar tools = new ToolBar();

        ProjectTree tree = null;

        ExplorerPanel explorerpanel = new ExplorerPanel(new TreeView());

        EditorPanel editorpanel = new EditorPanel();

        void Initialize()
        {
            menu.FileMenu.NewProjectMenuPressed += NewProject;
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
        }

        void ProjectTreeNodeSelected(object sender, EventArgs e)
        {
            ProjectTree tview = (ProjectTree)sender;
            ProjectTreeNode node = (ProjectTreeNode)tview.SelectedNode;
            
            editorpanel.Controls.Clear();

            switch(node.ShowPropertyEditor)
            {
                case true:
                    editorpanel.Controls.Add(new PropertyEditor(DockStyle.Fill, node.Tag));
                    break;
                default:
                    break;
            }
        }
    }
}
