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
        private Action shellDestroy;

        public CommandMoveSell(Unit unit, IScene scene, Action shellDestroy) : base(unit)
        {
            this.scene = scene;
            this.shellDestroy = shellDestroy;       
        }

        public override void Execute()
        {
            Move(Velosity);
            if (scene.ColisionBoard(Unit))
                OnShellDestroy();
            Unit colision = scene.Colision(Unit);
            if (colision != null)
            {
                switch (colision.Type)
                {
                    case TypeUnit.PlainTank:
                        if (!Unit.Properties[PropertiesType.Owner].Equals(colision.Properties[PropertiesType.Owner]))
                        {
                            scene.Remove(colision);
                            OnShellDestroy();
                        }
                        break;
                    case TypeUnit.Shell:
                        if (!Unit.Properties[PropertiesType.Owner].Equals(colision.Properties[PropertiesType.Owner]))
                        {
                            scene.Remove(colision);
                            OnShellDestroy();
                        }
                        break;
                    case TypeUnit.BrickWall:
                        scene.Remove(colision);
                        OnShellDestroy();
                        break;
                }
            }
        }

        private void OnShellDestroy()
        {
            scene.Remove(Unit);
            shellDestroy.Invoke();
        }
    }
}
