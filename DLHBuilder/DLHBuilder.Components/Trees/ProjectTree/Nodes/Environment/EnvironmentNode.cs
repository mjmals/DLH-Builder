using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class EnvironmentNode : ProjectTreeNode
    {
        public EnvironmentNode(IEnvironment environment)
        {
            Text = environment.Name;
            Environment = environment;
        }

        IEnvironment Environment
        {
            get => (IEnvironment)Tag;
            set
            {
                ((Environment)value).PropertyUpdated += SetTitle;
                Tag = value;
            }
        }

        void SetTitle(object sender, EventArgs e)
        {
            Text = Environment.Name;
        }

        public override void LabelChanged(string text)
        {
            base.LabelChanged(text);
            Environment.Name = text;
        }

        public override string ExpandedImage => "Environment";

        public override string CollapsedImage => "Environment";
    }
}
