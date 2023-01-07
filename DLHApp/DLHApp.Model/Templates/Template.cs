using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.Templates
{
    public class Template : ModelItem, IModelItem
    {
        public override string BasePath => "Templates";

        public static Template Load(string path)
        {
            return new Template();
        }
    }
}
