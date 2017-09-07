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
    class ScrenScore : BaseScren
    {
        private int level;
        private int countPoints;
        private Dictionary<TypeUnit, int> destrouTanksPlaeyr;
        private Font font = new Font(ConfigurationView.InfoFontFamily, 13);
        private float thirdOfWidth; // треть ширины
        private float horisontalCentr;
        private float verticalCentr;

        public ScrenScore()
        {
            thirdOfWidth = Width / 3; // треть ширины
            horisontalCentr = Width / 2;
            verticalCentr = Height / 2;
        }

        public void EndLevel(int level, int countPoints, Dictionary<TypeUnit, int> destrouTanksPlaeyr)
        {
            this.level = level;
            this.countPoints = countPoints;
            this.destrouTanksPlaeyr = destrouTanksPlaeyr;
            Start();
            UpdateImage();
        }

        public override void UpdateImage()
        {
            float lineInterval = 54;

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
            total.Y = lineInterval * 9 - 10;

            Text totalTank = new Text(destrouTanksPlaeyr.Sum(kv => kv.Value).ToString(), Brushes.White);
            totalTank.Size = g.MeasureString(totalTank.Str, font);
            totalTank.Y = total.Y;
            totalTank.X = horisontalCentr - 20 - g.MeasureString("←", font).Width - totalTank.Width;

            DrawString(g, hiscore);
            DrawString(g, text20000);
            DrawString(g, stage);
            DrawString(g, iPlayer);
            DrawString(g, points);

            DrawLine(g, lineInterval * 5, TypeUnit.PlainTank, destrouTanksPlaeyr);
            DrawLine(g, lineInterval * 6, TypeUnit.ArmoredPersonnelCarrierTank, destrouTanksPlaeyr);
            DrawLine(g, lineInterval * 7, TypeUnit.QuickFireTank, destrouTanksPlaeyr);
            DrawLine(g, lineInterval * 8, TypeUnit.ArmoredTank, destrouTanksPlaeyr);

            g.FillRectangle(Brushes.White, thirdOfWidth + 15, total.Y - 10, thirdOfWidth - 30, 3);

            DrawString(g, total);
            DrawString(g, totalTank);
            
        }

        private void DrawLine(Graphics g, float y, TypeUnit tankType, Dictionary<TypeUnit, int> destrouTanksPlaeyr)
        {
            Text pts = new Text(" PTS", Brushes.White);
            pts.Size = g.MeasureString(pts.Str, font);
            pts.X = thirdOfWidth - pts.Width;
            pts.Y = y;

            Text points = new Text((destrouTanksPlaeyr[tankType] * ConfigurationView.GetCountPoints(tankType)).ToString(), Brushes.White);
            points.Size = g.MeasureString(points.Str, font);
            points.X = pts.X - points.Width;
            points.Y = y;

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

            Text textCountTank = new Text(destrouTanksPlaeyr[tankType].ToString(), Brushes.White);
            textCountTank.Size = g.MeasureString(textCountTank.Str, font);
            textCountTank.X = arrow.X - textCountTank.Width;
            textCountTank.Y = y;

            DrawString(g, points);
            DrawString(g, pts);
            DrawString(g, textCountTank);
            DrawString(g, arrow);
            g.DrawImage(tank, posTank);
        }

        private void DrawString(Graphics g, Text text)
        {
            g.DrawString(text.Str, font, text.Brush, text.Point);
        }

        protected override void Timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now - StartTime < ConfigurationGame.DelayScrenScore)
            {
                UpdateImage();
            }
            else
            {
                IsAcive = false;
                TimerStop();
            }
        }
    }
}
