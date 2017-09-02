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

namespace SuperTank.WindowsForms
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class GameForm : Form, IGameInfo
    {
        private readonly Form form;

        private IKeyboard keyboard;
        private ScrenGame screnGame;
        private ViewLoadLevel viewLoadLevel;

        public GameForm(SceneView sceneView)
        {
            form = this;
            InitializeComponent();

            //GraphicsOption();
            this.ClientSize = new Size(ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);

            this.SuspendLayout();
            screnGame = new ScrenGame(sceneView);
            Controls.Add(screnGame);
            viewLoadLevel = new ViewLoadLevel();
            Controls.Add(viewLoadLevel);
            viewLoadLevel.Location = new Point(0, 0);
            viewLoadLevel.Size = this.Size;
            viewLoadLevel.BringToFront();
            viewLoadLevel.Visible = false;

            this.ResumeLayout(false);

            this.Focus();
        }

        public IKeyboard Keyboard
        {
            get { return keyboard; }
            set { keyboard = value; }
        }

        public void EndLevel(int countPoints, Dictionary<TypeUnit, int> destrouTanksPlaeyr)
        {
            viewLoadLevel.CloseScrean();
            viewLoadLevel.Visible = true;
            System.Threading.ThreadPool.QueueUserWorkItem((s) =>
            {
                System.Threading.Thread.Sleep(2000);
                form.Invoke(new Action(() => { viewLoadLevel.OpenScrean(); }));
            });
        }

        public void StartLevel(int level)
        {
            screnGame.StartLevel(level);
            viewLoadLevel.Visible = false;
        }

        public void GameOver()
        {
            throw new NotImplementedException();
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
