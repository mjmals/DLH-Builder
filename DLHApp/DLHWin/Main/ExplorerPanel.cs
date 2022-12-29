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
            Dock = DockStyle.Left;
            Width = 400;
            Controls.Add(Splitter);
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

        internal void SetTreePanel()
        {
            Controls.Clear();
            Controls.Add(new ExplorerTreePanel(Project));
            Controls.Add(Splitter);
        }
    }
}
