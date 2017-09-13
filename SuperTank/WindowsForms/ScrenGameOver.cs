using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.WindowsForms
{
    class ScrenGameOver : BaseScren
    {
        private Font font = ConfigurationView.FontGameOver;
        private string strGame = "GAME";
        private string strOver = "OVER";
        private float heightGame;
        private float Y;
        private float gameX;
        private float overX;

        public ScrenGameOver()
        {
            Y = base.ImgScren.Height;
            Graphics g = Graphics.FromImage(base.ImgScren);

            SizeF sizeTextGame = g.MeasureString(strGame, font);
            heightGame = sizeTextGame.Height;
            SizeF sizeTextOver = g.MeasureString(strOver, font);
            gameX = base.ImgScren.Width / 2 - sizeTextGame.Width / 2;
            overX = base.ImgScren.Width / 2 - sizeTextGame.Width / 2;
        }

        public override void UpdateImage()
        {
            Graphics g = Graphics.FromImage(base.ImgScren);
            g.Clear(Color.Transparent);

            g.DrawString(strGame, font, Brushes.White, gameX - 1, Y - 1);
            g.DrawString(strOver, font, Brushes.White, overX - 1, Y - 1 + heightGame);

            g.DrawString(strGame, font, Brushes.White, gameX + 1, Y + 1);
            g.DrawString(strOver, font, Brushes.White, overX + 1, Y + 1 + heightGame);

            g.DrawString(strGame, font, Brushes.DarkRed, gameX, Y);
            g.DrawString(strOver, font, Brushes.DarkRed, overX, Y + heightGame);
            Y -= 7;
        }

        protected override void Timer_Tick(object sender, EventArgs e)
        {
            if (Y > base.ImgScren.Height / 2)

                //if (DateTime.Now - StartTime < ConfigurationView.DelayScrenGameOver)
            {
                UpdateImage();
            }
            else
            {
                //IsAcive = false;
                TimerStop();
            }
        }
    }
}
