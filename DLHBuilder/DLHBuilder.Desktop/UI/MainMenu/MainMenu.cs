using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class MainMenu : MenuStrip
    {
        public MainMenu()
        {
            ImageList = Images.ImageList;
            Items.Add(FileMenu);
        }

        public FileMenu FileMenu = new FileMenu();
    }
}
