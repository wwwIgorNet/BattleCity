using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class Tank : MovableUnit
    {
        private IScene scene;
        private int velosityShell;

        public Tank(int id, int x, int y, int width, int height, TypeUnit type, IScene scene, int velosity, Direction direction, int velosityShell) : base(id, x, y, width, height, type, velosity, direction)
        {
            this.scene = scene;
            this.velosityShell = velosityShell;
            Properties[PropertiesType.IsParking] = true;
            Properties[PropertiesType.Detonation] = false;
        }

        #region Move

        protected virtual bool IsParking
        {
            get { return (bool)Properties[PropertiesType.IsParking]; }
            set { Properties[PropertiesType.IsParking] = value; }
        }
        protected virtual bool IsGlide { get; set; }


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

            Glide(ref rect, colision);

            while (scene.ColisionBoard(rect)) Move(ref rect, -1);

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

        private void Glide(ref Rectangle rect, List<Unit> colision)
        {
            for (int i = 0; i < colision.Count; i++)
            {
                if (colision[i].Type == TypeUnit.Ice && rect.IntersectsWith(colision[i].BoundingBox))
                {
                    IsGlide = true;
                    Move(ref rect, 2);
                    Ofset(ref rect, colision);
                    return;
                }
            }
            IsGlide = false;
        }

        private void Ofset(ref Rectangle rect, List<Unit> colision)
        {
            for (int i = 0; i < colision.Count; i++)
            {
                if (!(colision[i].Type == TypeUnit.Shell) && !(colision[i].Type == TypeUnit.Forest) && !(colision[i].Type == TypeUnit.Ice))
                    while (rect.IntersectsWith(colision[i].BoundingBox))
                        Move(ref rect, -1);
            }
        }

        private List<Unit> ColisionWithUnit(Rectangle rect)
        {
            List<Unit> res = new List<Unit>(2);
            for (int i = 0; i < scene.Units.Count; i++)
                if (rect.IntersectsWith(scene.Units[i].BoundingBox) && !scene.Units[i].Equals(this))
                    res.Add(scene.Units[i]);

            return res;
        }

        public void Move()
        {
            IsParking = false;
            MoveColision();
        }
        #endregion

        #region Stop

        public void Stop()
        {
            if (!IsParking)
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
        #endregion

        #region Turn

        public void Turn(Direction dir)
        {
            if (OffsetToBorderTile())
                Direction = dir;
        }
        #endregion

        #region Fire

        public virtual Shell Shell { get; set; }

        public bool Fire()
        {
            if (Shell != null) return false;

            int x = 0, y = 0;
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
                    y = Y + ConfigurationGame.HeigthTank - ConfigurationGame.HeightShell;
                    break;
                case Direction.Left:
                    x = X + ConfigurationGame.WidthShell;
                    y = Y + ConfigurationGame.HeightTile - ConfigurationGame.HeightShell / 2;
                    break;
            }
            Shell shell = GetShell(x, y, velosityShell, scene);
            Shell = shell;
            shell.Start();
            return true;
        }

        protected virtual Shell GetShell(int x, int y, int velosityShell, IScene scene)
        {
            return new Shell(Unit.NextID, x, y, ConfigurationGame.WidthShell, ConfigurationGame.HeightShell, TypeUnit.Shell, velosityShell, Direction, this, scene);
        }
        #endregion
    }
}
