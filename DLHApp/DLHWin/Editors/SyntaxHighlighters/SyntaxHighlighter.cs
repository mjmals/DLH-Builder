using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Editors.SyntaxHighlighters
{
    internal abstract class SyntaxHighlighter
    {
        protected virtual Dictionary<string, Color> Tokens()
        {
            return new Dictionary<string, Color>();
        }

        public void Highlight(RichTextBox textBox)
        {
            int currentPos = textBox.SelectionStart;
            Font font = textBox.Font;
            textBox.SelectAll();
            textBox.SelectionColor = Color.Black;
            textBox.Font = font;

            foreach(var token in Tokens())
            {
                int tokenStartPos = textBox.Find(token.Key);

                while(tokenStartPos > -1)
                {
                    int tokenEndPos = token.Key.Length;

                    textBox.Select(tokenStartPos, tokenEndPos);
                    textBox.SelectionColor = token.Value;
                    textBox.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Bold);

                    tokenStartPos = textBox.Find(token.Key, tokenStartPos + 1, RichTextBoxFinds.MatchCase);
                }

            }

            textBox.SelectionStart = currentPos;
            textBox.SelectionLength = 0;
        }
    }
}
