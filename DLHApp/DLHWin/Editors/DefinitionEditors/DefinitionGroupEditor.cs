using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Editors.DefinitionEditors
{
    internal class DefinitionGroupEditor : Editor
    {
        public DefinitionGroupEditor(string definitionDirectory, string[] identifiers, string identifierLabel)
        { 
            DefinitionDirectory = definitionDirectory;
            Identifiers = identifiers;
            IdentifierLabel = identifierLabel;

            Text = "Definitions";

            BodyPanel.Controls.Add(Grid = new DefinitionGroupEditorGrid(DefinitionDirectory, Identifiers, IdentifierLabel));
        }

        public string DefinitionDirectory { get; set; }

        public string[] Identifiers { get; set; }

        public string IdentifierLabel { get; set; }

        Panel BodyPanel = new Panel() { Dock = DockStyle.Fill };

        DefinitionGroupEditorGrid Grid { get; set; }

        protected override Control[] EditorControls()
        {
            return new Control[] { BodyPanel };
        }
    }
}
