using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    static class Factory
    {
        public static ITank CreateTank(TypeObjGame type)
        {
            ITank tank = null;
            switch(type)
            {
                case TypeObjGame.PlainUserTank:
                    MoveObj movable = new MoveObj(20, 300, Configuration.WidthTank, Configuration.HeigthTank, Direction.Up);
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

        internal static ObjGame CreateBrickWall(int x, int y)
        {
            ObjGame obj = new ObjGame(x, y, Configuration.WidthTile, Configuration.HeightTile);
            obj.Type = TypeObjGame.BrickWall;
            Board.AlloObj.Add(obj);
            return obj;
        }
    }
}
