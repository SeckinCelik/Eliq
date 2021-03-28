using Eliq.BL.Models;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Eliq.BL
{
    public class FileManager
    {
        private readonly string filePath = WebConfigurationManager.AppSettings["filePath"];
       
        public IEnumerable<T> ReadFile<T>()
        {
            using (StreamReader r = new StreamReader(filePath + typeof(T).Name + ".json"))
            {
                string json = r.ReadToEnd();

                List<T> items = JsonConvert.DeserializeObject<List<T>>(json);

                return items;
            }
        }
        public bool AppendToFile<T>(T obj) where T : BaseModel
        {
            try
            {
                string fullFilePath = filePath + typeof(T).Name + ".json";

                var firstState = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(fullFilePath));
                firstState.Add(obj);

                File.WriteAllText(fullFilePath, JsonConvert.SerializeObject(firstState));
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteFromFile<T>(Guid playerid) where T : BaseModel
        {
            try
            {
                string fullFilePath = filePath + typeof(T).Name + ".json";

                var firstState = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(fullFilePath));

                if (firstState.Any(x => x.id == playerid))
                {
                    File.WriteAllText(fullFilePath, JsonConvert.SerializeObject(firstState.Where(x => x.id != playerid)));
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
