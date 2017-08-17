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
            Init(ownerTank);
        }

        public Shell(int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, Tank ownerTank) : base(x, y, width, height, type, velosity, direction)
        {
            Init(ownerTank);
        }

        public Shell(int x, int y, TypeUnit type, int velosity, Direction direction, Tank ownerTank) : this(x, y, ConfigurationGame.WidthShell, ConfigurationGame.HeightShell, type, velosity, direction, ownerTank)
        { }

        private void Init(Tank ownerTank)
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
                        case TypeUnit.PainTank:
                            if (!Owner.Equals(item.Properties[PropertiesType.Owner]))
                            {
                                item.Properties[PropertiesType.Detonation] = true;
                                Detonation(item, true);
                                BigDetonation detonation = new BigDetonation(item, TypeUnit.BigDetonation);
                                detonation.Start();
                                return;
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
