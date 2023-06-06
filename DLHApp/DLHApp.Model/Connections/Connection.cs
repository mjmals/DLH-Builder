using System.Reflection;

namespace DLHApp.Model.Connections
{
    public abstract class Connection : ModelItem, IModelItem
    {
        public override string BasePath => "Connections";

        public static Connection Load(string fileName)
        {
            foreach (Type connectionType in typeof(Connection).Assembly.GetTypes().Where(x => x.IsAbstract == false && x.IsAssignableTo(typeof(Connection))))
            {
                Connection conn = (Connection)Activator.CreateInstance(connectionType);

                if(fileName.EndsWith(conn.OutputExtension))
                {
                    return (Connection)conn.GetType().GetMethod("Load").Invoke(null, new string[] { fileName });
                }
            }

            return null;
        }

        public static string[] GetConnectionTypeExtensions()
        {
            List<string> output = new List<string>();

            foreach (Type connectionType in typeof(Connection).Assembly.GetTypes().Where(x => x.IsAbstract == false && x.IsAssignableTo(typeof(Connection))))
            {
                Connection conn = (Connection)Activator.CreateInstance(connectionType);
                output.Add(conn.OutputExtension);
            }
            
            return output.ToArray();
        }

    }
}