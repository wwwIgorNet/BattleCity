using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;

namespace SuperTank.WindowsForms
{
    /// <summary>
    /// Manages the display of the game and game status information
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class ScrenGame : Label, IGameInfo
    {
        private Action gameOver = ()=> { };
        private Action startGame=()=> { };
        private Timer timerInvalidate = new Timer();
        private LevelInfo levelInfo;
        private SceneScene sceneView;
        private ScrenLoadLevel viewLoadLevel;
        private ScrenScore screnScore;
        private ScrenGameOver screnGameOver = new ScrenGameOver();
        private int countPointsIPlayer = 0;
        private int countPointsIIPlayer = 0;
        private System.Threading.AutoResetEvent autoResetEvent = new System.Threading.AutoResetEvent(false);

        public ScrenGame(SceneScene sceneView, LevelInfo levelInfo, ScrenScore screnScore, Action gameOver, Action startGame)
        {
            this.startGame += startGame;
            this.gameOver += gameOver;
            timerInvalidate.Interval = ConfigurationWinForms.TimerInterval;
            timerInvalidate.Tick += (s, e) => { Invalidate(); };
            timerInvalidate.Start();
            this.BackColor = ConfigurationWinForms.BackColor;
            this.ClientSize = new Size(ConfigurationWinForms.WindowClientWidth, ConfigurationWinForms.WindowClientHeight);

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
            GameForm.Sound.LevelStart();
            autoResetEvent.Reset();
        }
        public void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr, int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            screnScore.EndLevel(level, countPointsIPlayer, destrouTanksIPlaeyr, countPointsIIPlayer, destrouTanksIIPlaeyr);
            this.countPointsIPlayer = countPointsIPlayer;
            this.countPointsIIPlayer = countPointsIIPlayer;
            autoResetEvent.Set();
        }
        public void GameOver()
        {
            System.Threading.ThreadPool.QueueUserWorkItem( o =>
            {
                autoResetEvent.WaitOne();
                Invoke(new MethodInvoker(gameOver.Invoke));
                screnGameOver.IsAcive = false;
            });

            GameForm.Sound.GameOver();
            screnGameOver.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (screnScore.IsAcive)
            {
                e.Graphics.DrawImage(screnScore.ImgScren, 0, 0, ConfigurationWinForms.WindowClientWidth, ConfigurationWinForms.WindowClientHeight);
            }
            else
            {
                sceneView.Draw(e.Graphics);
                e.Graphics.DrawImage(levelInfo.ImgInfo, ConfigurationWinForms.WidthBoard + ConfigurationWinForms.WidthTile * 2, ConfigurationWinForms.HeightTile);
                if (viewLoadLevel.IsAcive)
                {
                    e.Graphics.DrawImage(viewLoadLevel.ImgScren, 0, 0, ConfigurationWinForms.WindowClientWidth, ConfigurationWinForms.WindowClientHeight);
                }
                else if (screnGameOver.IsAcive)
                {
                    e.Graphics.DrawImage(screnGameOver.ImgScren, 0, 0, ConfigurationWinForms.WindowClientWidth, ConfigurationWinForms.WindowClientHeight);
                }
            }
        }

        private void GraphicsOption()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void StartGame()
        {
            startGame.Invoke();
        }
    }
}
