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
            Environment environment = new Environment()
            {
                TenantID = Guid.NewGuid(),
                SubscriptionID = Guid.NewGuid(),
                ResourceGroup = "MyResourceGroup"
            };

            ScriptEngine builder = new ScriptEngine("Terraform.Variables.VariableData", environment);

            Console.WriteLine(builder.Render());

            Console.ReadKey();
        }

        public class Template
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }

        public class Environment
        {
            public Guid TenantID { get; set; }

            public Guid SubscriptionID { get; set; }

            public string ResourceGroup { get; set; }
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
