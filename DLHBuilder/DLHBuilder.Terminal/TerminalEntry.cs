using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using DLHBuilder.Terminal.Commands;

namespace DLHBuilder.Terminal
{
    class TerminalEntry : RichTextBox
    {
        public TerminalEntry(Project project = null)
        {
            Project = project;
            Dock = DockStyle.Fill;
            BackColor = Color.Black;
            ForeColor = Color.White;
            Font = new Font("Consolas", 10);
            BorderStyle = BorderStyle.None;
            PrepareCommand();
            KeyDown += RegisterKeyPress;
        }

        public Project Project { get; set; }

        string CurrentPath { get; set; }

        int CommandStartPosition { get; set; }

        public List<string> CommandHistory = new List<string>();

        int PreviousCommand { get; set; }

        void ProtectEntries()
        {
            SelectAll();
            SelectionProtected = true;
            DeselectAll();
        }

        void PrepareCommand()
        {
            ForeColor = Color.Honeydew;
            AppendText(CurrentPath = string.IsNullOrEmpty(CurrentPath) ? "<no project open>" : CurrentPath);
            ForeColor = Color.White;

            AppendText("\n$ ");
            ProtectEntries();
            SelectionStart = this.Text.Length;
            SetCommandStartPosition();
        }

        void SetCommandStartPosition()
        {
            CommandStartPosition = SelectionStart;
        }

        void RegisterKeyPress(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                ProcessCommand(Text.Substring(CommandStartPosition));
                e.SuppressKeyPress = true;
            }

            if(e.KeyCode == Keys.Up)
            {
                ShowPreviousCommand();
                e.SuppressKeyPress = true;
            }
        }

        void ProcessCommand(string commandText)
        {
            CommandHistory.Add(commandText);
            PreviousCommand = 0;

            if(commandText.ToLower() == "clear")
            {
                Clear();
                PrepareCommand();
                return;
            }

            CommandHandler handler = new CommandHandler();
            handler.RunCommand(commandText);

            if(handler.CommandOutput is string)
            {
                AppendText("\n");
                AppendText((string)handler.CommandOutput);
            }

            if(handler.CommandOutput is Project)
            {
                Project = (Project)handler.CommandOutput;
                CurrentPath = string.Format("/{0}/", Project.Name);
            }

            AppendText("\n\n");
            PrepareCommand();
        }

        void ShowPreviousCommand()
        {
            if(PreviousCommand == 0)
            {
                PreviousCommand = CommandHistory.Count() - 1;
            }
            else
            {
                PreviousCommand = PreviousCommand - 1;
            }

            if (CommandStartPosition < Text.Length)
            {
                Text = Text.Remove(CommandStartPosition);
            }

            if(CommandHistory.Count == 0)
            {
                return;
            }

            ProtectEntries();
            AppendText(CommandHistory[PreviousCommand]);
        }
    }
}
