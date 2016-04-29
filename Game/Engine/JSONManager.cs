using System.Collections.Generic;
using System.IO;

namespace MarioGame.Engine
{  
    public static class JSONManager
    {
        static JSONManager()
        {
        }

        public static List<Map> getMaps()
        {
            List<Map> mapList = new List<Map>();
            DirectoryInfo contentDirectory =
                new DirectoryInfo(@"maps/"); //get the maps folder
            FileInfo[] files = contentDirectory.GetFiles("*.map"); //get all .map files in the directory
            //incase of major compability breaks - we'll need to create seperate handlers for .map, .map2 etc etc.
            foreach (FileInfo file in files)
            {
            //    FileStream fileStream = file.OpenRead();
          //      string jsonMap = fileStream.ToString();
            StreamReader reader = new StreamReader(@contentDirectory + file.ToString());
            string jsonMap = reader.ReadToEnd();
            mapList.Add(JsonConvert.DeserializeObject<Map>(jsonMap));
                    //add them to a list so we can convert from and to them without worrying about future problems (can just hardcode any major changes there...)
            }
            foreach (Map map in mapList)
            {
                map.StringsToTextures();
            }
            return mapList;
        }

        public static void saveMap(Map map)
        {
            string newMap = JsonConvert.SerializeObject(map);
            System.IO.File.WriteAllText(@"maps/" + map.MapId + ".map", newMap);

        }
    }
}
