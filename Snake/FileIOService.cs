using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class FileIOService
    {
        //путь к json
        private readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }

        //читаем json
        public List<Player> LoadData()
        {
            if (!File.Exists(PATH))
            {
                File.CreateText(PATH).Dispose();
            }

            List<Player> players = new List<Player>();

            using (StreamReader reader = File.OpenText(PATH))
            {
                while (reader.Peek() != -1)
                {
                    players.Add(JsonConvert.DeserializeObject<Player>(reader.ReadLine()));
                }

                reader.Close();
            }

            return players;
        }

        //сохраняем json
        public void SaveData(Player player)
        {
            if (!File.Exists(PATH))
            {
                File.CreateText(PATH).Dispose();
            }

            using (StreamWriter writer = File.AppendText(PATH))
            {
                string serialize = JsonConvert.SerializeObject(player);

                writer.WriteLine(serialize);
            }
        }
    }
}
