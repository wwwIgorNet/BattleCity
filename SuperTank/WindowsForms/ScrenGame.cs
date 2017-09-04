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
        private ViewLoadLevel viewLoadLevel;
        private SceneView sceneView;
        private LevelInfo levelInfo;

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

            viewLoadLevel = new ViewLoadLevel();
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
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            sceneView.Draw(e.Graphics);
            e.Graphics.DrawImage(levelInfo.ImgInfo, ConfigurationView.WidthBoard + ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile);

            e.Graphics.DrawImage(viewLoadLevel.ImgLoadLevel, 0, 0, ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);
        }

        private void GraphicsOption()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}
