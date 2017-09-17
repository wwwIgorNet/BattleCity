using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class PistolBonus
    {
        public static void UpdatableTank(TankPlayer tank)
        {
            TypeUnit newType = TypeUnit.HeavyTankPlaeyr;

            Scene.Remove(tank.OwnerPlaeyr.CurrentTank);
            Scene.Tanks.Remove(tank.OwnerPlaeyr.CurrentTank);
            LevelManager.Updatable.Remove(tank);

            IDriver driver = tank.Driver;
            tank.OwnerPlaeyr.CurrentTank = FactoryUnit.CreateTank(tank.X, tank.Y, newType, tank.Direction, tank.Driver, tank.SoundGame, tank.OwnerPlaeyr);
            tank.OwnerPlaeyr.CurrentTank.Start();
            driver.Tank = tank.OwnerPlaeyr.CurrentTank;

        }
    }
}
