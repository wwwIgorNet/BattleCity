using SuperTank.View;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SuperTank.WindowsForms
{
    class StartScren : Label
    {
        private Image img;
        private Timer timer = new Timer();
        private Image imgTank;
        private float leftPosText;
        private float topPosText;
        private float heightText;
        private float lineHeight = 40;
        private int indexMenu = 0;
        private int countMenu = 3;
        private PointF pointDrawImg;

        public StartScren(Size size)
        {
            this.Size = size;
            img = new Bitmap(Width, Height);
            imgTank = Images.Plaeyr.SmallTank.Right1;
            timer.Interval = ConfigurationView.TimerInterval;
            timer.Tick += Timer_Tick;
            CreateImg();
            pointDrawImg = new PointF(0, Height);
        }

        public bool IsActiv { get; set; }
        public int IndexMenu
        {
            get
            {
                if (!IsActiv) return -1;
                return indexMenu;
            }
        }

        public void Start()
        {
            pointDrawImg.Y = Height;
            timer.Start();
            IsActiv = false;
        }
        public void MenuCursorUp()
        {
            if (IsActiv)
            {
                indexMenu--;
                if (indexMenu < 0) indexMenu = 0;
                Invalidate();
            }
        }
        public void MenuCursorDown()
        {
            if (IsActiv)
            {
                indexMenu++;
                if (indexMenu == countMenu) indexMenu--;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            g.DrawImage(img, pointDrawImg);
            if (IsActiv)
            {
                g.DrawImage(imgTank, leftPosText - imgTank.Width - 10, (topPosText + indexMenu * lineHeight) - (imgTank.Height / 2 - heightText / 2));
            }
        }

        private void CreateImg()
        {
            Font font = new Font(ConfigurationView.FontFamilyBattleCities, 55);
            Font fontInfo = new Font(ConfigurationView.InfoFontFamily, 12, FontStyle.Regular);
            string strBattle = "BATTLE";
            string strCity = "CITY";
            string str1Player = "1 PLAYER";
            string str2Player = "2 PLAYERS";
            string strConstructor = "CONSTRUCTION";
            string strCompany = "company";
            string strYear = "© 2015-2017 COMPANY LTD.";
            string strRights = "ALL RIGHTS RESERVED";

            float height = 100;
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Transparent);

            SizeF sizeBattle = g.MeasureString(strBattle, font);
            SizeF sizeCity = g.MeasureString(strCity, font);

            g.DrawString("I-      00 HI- 20000", fontInfo, Brushes.White, 65, height - lineHeight);
            g.DrawString(strBattle, font, Brushes.Red, Width / 2 - sizeBattle.Width / 2, height);
            g.DrawString(strCity, font, Brushes.Red, Width / 2 - sizeCity.Width / 2, height + sizeBattle.Height);

            SizeF size1Plaeyr = g.MeasureString(str1Player, fontInfo);
            SizeF sizeYear = g.MeasureString(strYear, fontInfo);
            SizeF sizeRights = g.MeasureString(strRights, fontInfo);

            leftPosText = Width / 2 - size1Plaeyr.Width / 2;
            heightText = size1Plaeyr.Height;
            topPosText = height + sizeBattle.Height + sizeCity.Height + lineHeight - heightText;

            g.DrawString(str1Player, fontInfo, Brushes.White, leftPosText, topPosText);
            g.DrawString(str2Player, fontInfo, Brushes.White, leftPosText, topPosText + lineHeight);
            g.DrawString(strConstructor, fontInfo, Brushes.White, leftPosText, topPosText + lineHeight * 2);
            g.DrawString(strCompany, new Font(new FontFamily("Arial"), 25, FontStyle.Bold), Brushes.Red, leftPosText, topPosText + lineHeight * 3 - 15);
            float posX = Width / 2 - sizeYear.Width / 2;
            g.DrawString(strYear, fontInfo, Brushes.White, posX, topPosText + lineHeight * 4);
            g.DrawString(strRights, fontInfo, Brushes.Gray, Width / 2 - sizeRights.Width / 2, topPosText + lineHeight * 5);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            pointDrawImg.Y -= 5;
            if (pointDrawImg.Y <= 0)
            {
                pointDrawImg.Y = 0;
                timer.Stop();
                IsActiv = true;
            }
            Invalidate();
        }
    }
}
