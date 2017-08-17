using SuperTank.Audio;
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
        private ISoundGame soundGame;

        public LevelManager(ISoundGame soundGame)
        {
            this.soundGame = soundGame;
        }

        public void CreateLevel(int level)
        {
            Scene.Clear();
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
                            objGame.Add(new Unit(x, y, TypeUnit.BrickWall));
                            break;
                        case '@':
                            objGame.Add(new Unit(x, y, TypeUnit.ConcreteWall));
                            break;
                        case '~':
                            objGame.Add(new Unit(x, y, TypeUnit.Water));
                            break;
                        case '%':
                            objGame.Add(new Unit(x, y, TypeUnit.Forest));
                            break;
                        case '-':
                            objGame.Add(new Unit(x, y, TypeUnit.Ice));
                            break;
                    }
                    x += ConfigurationGame.WidthTile;
                }
                x = 0;
                y += ConfigurationGame.HeightTile;
            }

            objGame.Add(new Unit(ConfigurationGame.PositionEagle.X,
                ConfigurationGame.PositionEagle.Y,
                ConfigurationGame.WidthTile * 2,
                ConfigurationGame.HeightTile * 2,
                TypeUnit.Eagle));

            Scene.AddRange(objGame);


            IDriver plaeyrDriver = new PlaeyrDriver();
            plaeyrDriver.Tank = new TankPlaetr(ConfigurationGame.StartPositionTankPlaeyr.X, ConfigurationGame.StartPositionTankPlaeyr.Y
                    , TypeUnit.PainTank, ConfigurationGame.VelostyPlainTank, Direction.Up, plaeyrDriver, ConfigurationGame.VelostyShellPlainTank, TypeUnit.Shell, soundGame);

            IDriver enemyDriver = new EnemyDriver();
            enemyDriver.Tank = new Tank(0, 0, TypeUnit.PainTank, ConfigurationGame.VelostyPlainTank, Direction.Up, enemyDriver, TypeUnit.Shell, ConfigurationGame.VelostyShellPlainTank);
            enemyDriver.Tank.Direction = Direction.Down;

            Star starPlaeyr= new Star(TypeUnit.Star, plaeyrDriver.Tank);
            Star starEnemy = new Star(TypeUnit.Star, enemyDriver.Tank);

            starPlaeyr.Start();
            starEnemy.Start();
        }
    }
}
