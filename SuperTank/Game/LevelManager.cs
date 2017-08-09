using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class LevelManager
    {
        private readonly IScene scene;
        private readonly IFactoryUnit factoryUnit;
        private readonly IPlaeyr plaeyr;

        public LevelManager(IScene scene, IFactoryUnit factoryUnit, IPlaeyr plaeyr)
        {
            this.scene = scene;
            this.factoryUnit = factoryUnit;
            this.plaeyr = plaeyr;
        }

        public void CreateLevel(int level)
        {
            scene.Clear();

            string[] linesTileMap = File.ReadAllLines(ConfigurationGame.Maps + level);
            int x = 0, y = 0;
            foreach (string line in linesTileMap)
            {
                foreach (char c in line)
                {
                    switch (c)
                    {
                        case '#':
                            scene.Add(factoryUnit.Create(x, y, TypeUnit.BrickWall));
                            break;
                        case '@':
                            scene.Add(factoryUnit.Create(x, y, TypeUnit.ConcreteWall));
                            break;
                        case '~':
                            scene.Add(factoryUnit.Create(x, y, TypeUnit.Water));
                            break;
                        case '%':
                            scene.Add(factoryUnit.Create(x, y, TypeUnit.Forest));
                            break;
                        case '-':
                            scene.Add(factoryUnit.Create(x, y, TypeUnit.Ice));
                            break;
                    }
                    x += ConfigurationGame.WidthTile;
                }
                x = 0;
                y += ConfigurationGame.HeightTile;
            }
            scene.Add(factoryUnit.Create(12 * ConfigurationGame.WidthTile, ConfigurationGame.HeightBoard - ConfigurationGame.HeightTile * 2, TypeUnit.Eagle));

            plaeyr.Unit.X = 9 * ConfigurationGame.WidthTile;
            plaeyr.Unit.Y = ConfigurationGame.HeightBoard - ConfigurationGame.HeigthTank;
            scene.Add(plaeyr.Unit);
        }
    }
}
