using GameBattleCity.Lib;
using System;
using System.Collections.Generic;
using System.Timers;

namespace SuperTank
{
    public class Shell : MovableUnit
    {
        private int delayDetonation = 0;

        public Shell(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, Tank ownerTank) : base(id, x, y, width, height, type, velosity, direction)
        {
            Owner = (Owner)ownerTank.Properties[PropertiesType.Owner];
            ownerTank.Shell = null;
            IsDetonation = false;
        }

        public override void Start()
        {
            base.Start();
            AddToScene();
        }
        public override void Update()
        {
            if (IsDetonation)
            {
                delayDetonation++;
                if (delayDetonation == ConfigurationGame.TimeDetonation)
                    Dispose();

                return;
            }

            Move(Velosity);

            if (Scene.ColisionBoard(this))
                Detonation(null, false);
            else
                ColisionWichUnit();
        }

        protected Owner Owner
        {
            set { Properties[PropertiesType.Owner] = value; }
            get { return (Owner)Properties[PropertiesType.Owner]; }
        }
        protected bool IsDetonation
        {
            set { Properties[PropertiesType.Detonation] = value; }
            get { return (bool)Properties[PropertiesType.Detonation]; }
        }

        protected virtual void Detonation(Unit item, bool dispouse)
        {
            if (item != null && dispouse) item.Dispose();
            IsDetonation = true;
        }

        private void ColisionWichUnit()
        {
            for (int i = 0; i < Scene.Units.Count; i++)
            {
                Unit item = Scene.Units[i];
                if (item.BoundingBox.IntersectsWith(this.BoundingBox) && !item.Equals(this))
                {
                    switch (item.Type)
                    {
                        // todo
                        case TypeUnit.SmallTankPlaeyr:
                        case TypeUnit.LightTankPlaeyr:
                        case TypeUnit.MediumTankPlaeyr:
                        case TypeUnit.HeavyTankPlaeyr:
                            if (!(bool)item.Properties[PropertiesType.IsInvulnerable])
                            {
                                if (Owner.Equals(Owner.Enemy)) HitInTank(item);
                                else if (Owner.Equals(Owner.IPlayer) && item.Properties[PropertiesType.Owner].Equals(Owner.IIPlayer)
                                    || Owner.Equals(Owner.IIPlayer) && item.Properties[PropertiesType.Owner].Equals(Owner.IPlayer))
                                {
                                    PauseTankPlayer(item);
                                    Detonation(item, false);
                                }
                            }
                            break;

                        case TypeUnit.PlainTank:
                        case TypeUnit.ArmoredPersonnelCarrierTank:
                        case TypeUnit.QuickFireTank:
                            if (!Owner.Equals(item.Properties[PropertiesType.Owner])) HitInTank(item);
                            break;
                        case TypeUnit.ArmoredTank:
                            if (!Owner.Equals(item.Properties[PropertiesType.Owner]))
                            {
                                int numberOfHits = (int)item.Properties[PropertiesType.NumberOfHits];
                                numberOfHits++;
                                if (numberOfHits == 4)
                                {
                                    HitInTank(item);
                                }
                                else
                                {
                                    item.Properties[PropertiesType.NumberOfHits] = numberOfHits;
                                    Detonation(item, false);
                                }
                            }
                            break;
                        case TypeUnit.Shell:
                        case TypeUnit.ConcreteWallShell:
                            if (!Owner.Equals(item.Properties[PropertiesType.Owner]))
                            {
                                ((Shell)item).Dispose();
                                Dispose();
                                return;
                            }
                            break;
                        case TypeUnit.BrickWall:
                            DestroyBlickWall(item);
                            break;
                        case TypeUnit.ConcreteWall:
                            Detonation(item, false);
                            break;
                        case TypeUnit.Eagle:
                            if (!(bool)item.Properties[PropertiesType.Detonation])
                            {
                                Detonation(item, true);
                            }
                            break;
                    }
                }
            }
        }

        private void DestroyBlickWall(Unit unit)
        {
            switch ((TypeBlickWall)unit.Properties[PropertiesType.TypeBlickWall])
            {
                case TypeBlickWall.Whole:
                    switch (Direction)
                    {
                        case Direction.Up:
                            SetTypeBlickWall(unit, TypeBlickWall.Top);
                            unit.Height /= 2;
                            break;
                        case Direction.Right:
                            SetTypeBlickWall(unit, TypeBlickWall.Right);
                            unit.Width /= 2;
                            unit.X += unit.Width;
                            break;
                        case Direction.Down:
                            SetTypeBlickWall(unit, TypeBlickWall.Bottom);
                            unit.Height /= 2;
                            unit.Y += unit.Height;
                            break;
                        case Direction.Left:
                            SetTypeBlickWall(unit, TypeBlickWall.Left);
                            unit.Width /= 2;
                            break;
                    }
                    Detonation(unit, false);
                    break;
                case TypeBlickWall.Top:
                    switch (Direction)
                    {
                        case Direction.Up:
                        case Direction.Down:
                            Detonation(unit, true);
                            return;
                        case Direction.Right:
                            SetTypeBlickWall(unit, TypeBlickWall.TopRight);
                            unit.X += ConfigurationGame.WidthTile / 2;
                            break;
                        case Direction.Left:
                            SetTypeBlickWall(unit, TypeBlickWall.TopLeft);
                            break;
                    }
                    unit.Width /= 2;
                    Detonation(unit, false);
                    break;
                case TypeBlickWall.Bottom:
                    switch (Direction)
                    {
                        case Direction.Up:
                        case Direction.Down:
                            Detonation(unit, true);
                            return;
                        case Direction.Right:
                            SetTypeBlickWall(unit, TypeBlickWall.BootomRight);
                            unit.X += ConfigurationGame.WidthTile / 2;
                            break;
                        case Direction.Left:
                            SetTypeBlickWall(unit, TypeBlickWall.BottomLeft);
                            break;
                    }
                    unit.Width /= 2;
                    Detonation(unit, false);
                    break;
                case TypeBlickWall.Left:
                    switch (Direction)
                    {
                        case Direction.Right:
                        case Direction.Left:
                            Detonation(unit, true);
                            return;
                        case Direction.Up:
                            SetTypeBlickWall(unit, TypeBlickWall.TopLeft);
                            break;
                        case Direction.Down:
                            SetTypeBlickWall(unit, TypeBlickWall.BottomLeft);
                            unit.Y += ConfigurationGame.HeightTile / 2;
                            break;
                    }
                    unit.Height /= 2;
                    Detonation(unit, false);
                    break;
                case TypeBlickWall.Right:
                    switch (Direction)
                    {
                        case Direction.Right:
                        case Direction.Left:
                            Detonation(unit, true);
                            return;
                        case Direction.Up:
                            SetTypeBlickWall(unit, TypeBlickWall.TopRight);
                            break;
                        case Direction.Down:
                            SetTypeBlickWall(unit, TypeBlickWall.BootomRight);
                            unit.Y += ConfigurationGame.HeightTile / 2;
                            break;
                    }
                    unit.Height /= 2;
                    Detonation(unit, false);
                    break;
                case TypeBlickWall.TopLeft:
                    Detonation(unit, true);
                    break;
                case TypeBlickWall.TopRight:
                    Detonation(unit, true);
                    break;
                case TypeBlickWall.BottomLeft:
                    Detonation(unit, true);
                    break;
                case TypeBlickWall.BootomRight:
                    Detonation(unit, true);
                    break;
            }
        }

        private static void SetTypeBlickWall(Unit unit, TypeBlickWall typeBlickWall)
        {
            unit.Properties[PropertiesType.TypeBlickWall] = typeBlickWall;
        }

        private static void PauseTankPlayer(Unit item)
        {
            Timer t = new Timer();
            t.Interval = ConfigurationGame.DelayTimePausePlayerTank;
            t.Elapsed += (s, e) =>
            {
                t.Stop();
                ((Tank)item).IsPause = false;
            };
            ((Tank)item).IsPause = true;
            t.Start();
        }

        private void HitInTank(Unit item)
        {
            item.Properties[PropertiesType.Detonation] = true;
            Detonation(item, true);
            BigDetonation detonation = FactoryUnit.CreateBigDetonation(item, TypeUnit.BigDetonation);
            detonation.Start();
        }
    }
}
