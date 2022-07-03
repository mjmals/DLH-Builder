using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Dialogs
{
    class SQLConnectionSelectionDialog : ConnectionSelectionDialog
    {
        public SQLConnectionSelectionDialog(DataConnectionCollection connections) : base(connections)
        {

        }

        protected override void AddConnections(DataConnectionCollection connections)
        {
            foreach (DataConnection connection in connections.Where(x => x.GetType() == typeof(SQLDataConnection)).OrderBy(x => x.Name))
            {
                Connection.Items.Add(connection);
            }
        }
    }
}
