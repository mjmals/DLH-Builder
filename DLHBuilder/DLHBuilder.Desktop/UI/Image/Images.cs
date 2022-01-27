using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    static class Images
    {
        public static ImageList ImageList
        {
            get
            {
                ImageList output = new ImageList();
                output.Images.Add("Open", Properties.Resources.OpenFile_16x);
                output.Images.Add("Project", Properties.Resources.ProjectFilterFile_16x);
                output.Images.Add("Save", Properties.Resources.Save_16x);
                return output;
            }
        }
    }
}
