using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHWin.Editors.DefinitionEditors
{
    internal class DefinitionEditor : Editor
    {
        public DefinitionEditor(string fileName)
        {
            
        }

        string FileName { get; set; }

        Panel BasePanel = new Panel() { Dock = DockStyle.Fill };

        protected override Control[] EditorControls()
        {
            Type[] panelTypes = this.GetType().Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(DefinitionEditorPanel)) && x.IsAbstract == false).ToArray();

            foreach (Type panelType in panelTypes)
            {
                DefinitionEditorPanel panel = (DefinitionEditorPanel)Activator.CreateInstance(panelType, new object[] { FileName });

                if(panel.Extensions.Contains(FileName))
                {
                    panel.Dock = DockStyle.Fill;
                    return new Control[] { panel };
                }
            }

            return new Control[] { BasePanel };
        }
    }
}
