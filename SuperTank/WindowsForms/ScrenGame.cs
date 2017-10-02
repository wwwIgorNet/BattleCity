using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;

namespace SuperTank.WindowsForms
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class ScrenGame : Label, IGameInfo
    {
        private Action gameOver;
        private Timer timerInvalidate = new Timer();
        private LevelInfo levelInfo;
        private SceneScene sceneView;
        private ScrenLoadLevel viewLoadLevel;
        private ScrenScore screnScore;
        private ScrenGameOver screnGameOver = new ScrenGameOver();
        private int countPointsIPlayer = 0;
        private int countPointsIIPlayer = 0;

        public ScrenGame(SceneScene sceneView, LevelInfo levelInfo, ScrenScore screnScore, Action gameOver)
        {
            this.gameOver = gameOver;
            timerInvalidate.Interval = ConfigurationView.TimerInterval;
            timerInvalidate.Tick += (s, e) => { Invalidate(); };
            timerInvalidate.Start();
            this.BackColor = ConfigurationView.BackColor;
            this.ClientSize = new Size(ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);

            this.SuspendLayout();
            this.sceneView = sceneView;

            this.levelInfo = levelInfo;
            viewLoadLevel = new ScrenLoadLevel(levelInfo);
            this.ResumeLayout(false);

            this.screnScore = screnScore;
            GraphicsOption();
        }

        public int CountPointsIPlayer { get { return countPointsIPlayer; } }
        public int CountPointsIIPlayer { get { return countPointsIIPlayer; } }

        public void SetCountTankEnemy(int count)
        {
            levelInfo.CountTankEnemy = count;
        }
        public void SetCountTankPlaeyr(int count, Owner owner)
        {
            levelInfo.SetCountTankPlaeyr(count, owner);
        }
        public void StartLevel(int level)
        {
            viewLoadLevel.Start(level);
            GameForm.Sound.GameStart();
        }
        public void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr, int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            screnScore.EndLevel(level, countPointsIPlayer, destrouTanksIPlaeyr, countPointsIIPlayer, destrouTanksIIPlaeyr);
            this.countPointsIPlayer = countPointsIPlayer;
            this.countPointsIIPlayer = countPointsIIPlayer;
        }
        public void GameOver()
        {
            Timer t = new Timer();
            t.Interval = ConfigurationView.TimeGameOver;
            t.Tick += (o, s) =>
            {
                t.Stop();
                gameOver.Invoke();
                screnGameOver.IsAcive = false;
            };
            t.Start();

            GameForm.Sound.GameOver();
            screnGameOver.Start();
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
                else if (screnGameOver.IsAcive)
                {
                    e.Graphics.DrawImage(screnGameOver.ImgScren, 0, 0, ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);
                }
            }
        }

        private void GraphicsOption()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}
