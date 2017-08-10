using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SuperTank
{
    public interface IScene
    {
        Int32 Height { get; }
        Int32 Widtch { get; }
        void Add(Unit unit);
        void Remove(Unit unit);
        void Clear();
        List<Unit> Units { get; }
        bool ColisionBoard(Unit unit);
        bool ColisionBoard(Rectangle rect);
    }
}