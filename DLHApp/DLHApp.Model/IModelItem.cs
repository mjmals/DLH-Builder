using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model
{
    public interface IModelItem
    {
        public string? Name { get; set; }

        public string BasePath { get; }

        public string? SourcePath { get; set; }

        public string OutputExtension { get; }

        public TemplateReferenceCollection Templates { get; set; }

        public TemplateModelItem GetTemplateItems();

        public void Save();

        public static IModelItem Load() { return null; }

        public static IModelItem New() { return null; }

    }
}
