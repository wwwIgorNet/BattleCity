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
            this.Unit.Properties[PropertiesType.Scoore] = false;
        }

        public override void Execute()
        {
            if((bool)Unit.Properties[PropertiesType.Scoore])
            {
                if (delayDetonation == ConfigurationGame.DelayDetonation) OnShellDestroy();
                delayDetonation++;
                return;
            }

            Move(Velosity);
            if (scene.ColisionBoard(Unit))
                // OnShellDestroy();
                Unit.Properties[PropertiesType.Scoore] = true;
            Unit colision = scene.Colision(Unit);
            if (colision != null)
            {
                switch (colision.Type)
                {
                    case TypeUnit.PlainTank:
                        if (!Unit.Properties[PropertiesType.Owner].Equals(colision.Properties[PropertiesType.Owner]))
                        {
                            scene.Remove(colision);
                            // OnShellDestroy();
                            Unit.Properties[PropertiesType.Scoore] = true;
                        }
                        break;
                    case TypeUnit.Shell:
                        if (!Unit.Properties[PropertiesType.Owner].Equals(colision.Properties[PropertiesType.Owner]))
                        {
                            if (Unit.Properties[PropertiesType.Scoore].Equals(false))
                            {
                                scene.Remove(colision);
                                // OnShellDestroy();
                                Unit.Properties[PropertiesType.Scoore] = true;
                            }
                        }
                        break;
                    case TypeUnit.BrickWall:
                        scene.Remove(colision);
                        // OnShellDestroy();
                        Unit.Properties[PropertiesType.Scoore] = true;
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
