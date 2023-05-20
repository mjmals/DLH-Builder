using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Editors.SyntaxHighlighters
{
    internal abstract class SyntaxHighlighter
    {
        protected class Block
        {
            public Block(string startChar, string endChar, Color highlightColor)
            {
                StartChar = startChar;
                EndChar = endChar;
                HightlightColor = highlightColor;
            }

            public string StartChar { get; set; }

            public string EndChar { get; set; }

            public Color HightlightColor { get; set; }
        }

        protected virtual Dictionary<string, Color> Tokens()
        {
            return new Dictionary<string, Color>();
        }

        protected virtual List<Block> Blocks()
        {
            return new List<Block>();
        }

        public void Highlight(RichTextBox textBox)
        {
            int currentPos = textBox.SelectionStart;
            Font font = textBox.Font;
            textBox.SelectAll();
            textBox.SelectionColor = Color.Black;
            textBox.Font = font;

            try
            {
                HighlightBlocks(textBox);
                HighlightTokens(textBox);
            }
            catch (Exception e)
            { 

            }

            textBox.SelectionStart = currentPos;
            textBox.SelectionLength = 0;
            textBox.Font = font;
        }

        void HighlightBlocks(RichTextBox textBox)
        {
            foreach(Block block in Blocks())
            {
                int blockStartPos = textBox.Find(block.StartChar);
                
                while(blockStartPos > -1)
                {
                    int blockEndPos = textBox.Find(block.EndChar.ToCharArray(), blockStartPos + 1);

                    if(blockEndPos == -1 && (block.EndChar == "\n" || block.EndChar == Environment.NewLine))
                    {
                        blockEndPos = textBox.Text.IndexOf("\n", blockStartPos +1);
                    }

                    if(blockEndPos == -1)
                    {
                        blockEndPos = textBox.Text.Length;
                    }

                    textBox.Select(blockStartPos, blockEndPos + 1 - blockStartPos);
                    textBox.SelectionColor = block.HightlightColor;
                    blockStartPos = textBox.Find(block.StartChar.ToCharArray(), (blockEndPos == textBox.Text.Length ? blockEndPos : blockEndPos + 1));
                }
            }
        }

        void HighlightTokens(RichTextBox textBox)
        {
            foreach (var token in Tokens())
            {
                int tokenStartPos = textBox.Find(token.Key);

                while (tokenStartPos > -1)
                {
                    int tokenEndPos = token.Key.Length;

                    textBox.Select(tokenStartPos, tokenEndPos);

                    if(textBox.SelectionColor == Color.Black)
                    {
                        textBox.SelectionColor = token.Value;
                        textBox.SelectionFont = new Font(textBox.Font.FontFamily, textBox.Font.Size, FontStyle.Bold);
                    }

                    tokenStartPos = textBox.Find(token.Key, tokenStartPos + 1, RichTextBoxFinds.MatchCase);
                }
            }
        }
    }
}
