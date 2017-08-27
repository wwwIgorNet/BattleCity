using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class BonusTank : Tank
    {
        private static TypeUnit[] bonuses = new[]
        {
            TypeUnit.Clock,
            TypeUnit.Grenade,
            TypeUnit.Helmet,
            TypeUnit.Pistol,
            TypeUnit.Shovel,
            TypeUnit.StarMedal,
            TypeUnit.Tank,
            TypeUnit.Clock,
            TypeUnit.Grenade
        };

        public BonusTank(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, IDriver driver, TypeUnit typeShell, int velosityShell) : base(id, x, y, width, height, type, velosity, direction, driver, typeShell, velosityShell)
        {
            Properties[PropertiesType.IsBonusTank] = true;
        }

        public override void Dispose()
        {
            base.Dispose();
            Point pos = GenerePositionForBonus();
            FactoryUnit.CreateBonus(pos.X, pos.Y, ConfigurationGame.WidthTile * 2, ConfigurationGame.HeightTile * 2, GetBonusType())
                .Start();
        }

        protected TypeUnit GetBonusType()
        {
            return TypeUnit.StarMedal;//todo
            return bonuses[Game.Random.Next(0, bonuses.Length - 1)];
        }

        public Point GenerePositionForBonus()
        {
            int x = Game.Random.Next(0, ConfigurationGame.WidthBoard / (ConfigurationGame.WidthTile * 2)) * ConfigurationGame.WidthTile * 2;
            int y = Game.Random.Next(0, ConfigurationGame.HeightBoard / (ConfigurationGame.HeightTile * 2)) * ConfigurationGame.HeightTile * 2;

            return new Point(x, y);
        }
    }
}
