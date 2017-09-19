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
    class GameOver : Label
    {
        private Font font = new Font(ConfigurationView.FontFamilyBattleCities, 64);
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
