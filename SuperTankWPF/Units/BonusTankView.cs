using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SuperTank;
using SuperTankWPF.Model;
using System.Timers;

namespace SuperTankWPF.Units
{
    class BonusTankView : TankView
    {
        private Timer timer = new Timer(ConfigurationWPF.TimerInterval);
        private Dictionary<Direction, ImageSource[]> imgGray;
        private Dictionary<Direction, ImageSource[]> imgRed;
        private int iteration = 0;
        private Action chengRed;
        private Action chengGrey;

        public BonusTankView(Direction direction, Dictionary<Direction, ImageSource[]> imgGray, Dictionary<Direction, ImageSource[]> imgRed, int updateInterval) : base(direction, imgGray, updateInterval)
        {
            this.imgGray = imgGray;
            this.imgRed = imgRed;

            chengGrey = new Action(SetImgTankGray);
            chengRed = new Action(SetImgTankRed);

            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void SetImgTankRed()
        {
            base.ImagesWithDirection = imgRed;
        }
        private void SetImgTankGray()
        {
            base.ImagesWithDirection = imgGray;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (iteration == ConfigurationWPF.DelayChangColorBonusTank)
            {
                this.Dispatcher.BeginInvoke(chengRed);
            }
            else if (iteration == ConfigurationWPF.DelayChangColorBonusTank * 2)
            {
                this.Dispatcher.BeginInvoke(chengGrey);
                iteration = 0;
            }
            iteration++;
        }
    }
}
