using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.WindowsForms
{
    /// <summary>
    /// Scren two players
    /// </summary>
    class ScrenScoreTwoPlayers : ScrenScore
    {
        private int countPointsIIPlayer;
        private Dictionary<TypeUnit, int> destrouTanksIIPlaeyr;
        private float pointsLeftX;
        private float countTankLeftX;
        private int countTankIIPlayer;

        public override void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr, int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            this.countPointsIIPlayer = countPointsIIPlayer;
            this.destrouTanksIIPlaeyr = destrouTanksIIPlaeyr;
            base.EndLevel(level, countPointsIPlayer, destrouTanksIPlaeyr, countPointsIIPlayer, destrouTanksIIPlaeyr);

            DelayScrenPoints = ConfigurationWinForms.GetDelayScrenPoints(destrouTanksIPlaeyr.Values.Sum() + destrouTanksIIPlaeyr.Values.Sum());

            countTankIIPlayer = 0;
        }

        protected override void CreateImg()
        {
            base.CreateImg();
            Graphics g = Graphics.FromImage(ImgScren);

            float leftPos = ThirdOfWidth * 2;

            Text IIPlayer = new Text("II-PLAYER", Brushes.Red);
            IIPlayer.X = leftPos;
            IIPlayer.Y = LineInterval * 3;

            Text points = new Text(countPointsIIPlayer.ToString(), Brushes.Yellow);
            points.X = leftPos;
            points.Y = LineInterval * 4;

            Text total = new Text("TOTAL", Brushes.White);
            total.X = leftPos;
            total.Y = TotalYPos;

            DrawString(g, IIPlayer);
            DrawString(g, points);

            DrawString(g, total);
        }
        public override void UpdateImage()
        {
            if (Row < destrouTanksIIPlaeyr.Count)
            {
                if (CountTankIPlayer < DestrouTanksIPlaeyr[CurentTank] || countTankIIPlayer < destrouTanksIIPlaeyr[CurentTank])
                {
                    UpdateTankIPlayer(PlainTankY + LineInterval * Row, CurentTank, CountTankIPlayer);
                    UpdateTankIIPlayer(PlainTankY + LineInterval * Row, CurentTank, countTankIIPlayer);

                    GameForm.Sound.CountTankIncrement();

                    if (CountTankIPlayer < DestrouTanksIPlaeyr[CurentTank]) CountTankIPlayer++;
                    if (countTankIIPlayer < destrouTanksIIPlaeyr[CurentTank]) countTankIIPlayer++;
                }
                else if (DestrouTanksIPlaeyr[CurentTank] == CountTankIPlayer && destrouTanksIIPlaeyr[CurentTank] == countTankIIPlayer)
                {
                    UpdateTankIPlayer(PlainTankY + LineInterval * Row, CurentTank, CountTankIPlayer);
                    UpdateTankIIPlayer(PlainTankY + LineInterval * Row, CurentTank, countTankIIPlayer);
                    RowIncrement();
                }
            }
            else DrawTotal();
        }
        protected override void RowIncrement()
        {
            base.RowIncrement();
            countTankIIPlayer = 0;
        }
        protected override void DrawTotal()
        {
            base.DrawTotal();
            Graphics g = Graphics.FromImage(ImgScren);
            Text totalTank = new Text(destrouTanksIIPlaeyr.Values.Sum().ToString(), Brushes.White);
            totalTank.X = countTankLeftX;
            totalTank.Y = TotalYPos;

            DrawString(g, totalTank);
        }
        private void UpdateTankIIPlayer(float y, TypeUnit tank, int countTank)
        {
            Graphics g = Graphics.FromImage(ImgScren);
            Text points = new Text((countTank * ConfigurationWinForms.GetCountPoints(tank)).ToString(), Brushes.White);
            points.Size = g.MeasureString(points.Str, Font);
            points.X = pointsLeftX;
            points.Y = y;

            Text textCountTank = new Text(countTank.ToString(), Brushes.White);
            textCountTank.Size = g.MeasureString(textCountTank.Str, Font);
            textCountTank.X = countTankLeftX;
            textCountTank.Y = y;

            g.FillRectangle(Brushes.Black, new RectangleF(points.Point, points.Size));
            DrawString(g, points);
            g.FillRectangle(Brushes.Black, new RectangleF(textCountTank.Point, textCountTank.Size));
            DrawString(g, textCountTank);
        }

        protected override void DrawLine(Graphics g, float y, TypeUnit tankType)
        {
            base.DrawLine(g, y, tankType);

            Text pts = new Text("PTS ", Brushes.White);
            pts.Size = g.MeasureString(pts.Str, Font);
            pts.X = ThirdOfWidth * 2;
            pts.Y = y;

            pointsLeftX = pts.X + pts.Width;

            Text arrow = new Text("→", Brushes.White);
            arrow.Size = g.MeasureString(arrow.Str, Font);
            arrow.X = Width / 2 + ConfigurationWinForms.WidthTank / 2;
            arrow.Y = y;

            countTankLeftX = arrow.X + arrow.Width;

            DrawString(g, arrow);
            DrawString(g, pts);
        }
    }
}
