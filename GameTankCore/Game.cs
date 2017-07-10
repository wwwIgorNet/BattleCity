using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GameTankCore
{
    public class Game
    {
        private Timer timer;

        public Game()
        {
            Initialize();
        }

        private void Initialize()
        {
            Level.CreateLevel();
            timer = new Timer();
            timer.Interval = Configuration.TimerInterval;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (timer)
            {
                Update();
            }
        }

        private void Update()
        {
            Level.Update();
        }

        public void Start()
        {
            timer.Start();
        }
    }
}
