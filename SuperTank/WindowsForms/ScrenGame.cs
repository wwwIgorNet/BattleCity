﻿using SuperTank.View;
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
        private SceneView sceneView;
        private ScrenLoadLevel viewLoadLevel = new ScrenLoadLevel();
        private ScrenScore screnScore = new ScrenScore();
        private ScrenGameOver screnGameOver = new ScrenGameOver();
        private int countPoints = 0;

        public ScrenGame(SceneView sceneView, Action gameOver)
        {
            this.gameOver = gameOver;
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

        public int CountPoints { get { return countPoints; } }

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
            this.countPoints = countPoints;
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
