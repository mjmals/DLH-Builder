using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DLHBuilder
{
    public static class DataArtifactNameViolation
    {
        static readonly string[] ViolationPattern = { " ", "?", ":", "#", @"\", "/", @"\(", @"\)" };

        public static bool IsViolation(string text)
        {
            if(text.Select(x => new string(x, 1)).Where(x => ViolationPattern.Contains<string>(x)).Count() > 0)
            {
                return true;
            }

            return false;
        }

        public static string RemoveViolations(string text)
        {
            string pattern = string.Format("[{0}]", string.Join(string.Empty, ViolationPattern));
            Regex searchPattern = new Regex(pattern);
            return searchPattern.Replace(text, "_");
        }
    }
}
