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
        private Font font = new Font(ConfigurationView.InfoFontFamily, 13);
        private float horisontalCentr;
        private float verticalCentr;
        private float pointsRightX;
        private float countTankRightX;
        private float armoredPersonnelCarrierTankY;
        private float quickFireTankY;
        private float armoredTankY;

        public ScrenScore()
        {
            ThirdOfWidth = Width / 3; // треть ширины
            horisontalCentr = Width / 2;
            verticalCentr = Height / 2;

            LineInterval = 54;
            PlainTankY = LineInterval * 5;
            armoredPersonnelCarrierTankY = LineInterval * 6;
            quickFireTankY = LineInterval * 7;
            armoredTankY = LineInterval * 8;
            TotalYPos = LineInterval * 9 - 10;
            SetInterval(150);
        }

        protected Font Font { get { return font; } }
        protected float ThirdOfWidth { get; set; } // треть ширины
        protected float LineInterval { get; set; }
        protected float TotalYPos { get; set; }
        public TimeSpan DelayScrenPoints { get; protected set; }
        protected float PlainTankY { get; set; }
        protected int Row { get; set; }
        protected TypeUnit CurentTank { get; set; }
        protected int CountTankIPlayer { get; set; }
        protected Dictionary<TypeUnit, int> DestrouTanksIPlaeyr { get; set; }

        public override void UpdateImage()
        {
            if (Row < DestrouTanksIPlaeyr.Count)
            {
                if (CountTankIPlayer < DestrouTanksIPlaeyr[CurentTank])
                {
                    UpdateTankIPlayer(PlainTankY + LineInterval * Row, CurentTank, CountTankIPlayer);
                    GameForm.Sound.CountTankIncrement();
                    CountTankIPlayer++;
                }
                else if (DestrouTanksIPlaeyr[CurentTank] == CountTankIPlayer)
                {
                    UpdateTankIPlayer(PlainTankY + LineInterval * Row, CurentTank, CountTankIPlayer);
                    RowIncrement();
                }
            }
            else DrawTotal();
        }

        protected virtual void RowIncrement()
        {
            Row++;
            CurentTank++;
            CountTankIPlayer = 0;
        }

        protected virtual void DrawTotal()
        {
            Graphics g = Graphics.FromImage(ImgScren);
            Text totalTank = new Text(DestrouTanksIPlaeyr.Values.Sum().ToString(), Brushes.White);
            totalTank.Size = g.MeasureString(totalTank.Str, Font);
            totalTank.X = countTankRightX - totalTank.Width;
            totalTank.Y = TotalYPos;

            DrawString(g, totalTank);
        }

        public virtual void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr, int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            DelayScrenPoints = ConfigurationView.GetDelayScrenPoints(destrouTanksIPlaeyr.Values.Sum());
            this.level = level;
            this.countPoints = countPointsIPlayer;
            this.DestrouTanksIPlaeyr = destrouTanksIPlaeyr;
            Start();
            Row = 0;
            CountTankIPlayer = 0;
            CurentTank = TypeUnit.PlainTank;
            CreateImg();
        }

        protected override void Timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now - StartTime < DelayScrenPoints)
            {
                UpdateImage();
            }
            else
            {
                IsAcive = false;
                TimerStop();
            }
        }

        protected void UpdateTankIPlayer(float y, TypeUnit tank, int countTank)
        {
            Graphics g = Graphics.FromImage(ImgScren);
            Text points = new Text((countTank * ConfigurationView.GetCountPoints(tank)).ToString(), Brushes.White);
            points.Size = g.MeasureString(points.Str, Font);
            points.X = pointsRightX - points.Width;
            points.Y = y;

            Text textCountTank = new Text(countTank.ToString(), Brushes.White);
            textCountTank.Size = g.MeasureString(textCountTank.Str, Font);
            textCountTank.X = countTankRightX - textCountTank.Width;
            textCountTank.Y = y;

            g.FillRectangle(Brushes.Black, new RectangleF(points.Point, points.Size));
            DrawString(g, points);
            g.FillRectangle(Brushes.Black, new RectangleF(textCountTank.Point, textCountTank.Size));
            DrawString(g, textCountTank);
        }
        protected virtual void CreateImg()
        {
            Graphics g = Graphics.FromImage(ImgScren);
            g.Clear(Color.Black);

            Text hiscore = new Text("HI-SCORE ", Brushes.Red);
            hiscore.Size = g.MeasureString(hiscore.Str, Font);
            hiscore.Y = LineInterval;

            Text text20000 = new Text(" 20000", Brushes.Yellow);
            text20000.Size = g.MeasureString(text20000.Str, Font);
            text20000.Y = LineInterval;

            hiscore.X = horisontalCentr - (hiscore.Width + text20000.Width) / 2;
            text20000.X = hiscore.X + hiscore.Width;

            Text stage = new Text("STAGE " + level, Brushes.White);
            stage.Size = g.MeasureString(stage.Str, Font);
            stage.X = horisontalCentr - stage.Width / 2;
            stage.Y = LineInterval * 2;

            Text IPlayer = new Text("I-PLAYER", Brushes.Red);
            IPlayer.Size = g.MeasureString(IPlayer.Str, Font);
            IPlayer.X = ThirdOfWidth - IPlayer.Width;
            IPlayer.Y = LineInterval * 3;

            Text points = new Text(countPoints.ToString(), Brushes.Yellow);
            points.Size = g.MeasureString(points.Str, Font);
            points.X = ThirdOfWidth - points.Width;
            points.Y = LineInterval * 4;

            Text total = new Text("TOTAL", Brushes.White);
            total.Size = g.MeasureString(total.Str, Font);
            total.X = ThirdOfWidth - total.Width;
            total.Y = TotalYPos;

            DrawString(g, hiscore);
            DrawString(g, text20000);
            DrawString(g, stage);
            DrawString(g, IPlayer);
            DrawString(g, points);

            DrawLine(g, PlainTankY, TypeUnit.PlainTank);
            DrawLine(g, armoredPersonnelCarrierTankY, TypeUnit.ArmoredPersonnelCarrierTank);
            DrawLine(g, quickFireTankY, TypeUnit.QuickFireTank);
            DrawLine(g, armoredTankY, TypeUnit.ArmoredTank);

            g.FillRectangle(Brushes.White, ThirdOfWidth + 15, total.Y - 10, ThirdOfWidth - 30, 3);

            DrawString(g, total);
        }
        protected virtual void DrawLine(Graphics g, float y, TypeUnit tankType)
        {
            Text pts = new Text(" PTS", Brushes.White);
            pts.Size = g.MeasureString(pts.Str, Font);
            pts.X = ThirdOfWidth - pts.Width;
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
            arrow.Size = g.MeasureString(arrow.Str, Font);
            arrow.X = posTank.X - arrow.Width;
            arrow.Y = y;

            countTankRightX = arrow.X;

            DrawString(g, pts);
            DrawString(g, arrow);
            g.DrawImage(tank, posTank);
        }
        protected void DrawString(Graphics g, Text text)
        {
            g.DrawString(text.Str, Font, text.Brush, text.Point);
        }
    }
}
