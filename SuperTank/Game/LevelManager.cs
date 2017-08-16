using SuperTank.Audio;
using SuperTank.Updatable;
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
        private IPlaeyr plaeyr;
        private ISoundGame soundGame;

        public LevelManager(IScene scene, IFactoryUnit factoryUnit, IPlaeyr plaeyr, ISoundGame soundGame)
        {
            this.scene = scene;
            this.factoryUnit = factoryUnit;
            this.plaeyr = plaeyr;
            this.soundGame = soundGame;
        }

        public void CreateLevel(int level)
        {
            scene.Clear();
            List<Unit> objGame = new List<Unit>();
            string[] linesTileMap = File.ReadAllLines(ConfigurationGame.Maps + level);
            int x = 0, y = 0;
            foreach (string line in linesTileMap)
            {
                foreach (char c in line)
                {
                    switch (c)
                    {
                        case '#':
                            objGame.Add(factoryUnit.Create(x, y, TypeUnit.BrickWall));
                            break;
                        case '@':
                            objGame.Add(factoryUnit.Create(x, y, TypeUnit.ConcreteWall));
                            break;
                        case '~':
                            objGame.Add(factoryUnit.Create(x, y, TypeUnit.Water));
                            break;
                        case '%':
                            objGame.Add(factoryUnit.Create(x, y, TypeUnit.Forest));
                            break;
                        case '-':
                            objGame.Add(factoryUnit.Create(x, y, TypeUnit.Ice));
                            break;
                    }
                    x += ConfigurationGame.WidthTile;
                }
                x = 0;
                y += ConfigurationGame.HeightTile;
            }

            objGame.Add(factoryUnit.Create(12 * ConfigurationGame.WidthTile, ConfigurationGame.HeightBoard - ConfigurationGame.HeightTile * 2, TypeUnit.Eagle));

            scene.AddRange(objGame);

            AddPlayerTank(plaeyr);
        }

        public void AddPlayerTank(IPlaeyr plaeyr)
        {
            plaeyr.Tank = new TankPlaetr(Unit.NextID, 9 * ConfigurationGame.WidthTile,
                ConfigurationGame.HeightBoard - ConfigurationGame.HeigthTank, ConfigurationGame.WidthTile * 2, ConfigurationGame.HeightTile * 2 , TypeUnit.PainTank, scene, factoryUnit, ConfigurationGame.VelostyPlainTank, Direction.Up, ConfigurationGame.VelostyShellPlainTank, soundGame);
            Star star = new Star(Unit.NextID, plaeyr.Tank.X, plaeyr.Tank.Y, plaeyr.Tank.Width, plaeyr.Tank.Height, TypeUnit.Star, plaeyr, scene);
            star.Start();
        }
    }
}
