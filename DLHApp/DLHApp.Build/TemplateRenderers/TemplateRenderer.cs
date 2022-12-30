using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHApp.Build.TemplateRenderers
{
    public abstract class TemplateRenderer
    {
        public virtual string FileExtension => string.Empty;

        public static TemplateRenderer GetRenderer(string templateFile)
        {
            string extension = Path.GetExtension(templateFile);

            Type[] rendererTypes = typeof(TemplateRenderer).Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(TemplateRenderer)) && x.IsAbstract == false).ToArray();

            foreach (Type rendererType in rendererTypes)
            {
                TemplateRenderer renderer = (TemplateRenderer)Activator.CreateInstance(rendererType);

                if(renderer.FileExtension == extension)
                {
                    return renderer;
                }
            }

            return null;
        }

        public virtual string Render(string templateFile, object baseObject, out string fileName)
        {
            fileName = string.Empty;
            return string.Empty;
        }
    }
}
