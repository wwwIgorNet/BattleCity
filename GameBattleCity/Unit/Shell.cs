using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                        case TypeUnit.PlainTank:
                        case TypeUnit.ArmoredPersonnelCarrierTank:
                        case TypeUnit.QuickFireTank:
                            if (!Owner.Equals(item.Properties[PropertiesType.Owner]) && !(bool)item.Properties[PropertiesType.IsInvulnerable])
                            {
                                item.Properties[PropertiesType.Detonation] = true;
                                Detonation(item, true);
                                BigDetonation detonation = FactoryUnit.CreateBigDetonation(item, TypeUnit.BigDetonation);
                                detonation.Start();
                                return;
                            }
                            break;
                        case TypeUnit.ArmoredTank:
                             if (!Owner.Equals(item.Properties[PropertiesType.Owner]))
                            {
                                int numberOfHits = (int)item.Properties[PropertiesType.NumberOfHits];
                                numberOfHits++;
                                if (numberOfHits == 4)
                                {
                                    item.Properties[PropertiesType.Detonation] = true;
                                    Detonation(item, true);
                                    BigDetonation detonation = FactoryUnit.CreateBigDetonation(item, TypeUnit.BigDetonation);
                                    detonation.Start();
                                    return;
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

        protected virtual void Detonation(Unit item, bool dispouse)
        {
            if (item != null && dispouse) item.Dispose();
            IsDetonation = true;
        }
    }
}
