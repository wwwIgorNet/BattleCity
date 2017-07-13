using System.Drawing;

namespace SuperTank.View
{
    public abstract class BaseView
    {
        private Unit unit;

        public BaseView(Unit unit)
        {
            this.unit = unit;
        }

        public Unit Unit { get { return unit; } }
        public int ZIndex { get; set; }

        public abstract void Draw(Graphics g);

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            BaseView view = obj as BaseView;
            if (obj == null)
                return false;

            return ZIndex.Equals(view.ZIndex) && Unit.Equals(view.Unit);
        }
        public bool Equals(Unit obj)
        {
            if (obj == null)
                return false;

            return Unit.Equals(obj);
        }
    }
}
