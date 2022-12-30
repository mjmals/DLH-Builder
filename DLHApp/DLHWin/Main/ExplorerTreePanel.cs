﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.ProjectTree;

namespace DLHWin.Main
{
    internal class ExplorerTreePanel : Panel
    {
        public ExplorerTreePanel(ProjectController project)
        {
            Dock = DockStyle.Fill;
            Controls.Add(Tree = new Tree(project));
            Tree.AfterSelect += OnTreeSelectionChanged;
        }

        Tree Tree { get; set; }

        public EventHandler<TreeViewEventArgs> TreeSelectionChanged { get; set; }

        void OnTreeSelectionChanged(object sender, TreeViewEventArgs e)
        {
            TreeSelectionChanged?.Invoke(Tree, new TreeViewEventArgs(e.Node));
        }
    }
}
