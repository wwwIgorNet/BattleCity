using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SuperTank
{
    public class LevelManager
    {
        private static bool abortUpdate;
        private static Random random = new Random();
        private static readonly Timer timer = new Timer();
        private static readonly List<IUpdatable> updatable = new List<IUpdatable>();

        private IGameInfo gameInfo;
        private ISoundGame soundGame;
        private int curentLevel;
        private Plaeyr plaeyr;
        private Enemy enemy;

        static LevelManager()
        {
            timer.Interval = ConfigurationGame.TimerInterval;
            timer.Elapsed += Timer_Elapsed;
        }

        public LevelManager(ISoundGame soundGame, IGameInfo gameInfo, Plaeyr plaeyr, Enemy enemy)
        {
            this.soundGame = soundGame;
            this.gameInfo = gameInfo;
            this.plaeyr = plaeyr;
            plaeyr.CountTank = 2;
            this.enemy = enemy;
            this.enemy.RemoveAllTank += Enemy_RemoveAllTank;
        }

        private void Enemy_RemoveAllTank()
        {
            if (curentLevel == ConfigurationBase.CountLevel)
                curentLevel = 0;

            System.Threading.ThreadPool.QueueUserWorkItem(s => {
                System.Threading.Thread.Sleep(3000);

                EndLevel();

                CreateLevel(curentLevel + 1);
                gameInfo.StartLevel(curentLevel);
                System.Threading.Thread.Sleep(1000);
                StartLevel();
            });
        }

        public void EndLevel()
        {
            soundGame.TankDispouse();
            timer.Stop();
            abortUpdate = true;
            System.Threading.Thread.Sleep(200);
            Scene.Tanks.Clear();
            updatable.Clear();
            gameInfo.EndLevel(plaeyr.Points, plaeyr.DestroyedTanks);

            System.Threading.Thread.Sleep(3000);
            enemy.Clear();
            Scene.Clear();
        }

        public static List<IUpdatable> Updatable { get { return updatable; } }
        public static Random Random { get { return random; } }


        public void CreateLevel(int level)
        {
            curentLevel = level;
            string[] linesTileMap = File.ReadAllLines(ConfigurationGame.Maps + level);

            List<Unit> objGame = GetStaticObjGame(linesTileMap);
            objGame.Add(FactoryUnit.CreateEagle(ConfigurationGame.PositionEagle.X,
                ConfigurationGame.PositionEagle.Y, soundGame));
            Scene.AddRange(objGame);

            LoadEnemyTank(linesTileMap[26], enemy);
        }

        public void StartLevel()
        {
            Game.GameInfo.StartLevel(curentLevel);
            plaeyr.Start();
            enemy.Start();
            abortUpdate = false;
            timer.Start();
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

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (timer)
            {
                for (int i = 0; i < Updatable.Count; i++)
                {
                    if (abortUpdate) return;
                    Updatable[i].Update();
                }
            }
        }
    }
}
