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
        private Timer timer = new Timer();
        private Font font = new Font(ConfigurationView.FontFamilyBattleCities, 50);
        private Font fontInfo = new Font(ConfigurationView.InfoFontFamily, 16);
        private Image imgTank;
        private string strBattle = "BATTLE";
        private string strCity = "CITY";
        private string strIPlayer = "I PLAYER";
        private string strIIPlayer = "II PLAYERS";
        private string strConstructor = "CONSTRUCTION";
        private int indexMenu = 0;
        private int countMenu = 3;

        public StartScren()
        {
            imgTank = Images.Plaeyr.SmallTank.Right1;
            timer.Interval = ConfigurationView.TimerInterval;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int y = this.Location.Y;
            y -= 5;
            if(y <= 0)
            {
                y = 0;
                timer.Stop();
                IsActiv = true;
            }
            this.Location = new Point(this.Location.X, y);
        }

        public bool  IsActiv { get; set; }

        public void Start()
        {
            timer.Start();
            IsActiv = false;
            this.Location = new Point(this.Location.X, this.Height);
        }

        public void MenuUp()
        {
            if (IsActiv)
            {
                indexMenu--;
                if (indexMenu < 0) indexMenu = 0;
                Invalidate();
            }
        }

        public void MenuDown()
        {
            if (IsActiv)
            {
                indexMenu++;
                if (indexMenu == countMenu) indexMenu--;
                Invalidate();
            }
        }

        public int IndexMenu
        {
            get
            {
                if (!IsActiv) return -1;
                return indexMenu;
            }
        }

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

            SizeF sizeIPlaeyr = g.MeasureString(strIPlayer, fontInfo);
            SizeF sizeIIPlaeyr = g.MeasureString(strIIPlayer, fontInfo);
            SizeF sizeConstructor = g.MeasureString(strConstructor, fontInfo);

            float posY = 300;
            float lineHeight = 40;
            float leftPos = Width / 2 - sizeIPlaeyr.Width / 2;

            g.DrawString(strIPlayer, fontInfo, Brushes.White, leftPos, posY);
            g.DrawString(strIIPlayer, fontInfo, Brushes.Gray, leftPos, posY + lineHeight);
            g.DrawString(strConstructor, fontInfo, Brushes.Gray, leftPos, posY + lineHeight * 2);

            g.DrawImage(imgTank, leftPos - imgTank.Width - 10, (posY + indexMenu * lineHeight) - (imgTank.Height / 2 - sizeIPlaeyr.Height / 2));
        }
    }
}
