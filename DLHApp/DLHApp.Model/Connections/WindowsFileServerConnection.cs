using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DLHApp.Model.Connections
{
    public class WindowsFileServerConnection : Connection, IModelItem
    {
        public override string? Name => !string.IsNullOrEmpty(base.Name) ? base.Name : string.Format("{0}_{1}", FileServerName, ShareFolderPath.Replace(@"\", "_").Replace(" ", "_"));

        public string FileServerName { get; set; }

        public string ShareFolderPath { get; set; }
        
        public string Username { get; set; }

        public string FullServerPath
        {
            get
            {
                return string.Format(@"\\{0}\{1}", FileServerName, ShareFolderPath);
            }
            set
            {
                int serverStartPos = value.IndexOf(@"\\") + 2;
                int serverEndPos = value.IndexOf(@"\", serverStartPos);
                FileServerName = value.Substring(serverStartPos, serverEndPos - 2);
                ShareFolderPath = value.Substring(serverEndPos + 1);
            }
        }

        public override string OutputExtension => "wfscon.json";

        public static WindowsFileServerConnection New()
        {
            return new WindowsFileServerConnection();
        }

        public static WindowsFileServerConnection Load(string filePath)
        {
            WindowsFileServerConnection output = JsonConvert.DeserializeObject<WindowsFileServerConnection>(File.ReadAllText(filePath));
            output.Name = Path.GetFileName(filePath).Replace(".wfscon.json", "");
            return output;
        }
    }
}
