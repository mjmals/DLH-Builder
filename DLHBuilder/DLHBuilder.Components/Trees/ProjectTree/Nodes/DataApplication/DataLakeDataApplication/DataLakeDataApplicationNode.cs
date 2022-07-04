using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataLakeDataApplicationNode : DataApplicationNode
    {
        public DataLakeDataApplicationNode(DataLakeDataApplication application) : base(application)
        {

        }

        public override ContextMenuStrip ContextMenuStrip => new DataLakeDataApplicationMenu(this);
    }
}
