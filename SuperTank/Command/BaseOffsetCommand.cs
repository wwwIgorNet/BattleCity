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

        protected void OffsetToBorderTile()
        {
            switch (Direction)
            {
                case Direction.Left:
                    Offset(false, Unit.X, ConfigurationGame.WidthTile);
                    break;
                case Direction.Up:
                    Offset(false, Unit.Y, ConfigurationGame.HeightTile);
                    break;
                case Direction.Right:
                    Offset(true, Unit.X, ConfigurationGame.WidthTile);
                    break;
                case Direction.Down:
                    Offset(true, Unit.Y, ConfigurationGame.HeightTile);
                    break;
            }
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
        private void Offset(bool directionDownOrRight, int coordXOrY, int sizeSide)
        {
            int offset = coordXOrY % sizeSide;
            if (offset == 0) return;

            if (directionDownOrRight) offset = sizeSide - offset;

            if (offset >= Velosity) Move(Velosity);
            else Move(offset);
        }
    }
}
