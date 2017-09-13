using SuperTank.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using SuperTank.Audio;

namespace SuperTank.WindowsForms
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class GameForm : Form, IGameInfo
    {
        private IKeyboard keyboard;
        private ScrenGame screnGame;
        private static IViewSound viewSound;

        public GameForm(SceneView sceneView, IViewSound sound)
        {
            InitializeComponent();

            GameForm.viewSound = sound;
            this.ClientSize = new Size(ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);

            this.SuspendLayout();
            screnGame = new ScrenGame(sceneView);
            Controls.Add(screnGame);
            this.ResumeLayout(false);

            this.Focus();
        }

        public static IViewSound Sound { get { return viewSound; } }

        public IKeyboard Keyboard
        {
            get { return keyboard; }
            set { keyboard = value; }
        }

        public void EndLevel(int level, int countPoints, Dictionary<TypeUnit, int> destrouTanksPlaeyr)
        {
            screnGame.EndLevel(level, countPoints, destrouTanksPlaeyr);
        }

        public void StartLevel(int level)
        {
            screnGame.StartLevel(level);
        }

        public void GameOver()
        {
            screnGame.GamoOver();
        }

        public void SetCountTankEnemy(int count)
        {
            screnGame.SetCountTankEnemy(count);
        }

        public void SetCountTankPlaeyr(int count)
        {
            screnGame.SetCountTankPlaeyr(count);
        }


        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    Graphics g = e.Graphics;
        //}
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.Space:
                case Keys.Enter:
                case Keys.Escape:
                    keyboard.KeyDown(e.KeyCode);
                    break;
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.Space:
                case Keys.Enter:
                case Keys.Escape:
                    keyboard.KeyUp(e.KeyCode);
                    break;
            }
        }

        //private void GraphicsOption()
        //{
        //    base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        //}
    }
}
