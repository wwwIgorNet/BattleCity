using SuperTank.View;
using System.Drawing;

namespace SuperTank.WindowsForms
{
    class LevelInfo
    {
        private Font font = new Font(ConfigurationView.InfoFontFamily, 15);
        private Point pointCountTankPlaeyr = new Point(ConfigurationView.WidthTile - 2, ConfigurationView.HeightTile * 17 + 1);
        private Point pointLevel = new Point(ConfigurationView.WidthTile - 2, ConfigurationView.HeightTile * 24 + 1);
        private int countTankEnemy;
        private int level;
        private int countTankPlaeyr;
        private Image imgInfo;

        public LevelInfo()
        {
            UpdateImage();
        }

        public Image ImgInfo { get { return imgInfo; } }
        public int Level
        {
            set
            {
                level = value;

                if (level > 9) pointLevel.X = -2;
                else pointLevel.X = ConfigurationView.WidthTile - 2;

                UpdateImage();
            }
        }
        public int CountTankEnemy
        {
            set
            {
                countTankEnemy = value;
                UpdateImage();
            }
        }
        public int CountTankPlaeyr
        {
            set
            {
                countTankPlaeyr = value;
                UpdateImage();
            }
        }

        protected void UpdateImage()
        {
            Bitmap bitmap = new Bitmap(Images.DashboardInfo);
            Graphics g = Graphics.FromImage(bitmap);

            g.DrawString(countTankPlaeyr.ToString(), font, Brushes.Black, new Point(pointCountTankPlaeyr.X, pointCountTankPlaeyr.Y));
            ;
            g.DrawString(level.ToString(), font, Brushes.Black, new Point(pointLevel.X, pointLevel.Y));

            for (int i = 0; i < countTankEnemy; i++)
            {
                int x, y = i / 2 * ConfigurationView.HeightTile;
                if (i % 2 == 0) x = 0;
                else x = ConfigurationView.WidthTile;

                g.DrawImage(Images.InformationTank, x, y);
            }

            imgInfo = bitmap;
        }
    }
}
