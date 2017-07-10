using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    public static class Level
    {
        private static readonly ObservableCollection<IUpdatable> updateObjects = new ObservableCollection<IUpdatable>();


        public static ObservableCollection<IUpdatable> UpdateObjects { get { return updateObjects; } }

        public static void CreateLevel()
        {
            string[] linesTileMap = File.ReadAllLines(Configuration.Maps + 1);
            int x = 0, y = 0;
            foreach (string line in linesTileMap)
            {
                foreach (char c in line)
                {
                    switch (c)
                    {
                        case '#':
                            Factory.CreateBrickWall(x, y);
                            break;
                    }
                    x += Configuration.WidthTile;
                }
                x = 0;
                y += Configuration.HeightTile;
            }
            Factory.CreateTank(TypeObjGame.PlainUserTank);
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
