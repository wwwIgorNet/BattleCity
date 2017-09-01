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
        private SceneView sceneView;
        private LevelInfo levelInfo;

        public ScrenGame(SceneView sceneView)
        {
            this.BackColor = ConfigurationView.BackColor;
            this.ClientSize = new Size(ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);

            this.SuspendLayout();
            this.sceneView = sceneView;
            Controls.Add(sceneView);

            levelInfo = new LevelInfo();
            levelInfo.Location = new Point(ConfigurationView.WidthBoard + ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile);
            Controls.Add(levelInfo);

            this.ResumeLayout(false);
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
            levelInfo.Level = level;
        }
    }
}
