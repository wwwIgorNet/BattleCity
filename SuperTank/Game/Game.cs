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
        public readonly IPlaeyr plaeyr;
        private readonly Timer timer = new Timer();
        private readonly LevelManager levelManager;

        public Game(IRender render)
        {
            timer.Interval = ConfigurationGme.TimerInterval;
            timer.Elapsed += Timer_Elapsed;

            IScene scene = new Scene();
            IFactoryUnit factoryUnit = new FactoryUnit(scene);
            plaeyr = new Plaeyr(factoryUnit.Create(0, 0, TypeUnit.PlainTank));
            levelManager = new LevelManager(scene, factoryUnit, plaeyr);
            scene.SceneChenges += render.SceneChangedHendler;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            plaeyr.Update();
        }

        public void Start()
        {
            levelManager.CreateLevel(1);
            timer.Start();
        }
    }
}
