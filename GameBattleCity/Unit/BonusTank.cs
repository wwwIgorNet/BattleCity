using System.Drawing;

namespace SuperTank
{
    /// <summary>
    /// Flashing tank, after destroying it appears a bonus
    /// </summary>
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

        public Point GenerePositionForBonus()
        {
            int x = LevelManager.Random.Next(0, ConfigurationGame.WidthBoard / (ConfigurationGame.WidthTile * 2)) * ConfigurationGame.WidthTile * 2;
            int y = LevelManager.Random.Next(0, ConfigurationGame.HeightBoard / (ConfigurationGame.HeightTile * 2)) * ConfigurationGame.HeightTile * 2;

            return new Point(x, y);
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
            return bonuses[LevelManager.Random.Next(0, bonuses.Length - 1)];
        }
    }
}
