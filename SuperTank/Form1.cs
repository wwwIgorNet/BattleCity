using SuperTank.Interface;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTank
{
    public partial class Form1 : Form, IRender
    {
        private readonly Timer timer = new Timer();
        private readonly List<BaseView> drowable = new List<BaseView>();
        private readonly IFactoryUnit factoryUnit;
        private readonly IFactoryViewUnit factoryViewUnit;
        private readonly Plaeyr plaeyr;

        public Form1()
        {
            InitializeComponent();
            GraphicsOption();
            this.ClientSize = new Size(Configuration.WidthBoard, Configuration.HeightBoard);
            factoryUnit = new FactoryUnit();
            factoryViewUnit = new FactoryViewUnit(this);

            Unit unit = factoryUnit.Create(TypeUnit.PlainTank);
            factoryViewUnit.Create(unit);
            plaeyr = new Plaeyr(unit);

            timer.Interval = Configuration.TimerInterval;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public List<BaseView> Drowable { get { return drowable; } }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            drowable.ForEach(item => item.Draw(g));
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
            plaeyr.Update();
            this.Invalidate();
        }
    }
}
