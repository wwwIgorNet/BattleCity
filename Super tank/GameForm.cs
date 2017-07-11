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
            this.ClientSize = new Size(Configuration.WidthBoard, Configuration.HeightBoard);

            timer = new Timer();
            timer.Interval = Configuration.TimerInterval;
            timer.Tick += Timer_Tick;
            sprites = new List<Sprite>();
            game = new Game();

            foreach (var objBoard in Board.AlloObj)
            {
                if (objBoard is Unit)
                    CreateMovableSprite((Unit)objBoard);
                else
                    CreateTile(objBoard);
            }
            Board.AlloObj.CollectionChanged += AlloObj_CollectionChanged;


            game.Start();
            timer.Start();
        }

        private void AlloObj_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (ObjGame item in e.NewItems)
                    {
                        if (item is Unit)
                            CreateMovableSprite((Unit)item);
                        else
                            CreateTile(item);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach(var item in e.OldItems)
                    {
                        sprites.RemoveAll(m => m.Obj.Equals(item));
                    }
                    break;
            }
        }

        private void CreateTile(ObjGame objBoard)
        {
            switch (objBoard.Type)
            {
                case TypeObjGame.BrickWall:
                    sprites.Add(new Sprite(objBoard, Images.BrickWall));
                    break;
            }
        }

        private void CreateMovableSprite(Unit obj)
        {
            Dictionary<Direction, Image> map = new Dictionary<Direction, Image>();
            switch (obj.Type)
            {
                case TypeObjGame.PlainUserTank:
                    map.Add(Direction.Up, Images.PlainUserTankUp);
                    map.Add(Direction.Down, Images.PlainUserTankDown);
                    map.Add(Direction.Left, Images.PlainUserTankLeft);
                    map.Add(Direction.Right, Images.PlainUserTankRight);
                    sprites.Add(new MovableSprite(map, obj));
                    break;
                case TypeObjGame.Shell:
                    map.Add(Direction.Up, Images.ShellUp);
                    map.Add(Direction.Down, Images.ShellDown);
                    map.Add(Direction.Left, Images.ShellLeft);
                    map.Add(Direction.Right, Images.ShellRight);
                    sprites.Add(new MovableSprite(map, obj));
                    break;
            }
        }

        private void GraphicsOption()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
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
