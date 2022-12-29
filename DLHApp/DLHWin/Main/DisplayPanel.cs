﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Terminal;

namespace DLHWin.Main
{
    internal class DisplayPanel : Panel
    {
        public DisplayPanel()
        {
            Dock = DockStyle.Fill;
            Controls.Add(EditorPanel = new EditorPanel());
            Controls.Add(TerminalPanel = new TerminalPanel());
            Resize += UpdateInterface;
            UpdateInterface();
        }

        public EditorPanel EditorPanel { get; set; }

        public TerminalPanel TerminalPanel { get; set; }

        public ProjectController Project 
        { 
            get => _project; 
            set
            {
                _project = value;
                OnProjectChanged();
            }
        }

        private ProjectController _project { get; set; }

        void OnProjectChanged()
        {
            
        }

        void UpdateInterface(object? sender = null, EventArgs? e = null)
        {
            foreach(Control ctrl in Controls)
            {
                ctrl.Width = this.Width;
                ctrl.Dock = DockStyle.None;
            }

            //if(TerminalPanel.Visible == false)
            //{
            //    EditorPanel.Dock = DockStyle.Fill;
            //    return;
            //}

            EditorPanel.Height = Convert.ToInt32(this.Height * 0.65);
            EditorPanel.Location = new Point(0, 0);
            TerminalPanel.Height = Convert.ToInt32(this.Height * 0.35);
            TerminalPanel.Location = new Point(0, EditorPanel.Bottom);
        }
    }
}
