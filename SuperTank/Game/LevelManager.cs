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
        private Enemy enemy = new Enemy();
        private ISoundGame soundGame;

        public LevelManager(ISoundGame soundGame)
        {
            this.soundGame = soundGame;
        }

        public void CreateLevel(int level)
        {
            enemy.Clear();
            Scene.Clear();
            string[] linesTileMap = File.ReadAllLines(ConfigurationGame.Maps + level);

            List<Unit> objGame = GetStaticObjGame(linesTileMap);
            objGame.Add(FactoryUnit.CreateUnit(ConfigurationGame.PositionEagle.X,
                ConfigurationGame.PositionEagle.Y,
                ConfigurationGame.WidthTile * 2,
                ConfigurationGame.HeightTile * 2,
                TypeUnit.Eagle));
            Scene.AddRange(objGame);

            LoadEnemyTank(linesTileMap[26], enemy);

            StartLevel();
        }

        private void StartLevel()
        {
            IDriver plaeyrDriver = new PlaeyrDriver(Game.Keyboard);
            plaeyrDriver.Tank = FactoryUnit.CreateTank(ConfigurationGame.StartPositionTankPlaeyr.X, ConfigurationGame.StartPositionTankPlaeyr.Y
                    , TypeUnit.PainTankPlaeyr, Direction.Up, plaeyrDriver, soundGame);

            IDriver enemyDriver = new EnemyDriver();
            enemyDriver.Tank = FactoryUnit.CreateTank(0, 0, TypeUnit.PainTank, Direction.Down, enemyDriver);

            Star starPlaeyr = FactoryUnit.CreateStar(TypeUnit.Star, plaeyrDriver.Tank);
            Star starEnemy = FactoryUnit.CreateStar(TypeUnit.Star, enemyDriver.Tank);

            starPlaeyr.Start();
            starEnemy.Start();
        }

        private void LoadEnemyTank(string data, Enemy enemy)
        {
            foreach (char c in data)
            {
                switch (c)
                {
                    case 'P':
                        enemy.AddTank(TypeUnit.PainTankPlaeyr);
                        break;
                    case 'A':
                        enemy.AddTank(TypeUnit.ArmoredPersonnelCarrierTankPlaeyr);
                        break;
                    case 'R':
                        enemy.AddTank(TypeUnit.QuickFireTankPlaeyr);
                        break;
                    case 'B':
                        enemy.AddTank(TypeUnit.ArmoredTankPlaeyr);
                        break;
                }
            }
        }

        private List<Unit> GetStaticObjGame(string[] linesTileMap)
        {
            List<Unit> objGame = new List<Unit>();
            int x = 0, y = 0;
            foreach (string line in linesTileMap)
            {
                foreach (char c in line)
                {
                    switch (c)
                    {
                        case '#':
                            objGame.Add(FactoryUnit.CreateUnit(x, y, TypeUnit.BrickWall));
                            break;
                        case '@':
                            objGame.Add(FactoryUnit.CreateUnit(x, y, TypeUnit.ConcreteWall));
                            break;
                        case '~':
                            objGame.Add(FactoryUnit.CreateUnit(x, y, TypeUnit.Water));
                            break;
                        case '%':
                            objGame.Add(FactoryUnit.CreateUnit(x, y, TypeUnit.Forest));
                            break;
                        case '-':
                            objGame.Add(FactoryUnit.CreateUnit(x, y, TypeUnit.Ice));
                            break;
                    }
                    x += ConfigurationGame.WidthTile;
                }
                x = 0;
                y += ConfigurationGame.HeightTile;
            }

            return objGame;
        }
    }
}
