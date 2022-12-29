using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class NewProjectEventArgs : EventArgs
    {
        public NewProjectEventArgs(Type projecttype)
        {
            ProjectType = projecttype;
        }

        public Type ProjectType { get; set; }
    }
}
