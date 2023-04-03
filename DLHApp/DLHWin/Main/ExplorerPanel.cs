using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Main
{
    internal class ExplorerPanel : Panel
    {
        public ExplorerPanel()
        {
            Dock = DockStyle.Fill;
            SetTreePanel();
        }

        Splitter Splitter = new Splitter() { Dock = DockStyle.Right, Width = 5 };

        public ProjectController Project 
        { 
            get => _project; 
            set
            {
                _project = value;
                SetTreePanel();
            }
        }

        private ProjectController _project { get; set; }

        public ExplorerTreePanel Tree { get; set; }

        internal void SetTreePanel(string filter = null)
        {
            Controls.Clear();
            Controls.Add(Tree = new ExplorerTreePanel(Project, filter));
            Tree.TreeSelectionChanged += OnTreeSelectionChanged;
        }

        public EventHandler<TreeViewEventArgs> TreeSelectionChanged { get; set; }

        void OnTreeSelectionChanged(object sender, TreeViewEventArgs e)
        {
            TreeSelectionChanged?.Invoke(Tree, new TreeViewEventArgs(e.Node));
        }

        public void ApplyFilter(string filter)
        {
            SetTreePanel(filter);
        }
    }
}
