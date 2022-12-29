using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.MainTest
{
    internal class SidePanel : Panel
    {
        public SidePanel()
        {
            BackColor = ColorTranslator.FromHtml("#3E3E42");
            Width = 55;
            Dock = DockStyle.Left;
            Controls.AddRange(PanelButtons());
        }


        Panel[] PanelButtons()
        {
            List<Panel> output = new List<Panel>();

            Dictionary<string, Image> panelBtns = new Dictionary<string, Image>();
            panelBtns.Add("Connections", null);
            panelBtns.Add("Data Structures", null);

            foreach(KeyValuePair<string, Image> keyPair in panelBtns)
            {
                string btnName = keyPair.Key;
                Image btnImg = keyPair.Value;

                Panel panelBtn = new Panel() {
                    Width = this.Width,
                    Height = this.Width,
                    Location = new Point(0, output.Count > 0 ? output.Last().Bottom + 20 : 20),
                    Padding = new Padding(5),
                    Cursor = Cursors.Hand
                };

                //panelBtn.Controls.Add(new PictureBox()
                //{
                //    Dock = DockStyle.Fill,
                //    Image = btnImg
                //});

                panelBtn.Controls.Add(new Label(){ 
                    Dock = DockStyle.Fill,
                    Text = btnName.Substring(0, 1),
                    Font = new Font("Consolas", 20),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = ColorTranslator.FromHtml("#a9a9a9")
                });

                panelBtn.Controls.Add(new Panel()
                {
                    Dock = DockStyle.Left,
                    Height = panelBtn.Height,
                    Width = 1,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = ColorTranslator.FromHtml("#a9a9a9")
                });

                ToolTip toolTip = new ToolTip();
                toolTip.SetToolTip(panelBtn.Controls[0], btnName);

                output.Add(panelBtn);
            }

            return output.ToArray();
        }
    }
}
