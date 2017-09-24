using System.Collections.Generic;
using System.Drawing;

namespace SuperTank
{
    public class Tank : MovableUnit
    {
        private int glidIteration = 0;
        private int velosityShell;
        private TypeUnit typeShell;
        private IDriver driver;
        private bool isPause;

        public Tank(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, IDriver driver, TypeUnit typeShell, int velosityShell) : base(id, x, y, width, height, type, velosity, direction)
        {
            this.driver = driver;
            this.typeShell = typeShell;
            this.velosityShell = velosityShell;
            Properties[PropertiesType.IsParking] = true;
            Properties[PropertiesType.Detonation] = false;
            Properties[PropertiesType.IsBonusTank] = false;
            Properties[PropertiesType.IsInvulnerable] = false;
        }

        public IDriver Driver { get { return driver; } }
        public bool IsPause
        {
            get
            {
                return isPause;
            }
            set
            {
                isPause = value;
                IsParking = value;
            }
        }

        public override void Update()
        {
            if (!IsPause) driver.Update();
        }
        public override void Start()
        {
            base.Start();
            AddToScene();
            Scene.Tanks.Add(this);
            IsPause = false;
        }
        public override void Dispose()
        {
            base.Dispose();
            Scene.Tanks.Remove(this);
        }

        #region Move
        public virtual bool IsParking
        {
            get { return (bool)Properties[PropertiesType.IsParking]; }
            set { Properties[PropertiesType.IsParking] = value; }
        }

        protected virtual bool OnIce { get; set; }

        public virtual void Move()
        {
            IsParking = false;
            MoveColision();
            glidIteration = 0;
        }

        private void Move(ref Rectangle rect, int spead)
        {
            switch (Direction)
            {
                case Direction.Up:
                    rect.Y -= spead;
                    break;
                case Direction.Right:
                    rect.X += spead;
                    break;
                case Direction.Down:
                    rect.Y += spead;
                    break;
                case Direction.Left:
                    rect.X -= spead;
                    break;
            }
        }
        private void MoveColision()
        {
            Rectangle rect = BoundingBox;
            Move(ref rect, Velosity);

            List<Unit> colision = ColisionWithUnit(rect);
            Ofset(ref rect, colision);

            while (Scene.ColisionBoard(rect)) Move(ref rect, -1);

            MoveToRect(rect);

            TestOnIce(colision);
        }
        private void TestOnIce(List<Unit> colision)
        {
            if (colision.Find(u => u.Type == TypeUnit.Ice && u.BoundingBox.IntersectsWith(BoundingBox)) != null)
                OnIce = true;
            else OnIce = false;
        }
        private void MoveToRect(Rectangle rect)
        {
            switch (Direction)
            {
                case Direction.Up:
                case Direction.Down:
                    Y = rect.Y;
                    break;
                case Direction.Right:
                case Direction.Left:
                    X = rect.X;
                    break;
            }
        }
        private void Ofset(ref Rectangle rect, List<Unit> colision)
        {
            for (int i = 0; i < colision.Count; i++)
            {
                switch (colision[i].Type)
                {//todo
                    case TypeUnit.SmallTankPlaeyr:
                    case TypeUnit.LightTankPlaeyr:
                    case TypeUnit.MediumTankPlaeyr:
                    case TypeUnit.HeavyTankPlaeyr:

                    case TypeUnit.PlainTank:
                    case TypeUnit.ArmoredPersonnelCarrierTank:
                    case TypeUnit.QuickFireTank:
                    case TypeUnit.ArmoredTank:

                    case TypeUnit.BrickWall:
                    case TypeUnit.ConcreteWall:
                    case TypeUnit.Water:
                    case TypeUnit.Star:
                        while (rect.IntersectsWith(colision[i].BoundingBox))
                            Move(ref rect, -1);
                        break;
                }
            }
        }
        private List<Unit> ColisionWithUnit(Rectangle rect)
        {
            List<Unit> res = new List<Unit>(2);
            for (int i = 0; i < Scene.Units.Count; i++)
                if (rect.IntersectsWith(Scene.Units[i].BoundingBox) && !Scene.Units[i].Equals(this))
                    res.Add(Scene.Units[i]);

            return res;
        }
        #endregion

        #region Stop
        public virtual void Stop()
        {
            if (Glide()) return;

            IsParking = OffsetToBorderTile();
        }

        protected bool OffsetToBorderTile()
        {
            bool res = false;
            switch (Direction)
            {
                case Direction.Left:
                    res = Offset(false, X, ConfigurationGame.WidthTile);
                    break;
                case Direction.Up:
                    res = Offset(false, Y, ConfigurationGame.HeightTile);
                    break;
                case Direction.Right:
                    res = Offset(true, X, ConfigurationGame.WidthTile);
                    break;
                case Direction.Down:
                    res = Offset(true, Y, ConfigurationGame.HeightTile);
                    break;
            }
            return res;
        }

        private bool Glide()
        {
            if (OnIce && glidIteration < ConfigurationGame.GlidDelay)
            {
                glidIteration++;
                IsParking = true;

                Rectangle rect = BoundingBox;
                Move(ref rect, Velosity);

                List<Unit> colision = ColisionWithUnit(rect);
                Ofset(ref rect, colision);

                while (Scene.ColisionBoard(rect)) Move(ref rect, -1);

                if (colision.Find(u => u.Type == TypeUnit.Ice && u.BoundingBox.IntersectsWith(rect)) != null)
                {
                    MoveToRect(rect);
                    return OnIce = true;
                }
                else
                {
                    return OnIce = false;
                }
            }

            return false;
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

            int vel;
            if (offset >= Velosity)
                vel = Velosity;
            else vel = offset;

            Rectangle rect = BoundingBox;
            Move(ref rect, vel);

            List<Unit> colision = ColisionWithUnit(rect);
            Ofset(ref rect, colision);

            MoveToRect(rect);
            TestOnIce(colision);

            if (colision.Find(u => u.Type == TypeUnit.SmallTankPlaeyr) != null)
                return true;

            return false;
        }
        #endregion

        #region Turn
        public virtual void Turn(Direction dir)
        {
            // если розвернулся на 180 градусов
            int tmp = (int)(Direction - dir);
            if (tmp == 2 || tmp == -2)
                Direction = dir;

            if (OffsetToBorderTile())
                Direction = dir;
        }
        #endregion

        #region Fire
        public int VelosityShell { get { return velosityShell; } }
        public TypeUnit TypeShell { get { return typeShell; } }
        public virtual Shell Shell { get; set; }

        public virtual bool TryFire()
        {
            if (Shell != null) return false;

            Fire();
            return true;
        }

        protected virtual void Fire()
        {
            Shell = GetShell(velosityShell);
            Shell.UnitDisposable += u => Shell = null;
            Shell.Start();
        }
        protected virtual Shell GetShell(int velosityShell)
        {
            Point pos = GetCoordNewShell();
            return FactoryUnit.CreateShell(pos.X, pos.Y, typeShell, Direction, velosityShell, this);
        }
        protected Point GetCoordNewShell()
        {
            int x = 0;
            int y = 0;
            switch (Direction)
            {
                case Direction.Up:
                    x = X + ConfigurationGame.WidthTile - ConfigurationGame.WidthShell / 2;
                    y = Y + ConfigurationGame.HeightShell;
                    break;
                case Direction.Right:
                    x = X + ConfigurationGame.WidthTank - ConfigurationGame.WidthShell;
                    y = Y + ConfigurationGame.HeightTile - ConfigurationGame.HeightShell / 2;
                    break;
                case Direction.Down:
                    x = X + ConfigurationGame.WidthTile - ConfigurationGame.WidthShell / 2;
                    y = Y + ConfigurationGame.HeightTank - ConfigurationGame.HeightShell;
                    break;
                case Direction.Left:
                    x = X + ConfigurationGame.WidthShell;
                    y = Y + ConfigurationGame.HeightTile - ConfigurationGame.HeightShell / 2;
                    break;
            }
            return new Point(x, y);
        }
        #endregion
    }
}
