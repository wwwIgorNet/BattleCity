using SuperTank;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SuperTank
{
    public class Game
    {
        private static readonly Timer timer = new Timer();
        private static readonly List<IUpdatable> updatable = new List<IUpdatable>();

        public readonly IPlaeyr plaeyr;
        private readonly LevelManager levelManager;

        static Game()
        {
            timer.Interval = ConfigurationGame.TimerInterval;
            timer.Elapsed += Timer_Elapsed;
        }

        public Game(IRender render)
        {
            IScene scene = new Scene();
            scene.SceneChenges += render.SceneChangedHendler;
            IFactoryUnit factoryUnit = new FactoryUnit(scene);
            plaeyr = new Plaeyr(factoryUnit.Create(0, 0, TypeUnit.PlainTank));
            Updatable.Add(plaeyr);
            levelManager = new LevelManager(scene, factoryUnit, plaeyr);
        }

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
    }
}
