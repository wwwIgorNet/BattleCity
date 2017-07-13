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

        public abstract void Draw(Graphics g);
    }
}
