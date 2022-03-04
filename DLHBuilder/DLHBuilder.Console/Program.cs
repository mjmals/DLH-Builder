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
using System.Reflection;
using DLHBuilder.Generator;

namespace DLHBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            string template = "Hello $Template.Name$, you are $Template_Age$ years old...";
            Console.Write("Enter name for Template value: ");
            object baseobject = new Template() { Name = Console.ReadLine(), Age = 24 };
            ScriptEngine builder = new ScriptEngine(template, baseobject);

            Console.WriteLine(builder.Render());

            Console.ReadKey();
        }

        public class Template
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }

        List<Type> DataTypes()
        {
            Assembly syslib = typeof(string).Assembly;

            Type[] types = syslib.GetTypes()
                .Where(x => x.Namespace == "System")
                .Where(x => x.IsPrimitive || (new string[] { "String", "Object" }).Contains(x.Name))
                .ToArray();

            return types.ToList();
        }
    }
}
