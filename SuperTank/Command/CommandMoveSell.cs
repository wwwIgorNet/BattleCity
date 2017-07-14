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
            if (colision != null && !Unit.Equals(colision))
            {
                switch (colision.Type)
                {
                    case TypeUnit.PlainTank:
                        break;
                    case TypeUnit.Shell:
                        break;
                    case TypeUnit.BrickWall:
                        scene.Remove(colision);
                        OnShellDestroy();
                        break;
                    default:
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
