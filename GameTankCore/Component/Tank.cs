using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    class Tank : Unit, ITank
    {
        // Если остановлен true
        private bool isParking;
        // Предыдущее направление движения
        private Direction prevDirection;
        // Новое направление
        private Direction newDirection;

        // Водитель танка
        private IDriwer driver;
        // Стрелок
        private IShooter shooter;
        // Пушка
        private ICannon cannon;
        // Столкновения
        private IColision colision;

        public Tank(int x, int y, int width, int height, Direction direction, TypeObjGame type, int velosity)
            : base(x, y, width, height, direction, type, velosity)
        {
            isParking = true;
            newDirection = prevDirection = Direction;
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
            // обновление стрилка
            if (shooter != null)
                shooter.Update();

            // устанавливаем исходное состояние если дривер не вызовет Move() значить стоим
            isParking = true;
            // перед вызовом drivera запоминаем направление
            prevDirection = Direction;
            // обновление водителя
            if (driver != null)
                driver.Update();

            // если изменили направление
            if (prevDirection != Direction)
            {
                // установка старого направления потомушто не доехали к границе тайла
                newDirection = Direction;
                Direction = prevDirection;
                // если розвеонудлся на 180 градусов
                int tmp = (int)(prevDirection - newDirection);
                if (tmp == 2 || tmp == -2)
                    Direction = newDirection;
                // смищаем до границ тайла а потом задайом новое направление
                else if (offsetToBorderTile())
                    Direction = newDirection;
            }

            if (isParking)
                offsetToBorderTile();
        }

        public override void Move()
        {
            base.Move();
            // если мы попали в етот метод то 
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
        /// Смищает танк к границам тайла
        /// </summary>
        /// <returns>
        /// true если танк на границе тайла (координата танка % на ширину тайла == 0)
        /// </returns>
        private bool offsetToBorderTile()
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
            int offset = Configuration.HeightTile - Y % Configuration.HeightTile;
            if (offset == Configuration.HeightTile)
                return true;

            if (offset >= Velosity)
                Y += Velosity;
            else Y += offset;
            return false;
        }
        private bool OffsetToRight()
        {
            int offset = Configuration.WidthTile - X % Configuration.WidthTile;
            if (offset == Configuration.WidthTile)
                return true;

            if (offset >= Velosity)
                X += Velosity;
            else X += offset;
            return false;
        }
        private bool OffsetToUp()
        {
            int offset = Y % Configuration.HeightTile;
            if (offset == 0)
                return true;

            if (offset >= Velosity)
                Y -= Velosity;
            else Y -= offset;
            return false;
        }
        private bool OffsetToLeft()
        {
            int offset = X % Configuration.WidthTile;
            if (offset == 0)
                return true;

            if (offset >= Velosity)
                X -= Velosity;
            else X -= offset;
            return false;
        }
    }
}
