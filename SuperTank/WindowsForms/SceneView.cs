using SuperTank.Audio;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTank
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SceneView : Label, IRender
    {
        private readonly Timer timer = new Timer();
        private readonly SortedView listDrowable = new SortedView();
        private readonly IFactoryViewUnit factoryViewUnit = new FactoryViewUnit();

        public SceneView()
        {
            this.BackColor = ConfigurationView.BackColor;
            GraphicsOption();
            this.ClientSize = new Size(ConfigurationView.HeightBoard + ConfigurationView.HeightTile * 2, ConfigurationView.WidthBoard + ConfigurationView.WidthTile * 2);
            timer.Interval = ConfigurationView.TimerInterval;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.Black, ConfigurationView.WidthTile, ConfigurationView.HeightTile, ConfigurationView.WidthBoard, ConfigurationView.HeightBoard);
            foreach (var item in listDrowable)
            {
                g.DrawImage(item.Img, item.X + ConfigurationView.WidthTile, item.Y + ConfigurationView.HeightTile, item.Width, item.Height);
            }
        }

        public void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties)
        {
            BaseView view = factoryViewUnit.Create(id, x, y, typeUnit, properties);
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
                case PropertiesType.X:
                    viewUpdate.X = (int)value;
                    break;
                case PropertiesType.Y:
                    viewUpdate.Y = (int)value;
                    break;
                //case PropertiesType.Direction:
                //case PropertiesType.IsStop:
                //case PropertiesType.Detonation:
                //case PropertiesType.Glide:
                default:
                    viewUpdate.Properties[prop] = value;
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

        public void AddRange(List<UnitDataForView> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                UnitDataForView data = collection[i];
                Add(data.ID, data.TypeUnit, data.X, data.Y, data.Properties);
            }
        }

        public void Init()
        { }
    }
}
