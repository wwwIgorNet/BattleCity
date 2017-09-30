using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.WindowsForms
{
    class LevelInfoTwoPlayer : LevelInfo
    {
        private Point pointCountTankIIPlaeyr = new Point(0, ConfigurationView.HeightTile * 18 + 8);
        private int countTankIIPlaeyr;

        protected override Image DashboardInfo { get { return Images.DashboardInfoIIPlayer; } }

        protected override void UpdateImage()
        {
            base.UpdateImage();
            Graphics g = Graphics.FromImage(ImgInfo);
            g.DrawString(countTankIIPlaeyr.ToString(), Font, Brushes.Black, pointCountTankIIPlaeyr.X + ConfigurationView.WidthTile, pointCountTankIIPlaeyr.Y + ConfigurationView.HeightTile);
        }

        public override void SetCountTankPlaeyr(int count, Owner owner)
        {
            if(Owner.IIPlayer == owner)
            {
                countTankIIPlaeyr = count;
            }
            base.SetCountTankPlaeyr(count, owner);
        }
    }
}
