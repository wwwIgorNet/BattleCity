using SuperTank;
using SuperTank.Audio;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SuperTank
{
    public class Game : IDisposable
    {
        private static Random random = new Random();
        private static readonly Timer timer = new Timer();
        private static readonly List<IUpdatable> updatable = new List<IUpdatable>();

        private readonly LevelManager levelManager;
        private static ISoundGame soundGame;
        private static IGameInfo gameInfo;

        static Game()
        {
            timer.Interval = ConfigurationGame.TimerInterval;
            timer.Elapsed += Timer_Elapsed;
        }

        public Game(IRender render, ISoundGame soundGame, IKeyboard keyboard, IGameInfo gameInfo)
        {
            Game.gameInfo = gameInfo;
            Game.Plaeyr = new Plaeyr(soundGame, Owner.Plaeyr);
            Game.Plaeyr.CountTank = 3;
            Game.Enemy = new Enemy();
            Game.Keyboard = keyboard;
            Game.soundGame = soundGame;
            Scene.Render = render;
            levelManager = new LevelManager(soundGame);
        }

        public static Plaeyr Plaeyr { get; set; }
        public static Enemy Enemy { get; set; }
        public static IKeyboard Keyboard { get; set; }
        public static List<IUpdatable> Updatable { get { return updatable; } }
        public static Random Random { get { return random; } }
        public static IGameInfo GameInfo { get { return gameInfo; } }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (timer)
            {
                for (int i = 0; i < Updatable.Count; i++)
                {
                    Updatable[i].Update();
                }
            }
        }

        public void Start()
        {
            levelManager.CreateLevel(1);
            timer.Start();
        }

        public void Dispose()
        {
            timer.Stop();
        }
    }
}
