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

        static Game()
        {
            timer.Interval = ConfigurationGame.TimerInterval;
            timer.Elapsed += Timer_Elapsed;
        }

        public Game(IRender render, ISoundGame soundGame, IKeyboard keyboard)
        {
            Game.Keyboard = keyboard;
            Game.soundGame = soundGame;
            Scene.Render = render;
            levelManager = new LevelManager(soundGame);
        }
        

        public static IKeyboard Keyboard { get; set; }
        public static ISoundGame SoundGame { get { return soundGame; } }
        public static List<IUpdatable> Updatable { get { return updatable; } }
        public static Random Random { get { return random; } }

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
