﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DLHBuilder
{
    public class ScriptTemplateCollection : BuilderCollection<ScriptTemplate>
    {
        protected override string DirectoryName => "Templates";

        internal override void Load(string path)
        {
            string searchpath = Path.Combine(path, DirectoryName);
            string[] files = Directory.GetFiles(searchpath, "*.json", SearchOption.AllDirectories);

            foreach(string file in files)
            {
                FileMetadataExtractor extractor = new FileMetadataExtractor(file);
                ScriptTemplate template = extractor.LoadFile<ScriptTemplate>();
                Add(template);

                using (StreamReader rdr = new StreamReader(new FileStream(file.Replace(".json", ".st"), FileMode.OpenOrCreate)))
                {
                    template.Content = rdr.ReadToEnd();
                }
            }
        }

        internal override void Save(string path)
        {
            foreach(ScriptTemplate template in this.Where(x => x.Type != ScriptTemplateType.BuiltIn))
            {
                string filepath = Path.Combine(path, DirectoryName, Path.Combine(template.Hierarchy.ToArray()), template.Name + ".json");

                if (!Directory.Exists(Path.GetDirectoryName(filepath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filepath));
                }

                FileMetadataExtractor extractor = new FileMetadataExtractor(filepath);
                extractor.Write(template);

                string templatefile = filepath.Replace(".json", ".st");

                using (FileStream stream = new FileStream(templatefile, FileMode.OpenOrCreate))
                {
                    stream.SetLength(0);
                    new StreamWriter(stream).Write(template.Content);
                }
            }
        }
    }
}
