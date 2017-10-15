using System.ServiceModel;
using System.Windows.Forms;

namespace SuperTank
{
    /// <summary>
    /// Keyboard interface
    /// </summary>
    [ServiceContract]
    public interface IKeyboard
    {
        /// <summary>
        /// Key down
        /// </summary>
        /// <param name="key">Key code</param>
        [OperationContract(IsOneWay = true)]
        void KeyDown(Keys key);
        /// <summary>
        /// Key up
        /// </summary>
        /// <param name="key">Key code</param>
        [OperationContract(IsOneWay = true)]
        void KeyUp(Keys key);

        bool Down { get; }
        bool Enter { get; }
        bool Escape { get; }
        bool Left { get; }
        bool Right { get; }
        bool Space { get; }
        bool Up { get; }
    }
}