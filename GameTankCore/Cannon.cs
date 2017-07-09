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
        private MoveObj tank;
        private int velosity;
        private Shell oldShell;

        public Cannon(MoveObj tank, int velosity)
        {
            this.tank = tank;
            this.velosity = velosity;
        }

        public void Fire()
        {
            MoveObj movable = new MoveObj(tank.X + SettingsGame.WidthTank / 2 -  SettingsGame.WidthShell / 2, tank.Y, SettingsGame.WidthShell, SettingsGame.HeightShell, tank.Direction);
            movable.Type = "shell";
            Board.AlloObj.Add(movable);
            oldShell = new Shell(movable, 3);
            Level.UpdateObjects.Add(oldShell);
        }
    }
}
