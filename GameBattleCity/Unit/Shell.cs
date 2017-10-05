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
                            Detonation(item, true);
                            return;
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
