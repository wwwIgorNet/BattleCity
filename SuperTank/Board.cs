using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SuperTank
{
    class Scene
    {
        private static readonly ObservableCollection<Unit> alloObj = new ObservableCollection<Unit>();

        public static ObservableCollection<Unit> AlloObj { get { return alloObj; } }
    }
}