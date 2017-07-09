using GameTankCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Super_tank
{
    public partial class GameForm : Form
    {
        private Timer timer;
        private Game game;
        private List<Sprite> sprites;

        public GameForm()
        {
            InitializeComponent();
            GraphicsOption();
            this.ClientSize = new Size(Board.Widtch, Board.Height);

            timer = new Timer();
            timer.Interval = SettingsGame.TimerInterval;
            timer.Tick += Timer_Tick;
            sprites = new List<Sprite>();
            game = new Game();

            foreach (var objBoard in Board.AlloObj)
            {
                CreateSprite(objBoard);
            }

            game.Start();
            timer.Start();
        }

        private void CreateSprite(ObjGame objBoard)
        {
            switch (objBoard.Type)
            {
                case "plain user":
                    sprites.Add(new Sprite(objBoard, 
                        SettingsGame.Content + "SmallTankPlayerUp_1.png"));
                    break;
                case "shell":
                    sprites.Add(new Sprite(objBoard,
                        SettingsGame.Content + "BulletUp.png"));
                    break;
            }
        }

        private void GraphicsOption()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            List<ObjGame> list = Board.AlloObj;
            for (int i = 0; i < list.Count; i++)
            {
                if(!sprites.Exists(v => v.Obj.Equals(list[i])))
                    CreateSprite(list[i]);
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].Draw(g);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Left)
                Keyboard.Left = true;
            else if (e.KeyCode == Keys.Right)
                Keyboard.Right = true;
            else if (e.KeyCode == Keys.Up)
                Keyboard.Up = true;
            else if (e.KeyCode == Keys.Down)
                Keyboard.Down = true;
            else if (e.KeyCode == Keys.Space)
                Keyboard.Space = true;
            else if (e.KeyCode == Keys.Enter)
                Keyboard.Enter = true;
            else if (e.KeyCode == Keys.Escape)
                Keyboard.Escape = true;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyCode == Keys.Left)
                Keyboard.Left = false;
            else if (e.KeyCode == Keys.Right)
                Keyboard.Right = false;
            else if (e.KeyCode == Keys.Up)
                Keyboard.Up = false;
            else if (e.KeyCode == Keys.Down)
                Keyboard.Down = false;
            else if (e.KeyCode == Keys.Space)
                Keyboard.Space = false;
            else if (e.KeyCode == Keys.Enter)
                Keyboard.Enter = false;
            else if (e.KeyCode == Keys.Escape)
                Keyboard.Escape = false;
        }
    }
}
