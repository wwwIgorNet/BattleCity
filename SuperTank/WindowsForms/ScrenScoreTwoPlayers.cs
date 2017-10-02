using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.WindowsForms
{
    class ScrenScoreTwoPlayers : ScrenScore
    {
        private int countPointsIIPlayer;
        private Dictionary<TypeUnit, int> destrouTanksIIPlaeyr;
        private float pointsLeftX;
        private float countTankLeftX;

        public override void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr, int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            this.countPointsIIPlayer = countPointsIIPlayer;
            this.destrouTanksIIPlaeyr = destrouTanksIIPlaeyr;
            base.EndLevel(level, countPointsIPlayer, destrouTanksIPlaeyr, countPointsIIPlayer, destrouTanksIIPlaeyr);
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

        protected override void DrawLine(Graphics g, float y, TypeUnit tankType)
        {
            base.DrawLine(g, y, tankType);

            Text pts = new Text("PTS ", Brushes.White);
            pts.Size = g.MeasureString(pts.Str, Font);
            pts.X = ThirdOfWidth * 2;
            pts.Y = y;

            pointsLeftX = pts.X;

            Text arrow = new Text("→", Brushes.White);
            arrow.Size = g.MeasureString(arrow.Str, Font);
            arrow.X = Width / 2 + ConfigurationView.WidthTank / 2;
            arrow.Y = y;

            countTankLeftX = arrow.X;

            DrawString(g, arrow);
            DrawString(g, pts);
        }
    }
}
