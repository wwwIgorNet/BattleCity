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
        Unit Colision(Unit unit);
        bool ColisionBoard(Unit unit);
        Unit Colision(Rectangle rect, int unitId);
        bool ColisionBoard(Rectangle rect);
    }
}