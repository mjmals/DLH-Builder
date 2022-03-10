using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class SQLDataApplicationNode : DataApplicationNode
    {
        public SQLDataApplicationNode(SQLDataApplication application) : base(application)
        {

        }

        public override ContextMenuStrip ContextMenuStrip => new SQLDataApplicationMenu(this);
    }
}
