using System.ServiceModel;
using System.Windows.Forms;

namespace SuperTank
{
    /// <summary>
    /// Keyboard status
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Keyboard : IKeyboard
    {
        public bool Enter { get; protected set; }
        public bool Escape { get; protected set; }
        public bool Left { get; protected set; }
        public bool Right { get; protected set; }
        public bool Down { get; protected set; }
        public bool Up { get; protected set; }
        public bool Space { get; protected set; }

        public void KeyDown(Keys key)
        {
            if (key == Keys.Left)
                Left = true;
            else if (key == Keys.Right)
                Right = true;
            else if (key == Keys.Up)
                Up = true;
            else if (key == Keys.Down)
                Down = true;
            else if (key == Keys.Space)
                Space = true;
            else if (key == Keys.Enter)
                Enter = true;
            else if (key == Keys.Escape)
                Escape = true;
        }
        public void KeyUp(Keys key)
        {
            if (key == Keys.Left)
                Left = false;
            else if (key == Keys.Right)
                Right = false;
            else if (key == Keys.Up)
                Up = false;
            else if (key == Keys.Down)
                Down = false;
            else if (key == Keys.Space)
                Space = false;
            else if (key == Keys.Enter)
                Enter = false;
            else if (key == Keys.Escape)
                Escape = false;
        }
    }
}
