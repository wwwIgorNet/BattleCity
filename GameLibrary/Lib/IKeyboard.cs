using System.ServiceModel;
using System.Windows.Forms;

namespace SuperTank
{
    [ServiceContract]
    public interface IKeyboard
    {
        [OperationContract(IsOneWay = true)]
        void KeyDown(Keys key);
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