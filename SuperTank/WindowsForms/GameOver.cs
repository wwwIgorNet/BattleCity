using SuperTank.View;
using System.Drawing;
using System.Windows.Forms;

namespace SuperTank.WindowsForms
{
    /// <summary>
    /// Scren game over
    /// </summary>
    class GameOver : Label
    {
        private Font font = new Font(ConfigurationWinForms.FontFamilyBattleCities, 64);
        private string strGame = "GAME";
        private string strOver = "OVER";

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            SizeF sizeGame = g.MeasureString(strGame, font);
            SizeF sizeOver = g.MeasureString(strOver, font);

            g.Clear(Color.Black);
            g.DrawString(strGame, font, Brushes.Red, Width / 2 - sizeGame.Width / 2, Height / 2 - (sizeGame.Height + sizeOver.Height) / 2);
            g.DrawString(strOver, font, Brushes.Red, Width / 2 - sizeOver.Width / 2, Height / 2 - (sizeGame.Height + sizeOver.Height) / 2 + sizeGame.Height);
        }
    }
}
