using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    static class Faktory
    {
        public static ITank CreateTank(String type)
        {
            ITank tank = null;
            switch(type)
            {
                case "plain user":
                    MoveObj movable = new MoveObj(20, 300, SettingsGame.WidthTank, SettingsGame.HeigthTank, Direction.Up);
                    movable.Type = type;
                    Board.AlloObj.Add(movable);
                    tank = new Tank(movable,
                                    new PlayerDriwer(movable, 3),
                                    new PlayerShooter(
                                    new Cannon(movable, 4)));
                    Level.UpdateObjects.Add(tank);
                    break;
            }
            return tank;
        }
    }
}
