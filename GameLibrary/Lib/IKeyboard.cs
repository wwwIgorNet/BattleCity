using GameLibrary.Lib;
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
        [ServiceKnownType(typeof(KeysGame))]
        void KeyDown(KeysGame key);
        /// <summary>
        /// Key up
        /// </summary>
        /// <param name="key">Key code</param>
        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(KeysGame))]
        void KeyUp(KeysGame key);

        bool Down { get; }
        bool Enter { get; }
        bool Escape { get; }
        bool Left { get; }
        bool Right { get; }
        bool Space { get; }
        bool Up { get; }

        void Clear();
    }
}