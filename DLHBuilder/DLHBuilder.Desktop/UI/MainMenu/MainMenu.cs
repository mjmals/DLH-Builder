using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLHBuilder.Components;

namespace DLHBuilder.Desktop.UI
{
    class MainMenu : MenuStrip
    {
        public MainMenu()
        {
            ImageList = Images.Items;
            Items.Add(FileMenu);
        }

        public FileMenu FileMenu = new FileMenu();
    }
}
