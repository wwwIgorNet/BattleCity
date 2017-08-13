using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    class CommandMoveSell : CommandMove
    {
        private IScene scene;
        private Action shellDetonaition;
        private int delayDetonation = 0;

        public CommandMoveSell(Unit unit, IScene scene, Action shellDetonaition) : base(unit)
        {
            this.scene = scene;
            this.shellDetonaition = shellDetonaition;
            Unit.Properties[PropertiesType.Detonation] = false;
        }

        public override void Execute()
        {
            if ((bool)Unit.Properties[PropertiesType.Detonation])
            {
                if (delayDetonation == ConfigurationGame.DelayDetonation)
                    OnShellDestroy();
                delayDetonation++;
                return;
            }

            Move(Velosity);
            if (scene.ColisionBoard(Unit))
                Unit.Properties[PropertiesType.Detonation] = true;
            for(int i = 0; i < scene.Units.Count; i++)
            {
                Unit item = scene.Units[i];
                if(item.BoundingBox.IntersectsWith(this.Unit.BoundingBox) && !item.Equals(this.Unit))
                    switch (item.Type)
                    {
                        case TypeUnit.PainTank:
                            if (!Unit.Properties[PropertiesType.Owner].Equals(item.Properties[PropertiesType.Owner]))
                            {
                                scene.Remove(item);
                                Unit.Properties[PropertiesType.Detonation] = true;
                            }
                            break;
                        case TypeUnit.Shell:
                            if (!Unit.Properties[PropertiesType.Owner].Equals(item.Properties[PropertiesType.Owner]))
                            {
                                if (Unit.Properties[PropertiesType.Detonation].Equals(false))
                                {
                                    scene.Remove(item);
                                    Unit.Properties[PropertiesType.Detonation] = true;
                                }
                            }
                            break;
                        case TypeUnit.BrickWall:
                            scene.Remove(item);
                            Unit.Properties[PropertiesType.Detonation] = true;
                            break;
                        case TypeUnit.ConcreteWall:
                            Unit.Properties[PropertiesType.Detonation] = true;
                            break;
                    }
            }
        }

        private void OnShellDestroy()
        {
            scene.Remove(Unit);
            shellDetonaition.Invoke();
        }
    }
}
