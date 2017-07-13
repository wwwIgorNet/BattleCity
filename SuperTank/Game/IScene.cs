using System.Collections.ObjectModel;

namespace SuperTank
{
    public interface IScene
    {
        int Height { get; }
        ObservableCollection<Unit> Units { get; }
        int Widtch { get; }

        void Clear();
        Unit Colision(Unit unit);
        bool ColisionBoard(Unit unit);
    }
}