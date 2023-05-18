using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.Connections;
using DLHWin.Styles;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DLHWin.Editors.Dialogs
{
    internal class WindowsFileServerConnectionDialog : Form
    {
        public WindowsFileServerConnectionDialog()
        {
            Text = "Connect to Windows File Share";
            Height = 175;
            Width = 575;

            MinimizeBox = false;
            MaximizeBox = false;

            Controls.Add(ToolBar);
            Controls.Add(PathLabel);
            Controls.Add(PathBox);
            Controls.Add(PathBtn);

            ToolBar.Items.Add(ImportButton);
            PathBtn.Click += SelectFolder;
            ImportButton.Click += Import;
        }

        public WindowsFileServerConnection Connection { get; set; }

        ToolStrip ToolBar = new ToolStrip();

        ToolStripButton ImportButton = new ToolStripButton() { Text = "Use Connection", AutoSize = false, Width = 90 };

        Label PathLabel = new Label() { Location = new Point(50, 50), Width = 100, Text = "File Server Path:" };

        TextBox PathBox = new TextBox() { Location = new Point(160, 50), Width = 250 };

        Button PathBtn = new Button() { Location = new Point(420, 50), Width = 30, ImageList = Images.List, ImageKey = "Folder Open" };

        void SelectFolder(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;

                if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    PathBox.Text = dialog.FileName;
                }
            }
        }

        void Import(object sender, EventArgs e)
        {
            Connection = WindowsFileServerConnection.New();
            Connection.FullServerPath = PathBox.Text;

            DialogResult = DialogResult.OK;
            Close();
            Dispose();
        }

    }
}
