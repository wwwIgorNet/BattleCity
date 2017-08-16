using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    class CommandMoveSell : CommandMove
    {
        private int delayDetonation = 0;

        public CommandMoveSell(Unit unit, IScene scene, Action shellDetonaition) : base(unit)
        {
            Scene = scene;
            ShellDetonaition = shellDetonaition;
            IsDetonation = false;
        }

        protected bool IsDetonation
        {
            set { Unit.Properties[PropertiesType.Detonation] = value; }
            get { return (bool)Unit.Properties[PropertiesType.Detonation]; }
        }

        public IScene Scene { get; internal set; }
        public Action ShellDetonaition { get; internal set; }

        public override void Execute()
        {
            if (IsDetonation)
            {
                delayDetonation++;
                if (delayDetonation == ConfigurationGame.DelayDetonation)
                    OnShellDestroy();

                return;
            }

            Move(Velosity);

            if (Scene.ColisionBoard(Unit))
            {
                Detonation(null);
                return;
            }

            ColisionWichUnit();
        }

        private void ColisionWichUnit()
        {
            for (int i = 0; i < Scene.Units.Count; i++)
            {
                Unit item = Scene.Units[i];
                if (item.BoundingBox.IntersectsWith(this.Unit.BoundingBox) && !item.Equals(this.Unit))
                    switch (item.Type)
                    {
                        case TypeUnit.PainTank:
                            if (!Unit.Properties[PropertiesType.Owner].Equals(item.Properties[PropertiesType.Owner]))
                            {
                                Detonation(item);
                                return;
                            }
                            break;
                        case TypeUnit.Shell:
                            if (!Unit.Properties[PropertiesType.Owner].Equals(item.Properties[PropertiesType.Owner]))
                            {
                                if (IsDetonation.Equals(false))
                                {
                                    Detonation(item);
                                    return;
                                }
                            }
                            break;
                        case TypeUnit.BrickWall:
                            Detonation(item);
                            return;
                        case TypeUnit.ConcreteWall:
                            Detonation(null);
                            break;
                    }
            }
        }

        protected virtual void Detonation(Unit item)
        {
            if(item != null) Scene.Remove(item);
            IsDetonation = true;
        }

        protected virtual void OnShellDestroy()
        {
            Scene.Remove(Unit);
            ShellDetonaition.Invoke();
        }
    }
}
