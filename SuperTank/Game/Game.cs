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
        private readonly IFactoryUnit factoryUnit;
        private readonly LevelManager levelManager;
        public readonly IRender render;
        public readonly IPlaeyr plaeyr;
        public readonly IScene scene;
        private readonly Timer timer;

        public Game(IRender render)
        {
            timer = new Timer();
            timer.Interval = ConfigurationGme.TimerInterval;
            timer.Elapsed += Timer_Elapsed;

            this.render = render;
            scene = new Scene();
            factoryUnit = new FactoryUnit(scene);
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
