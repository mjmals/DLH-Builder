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

        public static string DefaultFileContent()
        {
            string output = string.Empty;

            output += "@{";
            output += "\n\tTemplateItems = (TemplateModelItem)Model;";
            output += "\n\tModel = (ModelItem)Model[\"Main\"];";
            output += "\n}";
            output += "\n\nTemplateHeaderFileName: @string.Format(\"{0}.json\", Model.Name)";
            output += "\n\n";

            return output;
        }
    }
}
