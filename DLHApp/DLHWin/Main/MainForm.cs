﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;

namespace DLHWin.Main
{
    internal class MainForm : Form
    {
        public MainForm()
        {
            SetTitle();
            Controls.Add(DisplayPanel = new DisplayPanel());
            Controls.Add(ExplorerPanel = new ExplorerPanel());
            
            Controls.Add(ToolBar = new ToolBar());
            Controls.Add(Menu = new MenuBar());
            ConfigureToolBars();
            
            Controls.Add(StatusBar = new StatusBar());

            WindowState = FormWindowState.Maximized;
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

            ToolBar.SetToolbarItemClick("OpenProject", OpenProject);
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
                    Project = ProjectController.Load(dialog.FileName);
                    ExplorerPanel.Project = Project;
                }
            }
        }
    }

}