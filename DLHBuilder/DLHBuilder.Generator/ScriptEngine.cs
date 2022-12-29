using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4;
using System.IO;
using Antlr4.StringTemplate;
using System.Reflection;
using DLHBuilder.Generator.Renderers;

namespace DLHBuilder.Generator
{
    public class ScriptEngine
    {
        public ScriptEngine(ScriptTemplate template, params object[] baseObjects)
        {
            Template = template;
            BaseObjects = baseObjects;
        }

        ScriptTemplate Template { get; set; }

        object[] BaseObjects { get; set; }

        public string Render()
        {
            try
            {
                switch (Template.Engine)
                {
                    case ScriptTemplateEngineType.StringTemplate:
                        return new StringTemplateRenderer(Template, BaseObjects).Render();
                    case ScriptTemplateEngineType.Razor:
                        return new RazorRenderer(Template, BaseObjects).Render();
                    default:
                        return Template.Content;
                }
            }
            catch(Exception e)
            {
                return "ERROR: Failed to compile script";
            }
        }
    }
}
