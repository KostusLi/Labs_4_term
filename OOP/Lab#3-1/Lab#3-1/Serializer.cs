using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab_3_1
{
    public static class Serializer
    {
        public static void SaveJson(List<Computer> computers, string path)
        {
            string json = JsonSerializer.Serialize(computers,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(path, json);
        }

        public static void SaveXml(List<Computer> computers, string path)
        {
            XmlSerializer serializer = new(typeof(List<Computer>));

            using FileStream fs = new(path, FileMode.Create);
            serializer.Serialize(fs, computers);
        }
    }
}
