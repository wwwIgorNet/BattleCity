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
            float thirdOfWidth = Width / 3; // треть ширины
            float lineInterval = 54;
            float horisontalCentr = Width / 2;
            float verticalCentr = Height / 2;
            Graphics g = Graphics.FromImage(ImgScren);
            g.Clear(Color.Black);

            Text text = new Text("HI-SCORE ", g)
            {
                Brush = Brushes.Red,
                Y = lineInterval
            };
            Text number = new Text(" 20000", g)
            {
                Brush = Brushes.Yellow,
                Y = lineInterval
            };
            text.X = horisontalCentr - (text.Width + number.Width) / 2;
            number.X = text.X + text.Width;
            text.Draw();
            number.Draw();

            text = new Text("STAGE " + level, g)
            {
                Brush = Brushes.White,
                Y = text.Y + lineInterval
            };
            text.X = horisontalCentr - text.Width / 2;
            text.Draw();

            text = new Text("I-PLAYER", g)
            {
                Brush = Brushes.Red,
                Y = text.Y + lineInterval
            };
            text.X = thirdOfWidth - text.Width;
            text.Draw();


            text = new Text(countPoints.ToString(), g)
            {
                Brush = Brushes.Yellow,
                Y = text.Y + lineInterval
            };
            text.X = thirdOfWidth - text.Width;
            text.Draw();

            text = new Text((destrouTanksPlaeyr[TypeUnit.PlainTank] * ConfigurationGame.GetCountPoints(TypeUnit.PlainTank)).ToString() + " PTS", g)
            {
                Brush = Brushes.White,
                Y = text.Y + lineInterval
            };
            text.X = thirdOfWidth - text.Width;
            text.Draw();

            Image tank = Images.Enemy.PlainTank.Up1;
            g.DrawImage(tank, horisontalCentr - tank.Width / 2, text.Y - 10);


            text = new Text((destrouTanksPlaeyr[TypeUnit.ArmoredPersonnelCarrierTank] * ConfigurationGame.GetCountPoints(TypeUnit.ArmoredPersonnelCarrierTank)).ToString() + " PTS", g)
            {
                Brush = Brushes.White,
                Y = text.Y + lineInterval
            };
            text.X = thirdOfWidth - text.Width;
            text.Draw();

            tank = Images.Enemy.ArmoredPersonnelCarrierTank.Up1;
            g.DrawImage(tank, horisontalCentr - tank.Width / 2, text.Y - 10);


            text = new Text((destrouTanksPlaeyr[TypeUnit.QuickFireTank] * ConfigurationGame.GetCountPoints(TypeUnit.QuickFireTank)).ToString() + " PTS", g)
            {
                Brush = Brushes.White,
                Y = text.Y + lineInterval
            };
            text.X = thirdOfWidth - text.Width;
            text.Draw();

            text = new Text((destrouTanksPlaeyr[TypeUnit.ArmoredTank] * ConfigurationGame.GetCountPoints(TypeUnit.ArmoredTank)).ToString() + " PTS", g)
            {
                Brush = Brushes.White,
                Y = text.Y + lineInterval
            };
            text.X = thirdOfWidth - text.Width;
            text.Draw();

            text = new Text("TOTAL", g)
            {
                Brush = Brushes.White,
                Y = text.Y + lineInterval - 10
            };
            text.X = thirdOfWidth - text.Width;
            text.Draw();
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
