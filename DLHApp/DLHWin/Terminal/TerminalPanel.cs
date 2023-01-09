using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Commands;

namespace DLHWin.Terminal
{
    internal class TerminalPanel : Panel
    {
        public TerminalPanel()
        {
            Dock = DockStyle.Bottom;
            Controls.Add(TerminalBox);
            TerminalBox.KeyPress += TerminalEntry;
            TerminalBox.Enabled = false;
            TerminalBox.BackColor = Color.Black;
        }

        RichTextBox TerminalBox = new RichTextBox() { Dock = DockStyle.Fill, BackColor = Color.Black, ForeColor = Color.White, Height = 1000, Font = new Font("Cascadia Code", 10) };

        void TerminalEntry(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                CaptureTerminalEntry();
                ResetTerminalCommand();
            }
        }

        void ProtectEntries()
        {
            TerminalBox.Select(0, TerminalBox.TextLength);
            TerminalBox.SelectionProtected = true;
            TerminalBox.Select(TerminalBox.TextLength, 0);
        }

        string CurrentLocation()
        {
            if(!string.IsNullOrEmpty(SelectedLocation) && SelectedLocation != (new DirectoryInfo(Environment.CurrentDirectory)).Name)
            {
                return Path.Combine(Environment.CurrentDirectory, SelectedLocation);
            }

            return Environment.CurrentDirectory;
        }

        string SelectedLocation { get; set; }

        int CommandEntryStartPos = 0;

        void CaptureTerminalEntry()
        {
            string commandEntry = TerminalBox.Text.Substring(CommandEntryStartPos - 1, TerminalBox.TextLength - CommandEntryStartPos);
            string[] args = commandEntry.Split(" ");

            if (args[0].ToLower() == "clear")
            {
                TerminalBox.Clear();
            }

            CommandExecutor executor = new CommandExecutor(args);
            executor.CommandOutputWrite += OnCommandOutput;
            executor.Run();

            CommandExecuted?.Invoke(null, null);
        }

        public void ResetTerminalCommand(string selectedLocation = null)
        {
            TerminalBox.Enabled = true;
            SelectedLocation = selectedLocation;
            TerminalBox.AppendText("\n" + CurrentLocation() + ">");
            ProtectEntries();
            CommandEntryStartPos = TerminalBox.TextLength + 1;
        }

        public void OnCommandOutput(object? sender, CommandOutputEventArgs? e)
        {
            TerminalBox.AppendText(e.Output + "\n");
        }

        public EventHandler CommandExecuted { get; set; }
    }
}
