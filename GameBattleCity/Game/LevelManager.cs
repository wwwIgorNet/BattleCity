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
        private DateTime startGameOver;
        private static bool abortUpdate;
        private static Random random = new Random();
        private static readonly Timer timer = new Timer();
        private static readonly List<IUpdatable> updatable = new List<IUpdatable>();

        private IGameInfo gameInfo;
        private ISoundGame soundGame;
        private int curentLevel;
        private Player plaeyr;
        private Enemy enemy;

        static LevelManager()
        {
            timer.Interval = ConfigurationGame.TimerInterval;
            timer.Elapsed += Timer_Elapsed;
        }

        public LevelManager(ISoundGame soundGame, IGameInfo gameInfo, Player plaeyr, Enemy enemy)
        {
            abortUpdate = false;
            curentLevel = 0;
            this.soundGame = soundGame;
            this.gameInfo = gameInfo;
            plaeyr.PlayerGameOver += GameOver;
            this.plaeyr = plaeyr;
            plaeyr.CountTank = 2;
            this.enemy = enemy;
            this.enemy.RemoveAllTank += Enemy_RemoveAllTank;
        }

        public void EagleDelited()
        {
            plaeyr.EagleDestoed();
            GameOver();
        }

        public void GameOver()
        {
            startGameOver = DateTime.Now;
            gameInfo.GameOver();
            Timer t = new Timer(ConfigurationBase.TimeGameOver);
            t.Elapsed += (s, a) => {
                abortUpdate = true;
                Stop();
                Updatable.Clear();
                System.Threading.Thread.Sleep(100);
                enemy.Clear();
                plaeyr.Clear();
                plaeyr.CountTank = 0;
                gameInfo.SetCountTankEnemy(0);
                Scene.Clear();
                curentLevel = 0;
                t.Stop();
            };
            t.Start();
        }

        private void Enemy_RemoveAllTank()
        {
            if (curentLevel == ConfigurationBase.CountLevel)
                curentLevel = 0;

            System.Threading.ThreadPool.QueueUserWorkItem(s => {
                System.Threading.Thread.Sleep(3000);

                EndLevel();

                StartLevel();
            });
        }

        public void EndLevel()
        {
            soundGame.TankSoundStop();
            gameInfo.EndLevel(curentLevel, plaeyr.Points, plaeyr.DestroyedTanks);
            timer.Stop();
            abortUpdate = true;
            updatable.Clear();
            enemy.Clear();
            plaeyr.Clear();
            System.Threading.Thread.Sleep(200);
            for (int i = Scene.Units.Count - 1; i >= 0; i--)
            {
                switch (Scene.Units[i].Type)
                {
                    case TypeUnit.BrickWall:
                    case TypeUnit.ConcreteWall:
                    case TypeUnit.Water:
                    case TypeUnit.Forest:
                    case TypeUnit.Ice:
                    case TypeUnit.Eagle:
                        break;
                    default:
                        Scene.Remove(Scene.Units[i]);
                        break;
                }
            }
            System.Threading.Thread.Sleep(ConfigurationGame.DelayScrenPoints);// todo
        }

        public static List<IUpdatable> Updatable { get { return updatable; } }
        public static Random Random { get { return random; } }


        public void CreateLevel(int level)
        {
            curentLevel = level;
            string[] linesTileMap = File.ReadAllLines(ConfigurationGame.Maps + level);

            List<Unit> objGame = GetStaticObjGame(linesTileMap);
            Unit eagle = FactoryUnit.CreateEagle(ConfigurationGame.PositionEagle.X,
                ConfigurationGame.PositionEagle.Y, soundGame);
            eagle.UnitDisposable += u => { EagleDelited(); };
            objGame.Add(eagle);
            Scene.AddRange(objGame);

            LoadEnemyTank(linesTileMap[26], enemy);
        }

        public void StartLevel()
        {
            DateTime start = DateTime.Now;
            if (curentLevel == ConfigurationGame.CountLevel) curentLevel = 0;
            gameInfo.StartLevel(curentLevel + 1);
            System.Threading.Thread.Sleep(1000);
            Scene.Clear();
            CreateLevel(curentLevel + 1);
            System.Threading.Thread.Sleep(ConfigurationGame.DelayScrenLoadLevel - (DateTime.Now - start));

            plaeyr.Start();
            enemy.Start();
            abortUpdate = false;
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
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
