using GameBattleCity.Lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SuperTank.View.Images;

namespace SuperTank.View
{
    class BlickWallVeiw : ViewUnit, IBlickWallVeiw
    {
        private ImageBlickWall imagesBlickWall;

        public BlickWallVeiw(int id, float x, float y, float width, float height, int zIndex, Image img, ImageBlickWall imagesBlickWall)
            : base(id, x, y, width, height, zIndex, img)
        {
            this.imagesBlickWall = imagesBlickWall;
            this.TypeBlickWall = TypeBlickWall.Whole;
        }

        public TypeBlickWall TypeBlickWall
        {
            get { return (TypeBlickWall)Properties[PropertiesType.TypeBlickWall]; }
            set
            {
                switch (value)
                {
                    case TypeBlickWall.Top:
                        SetImg(imagesBlickWall.BlickWallTop);
                        Height /= 2;
                        break;
                    case TypeBlickWall.Bottom:
                        SetImg(imagesBlickWall.BlickWallBottom);
                        Height /= 2;
                        break;
                    case TypeBlickWall.Left:
                        SetImg(imagesBlickWall.BlickWallLeft);
                        Width /= 2;
                        break;
                    case TypeBlickWall.Right:
                        SetImg(imagesBlickWall.BlickWallRight);
                        Width /= 2;
                        break;
                    case TypeBlickWall.TopLeft:
                        SetImg(imagesBlickWall.BlickWallTopLeft);
                        Height = Img.Height;
                        Width = Img.Width;
                        break;
                    case TypeBlickWall.TopRight:
                        SetImg(imagesBlickWall.BlickWallTopRight);
                        Height = Img.Height;
                        Width = Img.Width;
                        break;
                    case TypeBlickWall.BottomLeft:
                        SetImg(imagesBlickWall.BlickWallBottomLeft);
                        Height = Img.Height;
                        Width = Img.Width;
                        break;
                    case TypeBlickWall.BootomRight:
                        SetImg(imagesBlickWall.BlickWallBottomRight);
                        Height = Img.Height;
                        Width = Img.Width;
                        break;
                    default:
                        SetImg(imagesBlickWall.BrickWall);
                        break;
                }
            }
        }
    }
}
