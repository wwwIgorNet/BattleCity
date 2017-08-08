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
    public partial class GameForm : Form, IRender
    {
        private readonly Timer timer = new Timer();
        private readonly SortedView listDrowable = new SortedView();
        private readonly IFactoryViewUnit factoryViewUnit = new FactoryViewUnit();

        public GameForm()
        {
            InitializeComponent();
            GraphicsOption();
            this.ClientSize = new Size(ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);
            timer.Interval = ConfigurationView.TimerInterval;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.Black, ConfigurationView.Boaed, ConfigurationView.Boaed, ConfigurationView.WidthBoard, ConfigurationView.HeightBoard);
            foreach (var item in listDrowable)
            {
                g.DrawImage(item.Img, item.X + ConfigurationView.Boaed, item.Y + ConfigurationView.Boaed, item.Width, item.Height);
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

        private void GraphicsOption()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        public void Add(int id, TypeUnit typeUnit, int x, int y, Property[] properties)
        {
            BaseView view = factoryViewUnit.Create(id, x, y, typeUnit);
            if (properties != null)
            {
                switch (typeUnit)
                {
                    case TypeUnit.PlainTank:

                        foreach (var p in properties)
                        {
                            if (p.Type == PropertiesType.Direction)
                            {
                                view.Properties[PropertiesType.Direction] = p.Value;
                            }
                            else if (p.Type == PropertiesType.IsStop)
                            {
                                view.Properties[PropertiesType.IsStop] = p.Value;
                            }
                        }
                        break;
                    case TypeUnit.Shell:
                        view.Properties[PropertiesType.Direction] = properties[0].Value;
                        break;
                }
            }
            if (view != null) listDrowable.Add(view);
        }

        public void Remove(int id)
        {
            listDrowable.Remove(id);
        }

        public void Clear()
        {
            listDrowable.Clear();
        }

        public void Update(int id, PropertiesType prop, object value)
        {

            BaseView viewUpdate = listDrowable.FindByID(id);
            switch (prop)
            {
                case PropertiesType.Direction:
                case PropertiesType.IsStop:
                case PropertiesType.Scoore:
                    viewUpdate.Properties[prop] = value;
                    break;
                case PropertiesType.X:
                    viewUpdate.X = (int)value;
                    break;
                case PropertiesType.Y:
                    viewUpdate.Y = (int)value;
                    break;
            }
        }
    }
}
