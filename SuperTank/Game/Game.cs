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
        private static readonly Timer timer = new Timer();
        private static readonly List<IUpdatable> updatable = new List<IUpdatable>();

        private static readonly IDriver plaeyr = new PlaeyrDriver();
        private readonly LevelManager levelManager;
        private static ISoundGame soundGame;

        static Game()
        {
            timer.Interval = ConfigurationGame.TimerInterval;
            timer.Elapsed += Timer_Elapsed;
        }

        public Game(IRender render, ISoundGame soundGame)
        {
            Game.soundGame = soundGame;
            IScene scene = new Scene(render);
            levelManager = new LevelManager(scene, plaeyr, soundGame);
        }
        
        public static ISoundGame SoundGame { get { return soundGame; } }
        public static List<IUpdatable> Updatable { get { return updatable; } }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < Updatable.Count; i++)
            {
                Updatable[i].Update();
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
