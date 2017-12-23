using GameLibrary.Lib;
using System.ServiceModel;
using System.Windows.Forms;

namespace SuperTank
{
    /// <summary>
    /// Keyboard interface
    /// </summary>
    public interface IKeyboard
    {
        void KeyDown(KeysGame key);
        void KeyUp(KeysGame key);
    }
}