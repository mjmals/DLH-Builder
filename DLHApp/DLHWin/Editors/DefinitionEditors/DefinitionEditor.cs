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
        public DefinitionEditor(string fileName, string[] identifiers = null, string identifierLabel = null)
        {
            FileName = fileName;
            Identifiers = identifiers;
            IdentifierLabel = identifierLabel;

            Text = Path.GetFileName(fileName);
            BasePanel.Controls.Add(BasePanelContent());
        }

        string FileName { get; set; }

        string[] Identifiers { get; set; }

        string IdentifierLabel { get; set; }

        Panel BasePanel = new Panel() { Dock = DockStyle.Fill };

        protected override Control[] EditorControls()
        {
            return new Control[] { BasePanel };
        }

        Panel BasePanelContent()
        {
            Type[] panelTypes = this.GetType().Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(DefinitionEditorPanel)) && x.IsAbstract == false).ToArray();

            foreach (Type panelType in panelTypes)
            {
                DefinitionEditorPanel panel = (DefinitionEditorPanel)Activator.CreateInstance(panelType, new object[] { FileName, Identifiers, IdentifierLabel });

                if (panel.Extensions.Contains(Path.GetExtension(FileName)))
                {
                    panel.Dock = DockStyle.Fill;
                    return panel;
                }
            }

            return new BaseDefinitionEditorPanel(FileName, Identifiers, IdentifierLabel);
        }
    }
}
