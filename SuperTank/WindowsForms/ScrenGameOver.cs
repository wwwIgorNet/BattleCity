using SuperTank.View;
using System;
using System.Drawing;

namespace SuperTank.WindowsForms
{
    /// <summary>
    /// Scren game over
    /// </summary>
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
            gameX = ConfigurationView.WidthBoard / 2 - sizeTextGame.Width / 2 + ConfigurationView.WidthTile;
            overX = ConfigurationView.HeightBoard / 2 - sizeTextGame.Width / 2 + ConfigurationView.HeightTile;
        }

        public override void Start()
        {
            base.Start();
            Y = base.ImgScren.Height;
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
            Y -= 5;
        }

        protected override void Timer_Tick(object sender, EventArgs e)
        {
            if (Y > base.ImgScren.Height / 2)
            {
                UpdateImage();
            }
            else
            {
                TimerStop();
            }
        }
    }
}
