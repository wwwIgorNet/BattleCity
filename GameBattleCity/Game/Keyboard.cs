using GameLibrary.Lib;
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

        public void KeyDown(KeysGame key)
        {
            if (key == KeysGame.Left)
                Left = true;
            else if (key == KeysGame.Right)
                Right = true;
            else if (key == KeysGame.Up)
                Up = true;
            else if (key == KeysGame.Down)
                Down = true;
            else if (key == KeysGame.Space)
                Space = true;
            else if (key == KeysGame.Enter)
                Enter = true;
            else if (key == KeysGame.Escape)
                Escape = true;
        }
        public void KeyUp(KeysGame key)
        {
            if (key == KeysGame.Left)
                Left = false;
            else if (key == KeysGame.Right)
                Right = false;
            else if (key == KeysGame.Up)
                Up = false;
            else if (key == KeysGame.Down)
                Down = false;
            else if (key == KeysGame.Space)
                Space = false;
            else if (key == KeysGame.Enter)
                Enter = false;
            else if (key == KeysGame.Escape)
                Escape = false;
        }

        public void Clear()
        {
            Enter = false;
            Escape = false;
            Left = false;
            Right = false;
            Down = false;
            Up = false;
            Space = false;
        }
    }
}
