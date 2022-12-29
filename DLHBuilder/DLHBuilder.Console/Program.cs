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
using DLHBuilder.Config;
using DLHBuilder.Build;

namespace DLHBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "This is a: TEST?";

            Console.WriteLine(text);

            bool isViolation = DataArtifactNameViolation.IsViolation(text);

            if(isViolation)
            {
                Console.WriteLine(DataArtifactNameViolation.RemoveViolations(text));
            }

            Console.Read();
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
