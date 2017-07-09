using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    public static class Level
    {
        private static readonly List<IUpdatable> updateObjects = new List<IUpdatable>();


        public static List<IUpdatable> UpdateObjects { get { return updateObjects; } }

        public  static void CreateLevel()
        {
            Faktory.CreateTank("plain user");
        }

        public static void Update()
        {
            for (int i = 0; i < updateObjects.Count; i++)
                updateObjects[i].Update();
        }

        public static void Clear()
        {
            updateObjects.Clear();
        }
    }
}
