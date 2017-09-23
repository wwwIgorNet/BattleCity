using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SuperTank.WindowsForms
{
    class ScrenScore : BaseScren
    {
        private int level;
        private int countPoints;
        private Dictionary<TypeUnit, int> destrouTanksPlaeyr;
        private Font font = new Font(ConfigurationView.InfoFontFamily, 13);
        private float thirdOfWidth; // треть ширины
        private float horisontalCentr;
        private float verticalCentr;
        private float pointsRightX;
        private float countTankRightX;
        private float plainTankY;
        private float armoredPersonnelCarrierTankY;
        private float quickFireTankY;
        private float armoredTankY;
        private float totalY;
        private float lineInterval;
        private int iteration;
        private int countTank;
        private TypeUnit curentTank;

        public ScrenScore()
        {
            thirdOfWidth = Width / 3; // треть ширины
            horisontalCentr = Width / 2;
            verticalCentr = Height / 2;

            lineInterval = 54;
            plainTankY = lineInterval * 5;
            armoredPersonnelCarrierTankY = lineInterval * 6;
            quickFireTankY = lineInterval * 7;
            armoredTankY = lineInterval * 8;
            totalY = lineInterval * 9 - 10;
            SetInterval(150);
        }

        public void EndLevel(int level, int countPoints, Dictionary<TypeUnit, int> destrouTanksPlaeyr)
        {
            this.level = level;
            this.countPoints = countPoints;
            this.destrouTanksPlaeyr = destrouTanksPlaeyr;
            Start();
            iteration = 0;
            countTank = 0;
            curentTank = TypeUnit.PlainTank;
            CreateImg();
        }
        public override void UpdateImage()
        {
            if (iteration < destrouTanksPlaeyr.Count)
            {
                if (countTank < destrouTanksPlaeyr[curentTank])
                {
                    UpdateTank(plainTankY + lineInterval * iteration, TypeUnit.PlainTank + iteration, countTank);
                    countTank++;
                }
                else if (destrouTanksPlaeyr[curentTank] == countTank)
                {
                    UpdateTank(plainTankY + lineInterval * iteration, curentTank, countTank);
                    curentTank++;
                    iteration++;
                    countTank = 0;
                }
            }
            else
            {
                Graphics g = Graphics.FromImage(ImgScren);
                Text totalTank = new Text(destrouTanksPlaeyr.Sum(kv => kv.Value).ToString(), Brushes.White);
                totalTank.Size = g.MeasureString(totalTank.Str, font);
                totalTank.X = countTankRightX - totalTank.Width;
                totalTank.Y = totalY;

                DrawString(g, totalTank);
            }
        }

        protected override void Timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now - StartTime < ConfigurationView.DelayScrenPoints)
            {
                UpdateImage();
            }
            else
            {
                IsAcive = false;
                TimerStop();
            }
        }

        private void UpdateTank(float y, TypeUnit tank, int countTank)
        {
            if (countTank > 0)
                GameForm.Sound.CountTankIncrement();

            Graphics g = Graphics.FromImage(ImgScren);
            Text points = new Text((countTank * ConfigurationView.GetCountPoints(tank)).ToString(), Brushes.White);
            points.Size = g.MeasureString(points.Str, font);
            points.X = pointsRightX - points.Width;
            points.Y = y;

            Text textCountTank = new Text(countTank.ToString(), Brushes.White);
            textCountTank.Size = g.MeasureString(textCountTank.Str, font);
            textCountTank.X = countTankRightX - textCountTank.Width;
            textCountTank.Y = y;

            g.FillRectangle(Brushes.Black, new RectangleF(points.Point, points.Size));
            DrawString(g, points);
            g.FillRectangle(Brushes.Black, new RectangleF(textCountTank.Point, textCountTank.Size));
            DrawString(g, textCountTank);
        }
        private void CreateImg()
        {
            Graphics g = Graphics.FromImage(ImgScren);
            g.Clear(Color.Black);

            Text hiscore = new Text("HI-SCORE ", Brushes.Red);
            hiscore.Size = g.MeasureString(hiscore.Str, font);
            hiscore.Y = lineInterval;

            Text text20000 = new Text(" 20000", Brushes.Yellow);
            text20000.Size = g.MeasureString(text20000.Str, font);
            text20000.Y = lineInterval;

            hiscore.X = horisontalCentr - (hiscore.Width + text20000.Width) / 2;
            text20000.X = hiscore.X + hiscore.Width;

            Text stage = new Text("STAGE " + level, Brushes.White);
            stage.Size = g.MeasureString(stage.Str, font);
            stage.X = horisontalCentr - stage.Width / 2;
            stage.Y = lineInterval * 2;

            Text iPlayer = new Text("I-PLAYER", Brushes.Red);
            iPlayer.Size = g.MeasureString(iPlayer.Str, font);
            iPlayer.X = thirdOfWidth - iPlayer.Width;
            iPlayer.Y = lineInterval * 3;

            Text points = new Text(countPoints.ToString(), Brushes.Yellow);
            points.Size = g.MeasureString(points.Str, font);
            points.X = thirdOfWidth - points.Width;
            points.Y = lineInterval * 4;

            Text total = new Text("TOTAL", Brushes.White);
            total.Size = g.MeasureString(total.Str, font);
            total.X = thirdOfWidth - total.Width;
            total.Y = totalY;

            DrawString(g, hiscore);
            DrawString(g, text20000);
            DrawString(g, stage);
            DrawString(g, iPlayer);
            DrawString(g, points);

            DrawLine(g, plainTankY, TypeUnit.PlainTank);
            DrawLine(g, armoredPersonnelCarrierTankY, TypeUnit.ArmoredPersonnelCarrierTank);
            DrawLine(g, quickFireTankY, TypeUnit.QuickFireTank);
            DrawLine(g, armoredTankY, TypeUnit.ArmoredTank);

            g.FillRectangle(Brushes.White, thirdOfWidth + 15, total.Y - 10, thirdOfWidth - 30, 3);

            DrawString(g, total);
        }
        private void DrawLine(Graphics g, float y, TypeUnit tankType)
        {
            Text pts = new Text(" PTS", Brushes.White);
            pts.Size = g.MeasureString(pts.Str, font);
            pts.X = thirdOfWidth - pts.Width;
            pts.Y = y;

            pointsRightX = pts.X;

            Image tank = null;
            switch (tankType)
            {
                case TypeUnit.PlainTank:
                    tank = Images.Enemy.PlainTank.Up1;
                    break;
                case TypeUnit.ArmoredPersonnelCarrierTank:
                    tank = Images.Enemy.ArmoredPersonnelCarrierTank.Up1;
                    break;
                case TypeUnit.QuickFireTank:
                    tank = Images.Enemy.QuickFireTank.Up1;
                    break;
                case TypeUnit.ArmoredTank:
                    tank = Images.Enemy.ArmoredTank.Up1;
                    break;
            }
            PointF posTank = new PointF(horisontalCentr - tank.Width / 2, y - 12);

            Text arrow = new Text("←", Brushes.White);
            arrow.Size = g.MeasureString(arrow.Str, font);
            arrow.X = posTank.X - arrow.Width;
            arrow.Y = y;

            countTankRightX = arrow.X;

            DrawString(g, pts);
            DrawString(g, arrow);
            g.DrawImage(tank, posTank);
        }
        private void DrawString(Graphics g, Text text)
        {
            g.DrawString(text.Str, font, text.Brush, text.Point);
        }
    }
}
