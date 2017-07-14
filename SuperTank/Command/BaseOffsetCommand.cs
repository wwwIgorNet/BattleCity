using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    abstract class BaseOffsetCommand : MoveCommand
    {
        public BaseOffsetCommand(Unit unit) : base(unit)
        {
        }

        public abstract void Execute();

        protected bool OffsetToBorderTile()
        {
            bool res = false;
            switch (Direction)
            {
                case Direction.Left:
                    res = Offset(false, Unit.X, ConfigurationGame.WidthTile);
                    break;
                case Direction.Up:
                    res = Offset(false, Unit.Y, ConfigurationGame.HeightTile);
                    break;
                case Direction.Right:
                    res = Offset(true, Unit.X, ConfigurationGame.WidthTile);
                    break;
                case Direction.Down:
                    res = Offset(true, Unit.Y, ConfigurationGame.HeightTile);
                    break;
            }
            return res;
        }
        /// <summary>
        /// Смешение к границе тайла для управляемости танка на поворотах (зазор всего в 1 пиксель не даст проехать)
        /// </summary>
        /// <param name="directionDownOrRight">
        /// Если направление движения в низ или в право передайом true иначе false, штоб знать в какую сторону смещаться
        /// </param>
        /// <param name="coordXOrY">
        /// Если направление движения в лево или в право передайом координату X иначе Y
        /// </param>
        /// <param name="sizeSide">
        /// Если направление движения в лево или в право передайом координату WidhtTile иначе HeightTile
        /// </param>
        /// <returns>
        /// Если смищение == 0 return true else false
        /// </returns>
        private bool Offset(bool directionDownOrRight, int coordXOrY, int sizeSide)
        {
            int offset = coordXOrY % sizeSide;
            if (offset == 0) return true;

            if (directionDownOrRight) offset = sizeSide - offset;

            if (offset >= Velosity) Move(Velosity);
            else Move(offset);
            return false;
        }
    }
}
