using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMap.dat
{
    internal class Program
    {
        public struct MapInfo
        {
            public uint Identity;
            public string Path;
            public uint Size;
        }
        public static Dictionary<uint, MapInfo> Maps;
        public static string Destination = Environment.CurrentDirectory + "\\GameMap.dat";
        static void Main(string[] args)
        {
            if (File.Exists(Destination))
            {
                File.Delete(Destination);
                Console.WriteLine("Old GameMap.dat Deleted");
            }
            if (File.Exists(Environment.CurrentDirectory + "\\Maps.txt"))
            {
                Maps = new Dictionary<uint, MapInfo>();
                foreach (var item in File.ReadAllLines(Environment.CurrentDirectory + "\\Maps.txt"))
                {

                    var data = item.Split(new[] { ' ' }, 3);
                    if (data.Length == 3)
                    {
                        MapInfo value = default(MapInfo);
                        value.Identity = uint.Parse(data[0]);
                        value.Path = data[1];
                        value.Size = uint.Parse(data[2]);
                        Maps.Add(value.Identity, value);
                        Console.WriteLine($"{item}");
                    }
                    else
                        Console.WriteLine($"Error in line : {item}");
                }
                FileStream fileStream = new FileStream(Destination, FileMode.OpenOrCreate);
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                binaryWriter.Write(Maps.Count);
                foreach (MapInfo mapInfo in Maps.Values)
                {
                    binaryWriter.Write(mapInfo.Identity);
                    binaryWriter.Write(mapInfo.Path.Length);
                    foreach (char ch in mapInfo.Path)
                    {
                        binaryWriter.Write(ch);
                    }
                    binaryWriter.Write(mapInfo.Size);
                }
                fileStream.Close();
                binaryWriter.Close();
                fileStream.Dispose();
                binaryWriter.Dispose();
            }
            else
                Console.WriteLine("Maps.txt not found");

            Console.WriteLine("New GameMap has been created");
            Console.ReadKey();
        }
    }
}
