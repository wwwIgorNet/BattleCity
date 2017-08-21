using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class Shell : MovableUnit, IUpdatable
    {
        private int delayDetonation = 0;
        private Tank ownerTank;

        public Shell(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, Tank ownerTank) : base(id, x, y, width, height, type, velosity, direction)
        {
            this.ownerTank = ownerTank;
            Owner = (Owner)ownerTank.Properties[PropertiesType.Owner];
            ownerTank.Shell = null;
            IsDetonation = false;
        }

        public void Start()
        {
            Game.Updatable.Add(this);
            AddToScene();
        }

        public void Update()
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

                        case TypeUnit.PainTank:
                        case TypeUnit.ArmoredPersonnelCarrierTank:
                        case TypeUnit.QuickFireTank:
                            if (!Owner.Equals(item.Properties[PropertiesType.Owner]))
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
                                int n = (int)item.Properties[PropertiesType.NumberOfHits];
                                n++;
                                if (n == 4)
                                {
                                    item.Properties[PropertiesType.Detonation] = true;
                                    Detonation(item, true);
                                    BigDetonation detonation = FactoryUnit.CreateBigDetonation(item, TypeUnit.BigDetonation);
                                    detonation.Start();
                                    return;
                                }
                                else
                                {
                                    item.Properties[PropertiesType.NumberOfHits] = n;
                                    Detonation(item, false);
                                }
                            }
                            break;
                        case TypeUnit.Shell:
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
                    }
                }
            }
        }

        protected virtual void Detonation(Unit item, bool dispouse)
        {
            if (item != null && dispouse) item.Dispose();
            IsDetonation = true;
        }


        public override void Dispose()
        {
            base.Dispose();
            Game.Updatable.Remove(this);
            ownerTank.Shell = null;
        }
    }
}
