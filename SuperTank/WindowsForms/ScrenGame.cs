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
    class ScrenGame : Label
    {
        private Timer timerInvalidate = new Timer();
        private LevelInfo levelInfo;
        private SceneView sceneView;
        private ScrenLoadLevel viewLoadLevel = new ScrenLoadLevel();
        private ScrenScore screnScore = new ScrenScore();

        public ScrenGame(SceneView sceneView)
        {
            timerInvalidate.Interval = ConfigurationView.TimerInterval;
            timerInvalidate.Tick += (s, e) => { Invalidate(); };
            timerInvalidate.Start();
            this.BackColor = ConfigurationView.BackColor;
            this.ClientSize = new Size(ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);

            this.SuspendLayout();
            this.sceneView = sceneView;

            levelInfo = new LevelInfo();
            this.ResumeLayout(false);
            
            GraphicsOption();
        }

        public void SetCountTankEnemy(int count)
        {
            levelInfo.CountTankEnemy = count;
        }

        public void SetCountTankPlaeyr(int count)
        {
            levelInfo.CountTankPlaeyr = count;
        }

        public void StartLevel(int level)
        {
            viewLoadLevel.EndClose += () => levelInfo.Level = level;
            viewLoadLevel.Start(level);
            GameForm.Sound.GameStart();
        }
        public void EndLevel(int level, int countPoints, Dictionary<TypeUnit, int> destrouTanksPlaeyr)
        {
            screnScore.EndLevel(level, countPoints, destrouTanksPlaeyr);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (screnScore.IsAcive)
            {
                e.Graphics.DrawImage(screnScore.ImgScren, 0, 0, ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);
            }
            else
            {
                sceneView.Draw(e.Graphics);
                e.Graphics.DrawImage(levelInfo.ImgInfo, ConfigurationView.WidthBoard + ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile);
                if (viewLoadLevel.IsAcive)
                {
                    e.Graphics.DrawImage(viewLoadLevel.ImgScren, 0, 0, ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);
                }
            }
        }

        private void GraphicsOption()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}
