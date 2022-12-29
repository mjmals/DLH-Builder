using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class KeyContainerCollection : BuilderCollection<KeyContainer>
    {
        protected override string FileNameProperty => "Name";

        protected override string DirectoryName => "Key Containers";
    }
}
