using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class ADLSDataStageParameter
    {
        public ADLSDataStageParameter(string name)
        {
            Name = name;
        }

        public string Name 
        { 
            get => name;
            set
            {
                string _name = value;

                if (_name.Contains("$("))
                {
                    int startpos = _name.IndexOf("$(");
                    int endpos = _name.IndexOf(")", startpos) - startpos + 1;

                    _name = _name.Substring(startpos, endpos);
                }

                name = _name;
            }
        }

        private string name { get; set; }
    }
}
