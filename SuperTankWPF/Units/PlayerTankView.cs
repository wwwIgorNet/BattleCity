using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SuperTank;
using System.Windows;
using System.Windows.Media.Imaging;
using SuperTankWPF.Model;
using Microsoft.Practices.ServiceLocation;
using SuperTankWPF.ViewModel;

namespace SuperTankWPF.Units
{
    class PlayerTankView : TankView, IInvulnerable
    {
        private ImageSource[] invulnerable;
        private AnimationView animationView;
        private bool isInvulnerable;

        public PlayerTankView(Direction direction, Dictionary<Direction, ImageSource[]> imgSources, ImageSource[] invulnerable, int updateInterval) : base(direction, imgSources, updateInterval)
        {
            this.invulnerable = invulnerable;
        }
        
        public bool IsInvulnerable
        {
            get { return isInvulnerable; }
            set
            {
                isInvulnerable = value;
                if (isInvulnerable)
                {
                    var av = new AnimationView(1, invulnerable, true);
                    av.X = X;
                    av.Y = Y;
                    av.ZIndex = 15;
                    animationView = av;

                    ServiceLocator.Current.GetInstance<ScrenSceneViewModel>().Units.Add(animationView);
                }
                else
                {
                    ServiceLocator.Current.GetInstance<ScrenSceneViewModel>().Units.Remove(animationView);
                }
            }
        }
    }
}
