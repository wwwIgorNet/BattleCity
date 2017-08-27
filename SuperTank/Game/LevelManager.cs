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
            Game.Enemy.Clear();
            Scene.Clear();
            string[] linesTileMap = File.ReadAllLines(ConfigurationGame.Maps + level);

            List<Unit> objGame = GetStaticObjGame(linesTileMap);
            objGame.Add(FactoryUnit.CreateEagle(ConfigurationGame.PositionEagle.X,
                ConfigurationGame.PositionEagle.Y, soundGame));
            Scene.AddRange(objGame);

            LoadEnemyTank(linesTileMap[26], Game.Enemy);

            StartLevel();
        }

        private void StartLevel()
        {
            Game.Plaeyr.Start();
            Game.Enemy.Start();
        }

        private void LoadEnemyTank(string data, Enemy enemy)
        {
            foreach (char c in data)
            {
                switch (c)
                {
                    case 'P':
                        enemy.AddTypeTank(TypeUnit.PlainTank);
                        break;
                    case 'A':
                        enemy.AddTypeTank(TypeUnit.ArmoredPersonnelCarrierTank);
                        break;
                    case 'R':
                        enemy.AddTypeTank(TypeUnit.QuickFireTank);
                        break;
                    case 'B':
                        enemy.AddTypeTank(TypeUnit.ArmoredTank);
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
