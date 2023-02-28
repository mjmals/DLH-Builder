using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Editors.SyntaxHighlighters;

namespace DLHWin.Editors
{
    internal class TextFileEditor : Editor
    {
        public TextFileEditor(string fileName)
        {
            FileName = fileName;
            TextEditor.Text = File.ReadAllText(fileName);
            Text = Path.GetFileName(fileName);

            switch(Path.GetExtension(FileName))
            {
                case ".cshtml":
                    Highlighter = new CSharpSyntaxHighlighter();
                    break;
            }

            HighlightText(null, null);
            TextEditor.TextChanged += WriteFile;
            TextEditor.TextChanged += HighlightText;
        }

        string FileName { get; set; }

        RichTextBox TextEditor = new RichTextBox() { Dock = DockStyle.Fill, AcceptsTab = true, Font = new Font("Cascadia Code", 9) };

        SyntaxHighlighter Highlighter { get; set; }

        protected override Control[] EditorControls()
        {
            return new Control[] { TextEditor };
        }

        void HighlightText(object sender, EventArgs e)
        {
            if (Highlighter != null)
            {
                Highlighter.Highlight(TextEditor);
            }
        }

        void WriteFile(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                fs.SetLength(0);

                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(TextEditor.Text);
                }
            }
        }
    }
}
