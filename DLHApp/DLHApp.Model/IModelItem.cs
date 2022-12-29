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

        public void Save();

        public static IModelItem Load() { return null; }

        public static IModelItem New() { return null; }
    }
}
