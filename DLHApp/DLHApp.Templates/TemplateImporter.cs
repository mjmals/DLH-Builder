using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHApp.Templates
{
    public class TemplateImporter
    {
        public static void Run()
        {
            string[] templateFiles = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            foreach(string templateFile in templateFiles)
            {
                WriteTemplate(GetTemplateName(templateFile), GetTemplateContent(templateFile));
            }
        }

        static string GetTemplateContent(string templateFile)
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(templateFile))
            {
                using (StreamReader rdr = new StreamReader(stream))
                {
                    return rdr.ReadToEnd();
                }
            }
        }

        static string GetTemplateName(string templateFile)
        {
            List<string> templatePath = templateFile.Split(".").ToList();
            int startPos = templatePath.IndexOf("Templates");

            return string.Join(@"\", templatePath.TakeLast(templatePath.Count - startPos).SkipLast(1)) + "." + templatePath.Last();
        }

        static void WriteTemplate(string templateName, string templateContent)
        {
            string templateFolder = Path.GetDirectoryName(templateName);

            if(!Directory.Exists(templateFolder))
            {
                Directory.CreateDirectory(templateFolder);
            }

            using (FileStream fs = new FileStream(templateName, FileMode.OpenOrCreate))
            {
                fs.SetLength(0);

                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(templateContent);
                }
            }
        }
    }
}
