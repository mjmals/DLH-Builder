using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class AzureStorageDataConnection : DataConnection, IDataConnection
    {
        public string SubscriptionName { get; set; }

        public string StorageAccountName { get; set; }

        public string ContainerName { get; set; }
    }
}
