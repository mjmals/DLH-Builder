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

namespace DLHBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly syslib = typeof(string).Assembly;

            Type[] types = syslib.GetTypes()
                .Where(x => x.Namespace == "System")
                .Where(x => x.IsPrimitive || (new string[] { "String", "Object"}).Contains(x.Name))
                .ToArray();

            foreach(Type type in types.OrderBy(x => x.Name))
            {
                Console.WriteLine(type.Name);
            }

            Console.ReadKey();
        }
    }
}
