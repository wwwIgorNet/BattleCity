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

namespace SuperTank.WindowsForms
{
    public partial class GameForm : Form, IRender
    {
        private readonly Timer timer = new Timer();
        private readonly SortedView drowable = new SortedView();
        private readonly IFactoryViewUnit factoryViewUnit = new FactoryViewUnit();

        public GameForm()
        {
            InitializeComponent();
            GraphicsOption();
            this.ClientSize = new Size(ConfigurationView.WidthBoard, ConfigurationView.HeightBoard);
            timer.Interval = ConfigurationView.TimerInterval;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public void SceneChangedHendler(SceneEventArgs e)
        {
            switch (e.Action)
            {
                case TypeAction.Add:
                    BaseView view = factoryViewUnit.Create(e.Unit);
                    if(view != null) drowable.Add(view);
                    break;
                case TypeAction.Remove:
                    drowable.Remove(v => v.Equals(e.Unit));
                    break;
                case TypeAction.Clear:
                    drowable.Clear();
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            drowable.DrowAll(g);
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

        private void GraphicsOption()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        public int Compare(object x, object y)
        {
            throw new NotImplementedException();
        }
    }
}
