using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linearAPI.Repo.Database
{
    internal class LinearFileHandler
    {
       // private readonly object Writelock = new();
        const string EXTENSION = ".json";
        string directoryPath = "./Repo/Database/Data/";

        public LinearFileHandler(string? directoryPathArg)
        {
            if (directoryPathArg != null)
            {
                directoryPath += directoryPathArg;
            }
            else
            {
                directoryPath += "Test/";
            }

            if (!System.IO.Directory.Exists(directoryPath))
            {
                Console.WriteLine("Created dir");
                System.IO.Directory.CreateDirectory(directoryPath);
            }
        }

        public string? ReadAsString(string entityName)
        {
            var path = $"{directoryPath}{entityName}{EXTENSION}";
            if (entityName == null) throw new Exception($"Missing argument: entityName");

            if (!System.IO.File.Exists(path))
            {
                return null;
            }

            string content = File.ReadAllText(path);
            return content;
        }

        public void Write(string entityName, string serializedData)
        {
            //lock (Writelock)
            //{
                if (entityName == null) throw new Exception($"Missing argument: entityName");
                if (serializedData == null) throw new Exception($"Missing argument: serializedName");

                File.WriteAllText($"{directoryPath}{entityName}{EXTENSION}", serializedData);
           // }
        }
    }
}
