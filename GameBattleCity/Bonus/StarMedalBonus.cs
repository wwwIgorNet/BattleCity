using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class StarMedalBonus
    {
        public static void UpdatableTank(TankPlayer tank)
        {
            TypeUnit newType = tank.Type;

            switch (tank.OwnerPlaeyr.CurrentTank.Type)
            {
                case TypeUnit.SmallTankPlaeyr:
                    newType = TypeUnit.LightTankPlaeyr;
                    break;
                case TypeUnit.LightTankPlaeyr:
                    newType = TypeUnit.MediumTankPlaeyr;
                    break;
                case TypeUnit.MediumTankPlaeyr:
                    newType = TypeUnit.HeavyTankPlaeyr;
                    break;
                case TypeUnit.HeavyTankPlaeyr:
                    break;
            }

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
