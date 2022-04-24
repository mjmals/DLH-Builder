using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class ScriptTemplateFolderNode : ProjectTreeNode
    {
        public ScriptTemplateFolderNode(string name, string path, bool allowupdate)
        {
            Path = string.Concat(path, ".", name);
            Name = path;
            Text = name;
            AllowUpdate = allowupdate;

            if(AllowUpdate)
            {
                ContextMenuStrip = new ScriptTemplateFolderMenu(this);
            }
        }

        public bool AllowUpdate { get; set; }

        public string Path { get; set; }

        public override void LabelChanged(string text)
        {
            base.LabelChanged(text);
            UpdatePath();
        }


        void UpdatePath()
        {
            List<string> pathitems = Path.Split('.').ToList();
            pathitems[pathitems.IndexOf(pathitems.Last())] = Text;

            Path = string.Join('.', pathitems);
            Name = Path;
        }
    }
}
