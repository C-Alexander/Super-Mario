using System;
using System.Collections.Generic;

namespace MarioGame.Engine
{
    public static class MapManager
    {
        private static List<Map> mapList;
        private static Random rand;
    //    private static List<List<Map>> difficultiesList;
        static MapManager()
        {
       //     for (int i = 0; i < GameSettings.DifficultyLevels; i++)
       //     {
       //         difficultiesList = new List<List<Map>>();
      //          difficultiesList.Add(new List<Map>());
      //      }
            mapList = JSONManager.getMaps();
            rand = new Random();
        }

        public static Map getRandomMap()
        {
            return mapList[rand.Next(0, mapList.Count)];
        }
    }
}
