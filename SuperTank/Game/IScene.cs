using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
    }
}