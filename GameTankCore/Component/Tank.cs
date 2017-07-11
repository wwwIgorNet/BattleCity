using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    class Tank : Unit, ITank
    {

        private bool isParking;
        // Cтарое направление движения
        protected Direction oldDirection;
        private IDriwer driver;
        private IShooter shooter;
        private ICannon cannon;
        protected IColision colision;

        public Tank(int x, int y, int width, int height, Direction direction, TypeObjGame type, int velosity)
            : base(x, y, width, height, direction, type, velosity)
        {
            isParking = true;
            oldDirection = Direction;
            Level.UpdateObjects.Add(this);
        }


        public IDriwer Driver { set { driver = value; } }
        public ICannon Cannon
        {
            set
            {
                cannon = value;
                if(shooter != null)
                    shooter.Cannon = cannon;
            }
        }
        public IShooter Shooter
        {
            set
            {
                shooter = value;
                if (shooter != null)
                    shooter.Cannon = cannon;
            }
        }
        public IColision Colision { set { colision = value; } }

        public void Update()
        {
            isParking = true;
            if (driver != null)
                driver.Update();
            oldDirection = Direction;
            if (shooter != null)
                shooter.Update();
            if (isParking)
                offsetToBorderTile(Direction);
            if (oldDirection != Direction)
                offsetToBorderTile(oldDirection);
        }

        public override void Move()
        {
            base.Move();
            isParking = false;
            if (colision != null)
                colision.Update();
        }
        public override void Dispose()
        {
            base.Dispose();
            Level.UpdateObjects.Remove(this);
        }

        /// <summary>
        /// Смищение к границам тайла
        /// </summary>
        private void offsetToBorderTile(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    OffsetToLeft();
                    break;
                case Direction.Up:
                    OffsetToUp();
                    break;
                case Direction.Right:
                    OffsetToRight();
                    break;
                case Direction.Down:
                    OffsetToDown();
                    break;
            }
        }
        private void OffsetToDown()
        {
            int offset = Configuration.HeightTile - Y % Configuration.HeightTile;
            if (offset == Configuration.HeightTile) return;

            if (offset >= Velosity) Y += Velosity;
            else Y += offset;
        }
        private void OffsetToRight()
        {
            int offset = Configuration.WidthTile - X % Configuration.WidthTile;
            if (offset == Configuration.WidthTile) return;

            if (offset >= Velosity) X += Velosity;
            else X += offset;
        }
        private void OffsetToUp()
        {
            int offset = Y % Configuration.HeightTile;
            if (offset == 0) return;

            if (offset >= Velosity) Y -= Velosity;
            else Y -= offset;
        }
        private void OffsetToLeft()
        {
            int offset = X % Configuration.WidthTile;
            if (offset == 0) return;

            if (offset >= Velosity) X -= Velosity;
            else X -= offset;
        }
    }
}
