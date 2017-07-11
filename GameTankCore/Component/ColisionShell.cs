using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    class ColisionShell : IColision
    {
        private Unit shell;

        public ColisionShell(Unit shell)
        {
            this.shell = shell;
        }

        public void Update()
        {
            if (Board.ColisionBoard(shell))
                shell.Dispose();
            ObjGame obj = Board.Colision(shell);
            if (obj == null) return;

            switch (obj.Type)
            {
                case TypeObjGame.BrickWall:
                    obj.Dispose();
                    shell.Dispose();
                    break;
            }
        }
    }
}
