using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBattleCity.Lib;
using SuperTank;
using SuperTankWPF.Model;

namespace SuperTankWPF.Units
{
    class BlickWall : UnitView
    {
        private IImageSourceStor imageSourceStor;

        public BlickWall(IImageSourceStor imageSourceStor)
        {
            this.imageSourceStor = imageSourceStor;
        }

        public TypeBlickWall TypeBlickWall
        {
            set
            {
                switch (value)
                {
                    case TypeBlickWall.Whole:
                        Source = imageSourceStor.BrickWall;
                        break;
                    case TypeBlickWall.Top:
                        Source = imageSourceStor.BlickWallTop;
                        break;
                    case TypeBlickWall.Bottom:
                        Source = imageSourceStor.BlickWallBottom;
                        break;
                    case TypeBlickWall.Left:
                        Source = imageSourceStor.BlickWallLeft;
                        break;
                    case TypeBlickWall.Right:
                        Source = imageSourceStor.BlickWallRight;
                        break;
                    case TypeBlickWall.TopLeft:
                        Source = imageSourceStor.BlickWallTopLeft;
                        break;
                    case TypeBlickWall.TopRight:
                        Source = imageSourceStor.BlickWallTopRight;
                        break;
                    case TypeBlickWall.BottomLeft:
                        Source = imageSourceStor.BlickWallBottomLeft;
                        break;
                    case TypeBlickWall.BootomRight:
                        Source = imageSourceStor.BlickWallBottomRight;
                        break;
                }
            }
        }

        public override void Update(PropertiesType prop, object value)
        {
            if (prop == PropertiesType.TypeBlickWall)
                TypeBlickWall = (TypeBlickWall)value;
            else
                base.Update(prop, value);
        }
    }
}
