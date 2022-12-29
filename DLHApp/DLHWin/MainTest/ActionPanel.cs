using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.MainTest
{
    internal class ActionPanel : Panel
    {
        public ActionPanel()
        {
            Dock = DockStyle.Left;
            Width = 450;
            BackColor = ColorTranslator.FromHtml("#2d2d30");
        }
    }
}
