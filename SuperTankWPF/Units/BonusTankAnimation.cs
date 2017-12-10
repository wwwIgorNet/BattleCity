using SuperTankWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;

namespace SuperTankWPF.Units
{
    class BonusTankAnimation : IDisposable
    {
        private Dispatcher dispatcher;
        private Timer timer = new Timer(ConfigurationWPF.TimerInterval);
        private int iteration = 0;

        public BonusTankAnimation(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;

            timer.Elapsed += Timer_Elapsed;
        }

        public event Action SetRedTank;
        public event Action SetCurentTank;

        public void Start()
        {
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (iteration == ConfigurationWPF.DelayChangColorBonusTank)
            {
                dispatcher.BeginInvoke(SetRedTank);
            }
            else if (iteration == ConfigurationWPF.DelayChangColorBonusTank * 2)
            {
                dispatcher.BeginInvoke(SetCurentTank);
                iteration = 0;
            }
            iteration++;
        }

        public void Dispose()
        {
            timer.Dispose();
        }
    }
}
