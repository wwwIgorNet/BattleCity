using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class LevelManager
    {
        private readonly IScene scene;
        private IDriver plaeyrDriver;
        private ISoundGame soundGame;

        public LevelManager(IScene scene, IDriver plaeyrDriver, ISoundGame soundGame)
        {
            this.scene = scene;
            this.plaeyrDriver = plaeyrDriver;
            this.soundGame = soundGame;
        }

        public void CreateLevel(int level)
        {
            scene.Clear();
            List<Unit> objGame = new List<Unit>();
            string[] linesTileMap = File.ReadAllLines(ConfigurationGame.Maps + level);
            int x = 0, y = 0;
            foreach (string line in linesTileMap)
            {
                foreach (char c in line)
                {
                    switch (c)
                    {
                        case '#':
                            objGame.Add(new Unit(Unit.NextID, x, y, ConfigurationGame.WidthTile, ConfigurationGame.HeightTile, TypeUnit.BrickWall));
                            break;
                        case '@':
                            objGame.Add(new Unit(Unit.NextID, x, y, ConfigurationGame.WidthTile, ConfigurationGame.HeightTile, TypeUnit.ConcreteWall));
                            break;
                        case '~':
                            objGame.Add(new Unit(Unit.NextID, x, y, ConfigurationGame.WidthTile, ConfigurationGame.HeightTile, TypeUnit.Water));
                            break;
                        case '%':
                            objGame.Add(new Unit(Unit.NextID, x, y, ConfigurationGame.WidthTile, ConfigurationGame.HeightTile, TypeUnit.Forest));
                            break;
                        case '-':
                            objGame.Add(new Unit(Unit.NextID, x, y, ConfigurationGame.WidthTile, ConfigurationGame.HeightTile, TypeUnit.Ice));
                            break;
                    }
                    x += ConfigurationGame.WidthTile;
                }
                x = 0;
                y += ConfigurationGame.HeightTile;
            }

            objGame.Add(new Unit(Unit.NextID,
                12 * ConfigurationGame.WidthTile,
                ConfigurationGame.HeightBoard - ConfigurationGame.HeightTile * 2,
                ConfigurationGame.WidthTile,
                ConfigurationGame.HeightTile,
                TypeUnit.Eagle));

            scene.AddRange(objGame);

            plaeyrDriver.Tank = new TankPlaetr(Unit.NextID, 9 * ConfigurationGame.WidthTile,
                ConfigurationGame.HeightBoard - ConfigurationGame.HeigthTank, ConfigurationGame.WidthTile * 2, ConfigurationGame.HeightTile * 2, TypeUnit.PainTank, scene, ConfigurationGame.VelostyPlainTank, Direction.Up, ConfigurationGame.VelostyShellPlainTank, soundGame);

            IDriver enemyDriver = new EnemyDriver();
            enemyDriver.Tank = new Tank(Unit.NextID, 0, 0, ConfigurationGame.WidthTile * 2, ConfigurationGame.HeightTile * 2, TypeUnit.PainTank, scene, ConfigurationGame.VelostyPlainTank, Direction.Up, ConfigurationGame.VelostyShellPlainTank);
            enemyDriver.Tank.Direction = Direction.Down;
            AddTank(enemyDriver);

            AddTank(plaeyrDriver);
        }

        public void AddTank(IDriver driver)
        {
            Star star = new Star(Unit.NextID, driver.Tank.X, driver.Tank.Y, driver.Tank.Width, driver.Tank.Height, TypeUnit.Star, driver, scene);
            star.Start();
        }
    }
}
