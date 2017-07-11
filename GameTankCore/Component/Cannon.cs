using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    /// <summary>
    /// Пушка
    /// </summary>
    class Cannon : ICannon
    {
        private Unit tank;
        private int velosity;
        private Shell shell;

        public Cannon(Unit tank, int velosity)
        {
            this.tank = tank;
            this.velosity = velosity;
        }

        public void Fire()
        {
            if (Level.UpdateObjects.Contains(shell))
                return;

            int x = 0, y = 0;
            bool create = true;
            switch (tank.Direction)
            {
                case Direction.Up:
                    x = tank.X + tank.Width / 2 - Configuration.WidthShell / 2;
                    y = tank.Y;
                    break;
                case Direction.Down:
                    x = tank.X + tank.Width / 2 - Configuration.WidthShell / 2;
                    y = tank.Y + tank.Height;
                    break;
                case Direction.Left:
                    x = tank.X;
                    y = tank.Y + tank.Height / 2 - Configuration.HeightShell / 2;
                    break;
                case Direction.Right:
                    x = tank.X + tank.Width;
                    y = tank.Y + tank.Height / 2 - Configuration.HeightShell / 2;
                    break;
                default:
                    create = false;
                    break;
            }

            if (create)
                shell = Factory.CreateShell(x, y, tank.Direction, velosity);
        }
    }
}
