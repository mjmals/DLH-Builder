﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class ProjectMenu : ProjectTreeMenu
    {
        public ProjectMenu(ProjectNode node)
        {
            Node = node;
        }

        ProjectNode Node
        {
            get => (ProjectNode)Tag;
            set => Tag = value;
        }
    }
}
