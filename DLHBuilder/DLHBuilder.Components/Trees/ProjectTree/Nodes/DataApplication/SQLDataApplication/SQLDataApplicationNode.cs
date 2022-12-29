using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class SQLDataApplicationNode : DataApplicationNode
    {
        public SQLDataApplicationNode(SQLDataApplication application) : base(application)
        {

        }

        public override ContextMenuStrip ContextMenuStrip => new SQLDataApplicationMenu(this);
    }
}
