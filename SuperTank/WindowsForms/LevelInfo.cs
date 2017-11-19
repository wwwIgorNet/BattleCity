using SuperTank.View;
using System.Drawing;

namespace SuperTank.WindowsForms
{
    /// <summary>
    /// Displays information about the game: the number of enemy tanks, the number of lives of player and the current level
    /// </summary>
    class LevelInfo
    {
        private Font font = new Font(ConfigurationView.InfoFontFamily, 13);
        private Point pointCountTankPlaeyr = new Point(ConfigurationView.WidthTile, ConfigurationView.HeightTile * 16 + 12);
        private Point pointLevel = new Point(ConfigurationView.WidthTile, ConfigurationView.HeightTile * 23);
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

        protected Font Font { get { return font; } }
        protected virtual Image DashboardInfo { get { return Images.DashboardInfo; } }

        public virtual void SetCountTankPlaeyr(int count, Owner owner)
        {
            if (Owner.IPlayer == owner)
            {
                countTankPlaeyr = count;
            }
            UpdateImage();
        }

        protected virtual void UpdateImage()
        {
            Bitmap bitmap = new Bitmap(DashboardInfo);
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
