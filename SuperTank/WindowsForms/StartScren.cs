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
    class StartScren : Label
    {
        private Font font = new Font(ConfigurationView.FontFamilyBattleCities, 45);
        private string strBattle = "BATTLE";
        private string strCity = "CITY";

        protected override void OnPaint(PaintEventArgs e)
        {
            float height = 100;
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.Clear(Color.Black);

            SizeF sizeBattle = g.MeasureString(strBattle, font);
            SizeF sizeCity = g.MeasureString(strCity, font);

            g.DrawString(strBattle, font, Brushes.Red, Width / 2 - sizeBattle.Width / 2, height);
            g.DrawString(strCity, font, Brushes.Red, Width / 2 - sizeCity.Width / 2, height + sizeBattle.Height);
        }
    }
}
