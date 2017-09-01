using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTank.WindowsForms
{
    class LevelInfo : Label
    {
        private Font font = new Font(ConfigurationView.FontNumbers, 15, FontStyle.Bold);
        private Point pointCountTankPlaeyr = new Point(ConfigurationView.WidthTile - 2, ConfigurationView.HeightTile * 16 + 1);
        private Point pointLevel = new Point(ConfigurationView.WidthTile - 2, ConfigurationView.HeightTile * 23 + 1);
        private int countTankEnemy;
        private int level;
        private int countTankPlaeyr;

        public LevelInfo()
        {
            this.ClientSize = new System.Drawing.Size(ConfigurationView.WidthTile * 2, ConfigurationGame.HeightBoard);
            this.Image = Images.DashboardInfo;

            GraphicsOption();
        }

        public int Level
        {
            set
            {
                level = value;

                if (level > 9) pointLevel.X = -2;
                else pointLevel.X = ConfigurationView.WidthTile - 2;

                this.Invalidate();
            }
        }
        public int CountTankEnemy
        {
            set
            {
                countTankEnemy = value;
                Invalidate();
            }
        }
        public int CountTankPlaeyr
        {
            set
            {
                countTankPlaeyr = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            g.DrawString(countTankPlaeyr.ToString(), font, Brushes.Black, pointCountTankPlaeyr);
;
            g.DrawString(level.ToString(), font, Brushes.Black, pointLevel);

            for (int i = 0; i < countTankEnemy; i++)
            {
                int x, y = i / 2 * ConfigurationView.HeightTile;
                if (i % 2 == 0) x = 0;
                else x = ConfigurationView.WidthTile;

                g.DrawImage(Images.InformationTank, x, y);
            }
        }

        private void GraphicsOption()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}
