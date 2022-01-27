using System;
using System.Text.RegularExpressions;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace DLHBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Group raw = new Group();
            raw.Name = "RAW";

            GroupPath rawpath = new GroupPath("RAW", "Stage");
            raw.Paths = new GroupPath[] { rawpath };

            GroupPath nonsensitive = new GroupPath("Non Sensitive", "Classification");
            rawpath.AddChild(nonsensitive);

            GroupPath sensitive = new GroupPath("Sensitive", "Classification");
            rawpath.AddChild(sensitive);

            GroupPath nsinternal = new GroupPath("Internal", "Area Type");
            nonsensitive.AddChild(nsinternal);

            GroupPath nsexternal = new GroupPath("External", "Area Type");
            nonsensitive.AddChild(nsexternal);

            GroupPath sinternal = new GroupPath("Internal", "Area Type");
            sensitive.AddChild(sinternal);

            GroupPath sexternal = new GroupPath("External", "Area Type");
            sensitive.AddChild(sexternal);

            string data = JsonConvert.SerializeObject(raw, Formatting.Indented);

            using (FileStream stream = new FileStream("", FileMode.OpenOrCreate))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                }
            }

            Console.WriteLine();
        }
    }

    public class Group
    {
        public string Name { get; set; }

        public GroupPath[] Paths { get; set; }
    }

    public class GroupPath
    {
        public GroupPath(string name, string tag, string description = null)
        {
            Name = name;
            Tag = tag;
            Description = Description;
        }

        public string Name { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        public GroupPath[] Children { get; set; }

        public void AddChild(GroupPath path)
        {
            List<GroupPath> children = Children == null ? new List<GroupPath>() : Children.ToList();
            children.Add(path);

            Children = children.ToArray();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
