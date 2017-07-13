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
            switch (Direction)
            {
                case Direction.Left:
                    return OffsetToLeft();
                case Direction.Up:
                    return OffsetToUp();
                case Direction.Right:
                    return OffsetToRight();
                case Direction.Down:
                    return OffsetToDown();
            }
            return false;
        }
        private bool OffsetToDown()
        {
            int offset = ConfigurationGme.HeightTile - Unit.Y % ConfigurationGme.HeightTile;
            if (offset == ConfigurationGme.HeightTile)
                return true;

            if (offset >= Velosity)
                Move(Velosity);
            else Move(offset);
            return false;
        }
        private bool OffsetToRight()
        {
            int offset = ConfigurationGme.WidthTile - Unit.X % ConfigurationGme.WidthTile;
            if (offset == ConfigurationGme.WidthTile)
                return true;

            if (offset >= Velosity)
                Move(Velosity);
            else Move(offset);
            return false;
        }
        private bool OffsetToUp()
        {
            int offset = Unit.Y % ConfigurationGme.HeightTile;
            if (offset == 0)
                return true;

            if (offset >= Velosity)
                Unit.Y -= Velosity;
            else Unit.Y -= offset;
            return false;
        }
        private bool OffsetToLeft()
        {
            int offset = Unit.X % ConfigurationGme.WidthTile;
            if (offset == 0)
                return true;

            if (offset >= Velosity)
                Unit.X -= Velosity;
            else Unit.X -= offset;
            return false;
        }
    }
}
