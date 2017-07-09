using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    interface IMoveble : IDirection
    {
        void MoveUp(int offset);
        void MoveDown(int offset);
        void MoveLeft(int offset);
        void MoveRight(int offset);
    }
}
