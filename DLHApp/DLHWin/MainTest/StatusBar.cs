using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.MainTest
{
    internal class StatusBar : StatusStrip
    {
        public StatusBar()
        {
            BackColor = ColorTranslator.FromHtml("#007ACC");
            Dock = DockStyle.Bottom;
            Height = 20;
        }
    }
}
